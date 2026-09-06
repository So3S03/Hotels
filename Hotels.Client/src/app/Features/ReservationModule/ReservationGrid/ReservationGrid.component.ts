import { HttpErrorResponse } from '@angular/common/http';
import { Component, inject, OnInit, signal, WritableSignal } from '@angular/core';
import { IActionStatusDto } from '../../../Core/Interfaces/_Common/IActionStatusDto';
import { IGridToReturnDto } from '../../../Core/Interfaces/_Common/IGridToReturnDto';
import { debounceTime, distinctUntilChanged, Subscription } from 'rxjs';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { Calendar, CircleX, Filter, LucideAngularModule, Plus } from 'lucide-angular';
import { ReservationService } from '../../../Core/Services/ReservationServices/Reservation.service';
import { ToastService } from '../../../Core/Services/ToastServices/Toast.service';
import { PaginationComponent } from "../../_Common/Pagination/Pagination.component";
import { IReservationToReturnDto } from '../../../Core/Interfaces/ReservationModule/IReservationToReturnDto';

@Component({
  selector: 'app-ReservationGrid',
  templateUrl: './ReservationGrid.component.html',
  styleUrls: ['./ReservationGrid.component.css'],
  imports: [PaginationComponent, LucideAngularModule, ReactiveFormsModule]
})
export class ReservationGridComponent implements OnInit {
  //DI Container
  private readonly _ReservationService: ReservationService = inject(ReservationService);
  private readonly _ToastService: ToastService = inject(ToastService);
  private readonly _FormBuilder: FormBuilder = inject(FormBuilder);

  //Common vars
  fetchSubs: Subscription = new Subscription();
  pageNum: WritableSignal<number> = signal(1);
  pageSize: WritableSignal<5 | 10 | 15> = signal(5);
  iteratedPageSize = Array.from({ length: this.pageSize() });
  isGridLoading: WritableSignal<boolean> = signal(false);
  data: WritableSignal<IReservationToReturnDto[]> = signal([]);
  dataCount: WritableSignal<number> = signal(0);
  icons = {
    Plus,
    Filter,
    CircleX,
    Calendar
  }
  searchForm: FormGroup = this._FormBuilder.group({
    status: [null],
    minPrice: [null],
    maxPrice: [null],
    startDate: [null],
    endDate: [null],
    guestName: [null],
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
          values.status,
          values.minPrice,
          values.maxPrice,
          values.startDate,
          values.endDate,
          values.guestName
        );
      })
    );
  }

  getGrid(pageNum: number, pageSize: 5 | 10 | 15, status?: number, minPrice?: number, maxPrice?: number, startDate?: string, endDate?: string, guestName?: string): void {
    this.pageNum.set(pageNum);
    this.pageSize.set(pageSize);
    this.isGridLoading.set(true);
    this.fetchSubs.add(
      this._ReservationService.GetAllReservation(pageNum, pageSize, startDate, endDate, minPrice, maxPrice, status, guestName).subscribe({
        next: (res: IGridToReturnDto<IReservationToReturnDto>) => {
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

  cancelReservation(id: string): void {
    this._ReservationService.CancelReservation(id).subscribe({
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

  clear(): void {
    this.searchForm.reset();
  }

  ngOnDestroy(): void {
    this.fetchSubs.unsubscribe();
  }
}
