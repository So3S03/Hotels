import { Component, ElementRef, EventEmitter, inject, OnDestroy, OnInit, Output, signal, ViewChild, WritableSignal } from '@angular/core';
import { Calendar, Filter, LucideAngularModule, Pen, Pin, Plus, Trash2 } from 'lucide-angular';
import { BehaviorSubject, debounceTime, distinctUntilChanged, Subscription } from 'rxjs';
import { IGridToReturnDto } from '../../../Core/Interfaces/_Common/IGridToReturnDto';
import { HttpErrorResponse } from '@angular/common/http';
import { ToastService } from '../../../Core/Services/ToastServices/Toast.service';
import { RoomService } from '../../../Core/Services/RoomServices/Room.service';
import { PaginationComponent } from "../../_Common/Pagination/Pagination.component";
import { IRoomToReturnDto } from '../../../Core/Interfaces/RoomModule/IRoomToReturnDto';
import { IActionStatusDto } from '../../../Core/Interfaces/_Common/IActionStatusDto';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-RoomGrid',
  templateUrl: './RoomGrid.component.html',
  styleUrls: ['./RoomGrid.component.css'],
  standalone: true,
  imports: [LucideAngularModule, PaginationComponent, ReactiveFormsModule]
})
export class RoomGridComponent implements OnInit, OnDestroy {
  //DI Container
  private readonly _RoomService: RoomService = inject(RoomService);
  private readonly _ToastService: ToastService = inject(ToastService);
  private readonly _FormBuilder: FormBuilder = inject(FormBuilder);

  //Common vars
  @Output() updateRoom: EventEmitter<string> = new EventEmitter<string>();
  @Output() roomLog: EventEmitter<string> = new EventEmitter<string>();
  fetchSubs: Subscription = new Subscription();
  pageNum: WritableSignal<number> = signal(1);
  pageSize: WritableSignal<5 | 10 | 15> = signal(5);
  iteratedPageSize = Array.from({ length: this.pageSize() });
  isGridLoading: WritableSignal<boolean> = signal(false);
  data: WritableSignal<IRoomToReturnDto[]> = signal([]);
  dataCount: WritableSignal<number> = signal(0);
  icons = {
    Plus,
    Filter,
    Pen,
    Trash2,
    Calendar
  }
  searchForm: FormGroup = this._FormBuilder.group({
    roomType: [null],
    minPrice: [null],
    maxPrice: [null],
    isAvailable: [null]
  });

  //Logics
  ngOnInit() {
    this.getGrid(this.pageNum(), this.pageSize());
    this.fetchSubs.add(
      this.searchForm.valueChanges.pipe(
        debounceTime(400),
        distinctUntilChanged((prev, curr) => JSON.stringify(prev) === JSON.stringify(curr))
      ).subscribe(values => {
        this.getGrid(
          1,
          this.pageSize(),
          values.roomType,
          values.minPrice,
          values.maxPrice,
          values.isAvailable
        );
      })
    );
  }

  getGrid(pageNum: number, pageSize: 5 | 10 | 15, roomType?: number, minPrice?: number, maxPrice?: number, isAvailable?: boolean): void {
    this.pageNum.set(pageNum);
    this.pageSize.set(pageSize);
    this.isGridLoading.set(true);
    this.fetchSubs.add(
      this._RoomService.GetAllRooms(pageNum, pageSize, this.searchForm.value.roomType ?? roomType, this.searchForm.value.minPrice ?? minPrice, this.searchForm.value.maxPrice ?? maxPrice, this.searchForm.value.isAvailable ?? isAvailable).subscribe({
        next: (res: IGridToReturnDto<IRoomToReturnDto>) => {
          this.isGridLoading.set(false);
          this.data.set(res.data);
          this.dataCount.set(res.total);
          console.log(res);
        },
        error: (err: HttpErrorResponse) => {
          this.isGridLoading.set(false);
          console.log(err);
        }
      })
    )
  }

  deleteRoom(id: string): void {
    this._RoomService.DeleteRoom(id).subscribe({
      next: (res: IActionStatusDto) => {
        this._ToastService.showSuccess(res.message);
        this.getGrid(this.pageNum(), this.pageSize());
      },
      error: (err: HttpErrorResponse) => {
        console.log(err);
        this._ToastService.showError(err.error ?? "Something Went Wrong!");
      }
    })
  }
  update(roomId: string): void
  {
    this.updateRoom.emit(roomId);
  }
  showLog(roomId: string): void
  {
    this.roomLog.emit(roomId);
  }

  clear(): void {
    this.searchForm.reset();
  }

  ngOnDestroy(): void {
    this.fetchSubs.unsubscribe();
  }
}
