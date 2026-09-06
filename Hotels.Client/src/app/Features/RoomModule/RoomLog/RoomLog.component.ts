import { Component, EventEmitter, inject, Input, OnInit, Output, signal, WritableSignal } from '@angular/core';
import { LucideAngularModule, X } from "lucide-angular";
import { Subscription } from 'rxjs';
import { PaginationComponent } from "../../_Common/Pagination/Pagination.component";
import { RoomService } from '../../../Core/Services/RoomServices/Room.service';
import { IGridToReturnDto } from '../../../Core/Interfaces/_Common/IGridToReturnDto';
import { HttpErrorResponse } from '@angular/common/http';
import { ILogToReturnDto } from '../../../Core/Interfaces/_Common/ILogToReturnDto';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-RoomLog',
  templateUrl: './RoomLog.component.html',
  styleUrls: ['./RoomLog.component.css'],
  imports: [LucideAngularModule, PaginationComponent, DatePipe]
})
export class RoomLogComponent implements OnInit {
  //DI Container
  private readonly _RoomService: RoomService = inject(RoomService);

  //Common Vars
  @Input({ required: true }) roomId!: string;
  @Output() close: EventEmitter<""> = new EventEmitter<"">();
  icons = {
    X
  };
  fetchSubs: Subscription = new Subscription();
  pageNum: WritableSignal<number> = signal(1);
  savedId: WritableSignal<string> = signal("");
  pageSize: WritableSignal<5 | 10 | 15> = signal(5);
  iteratedPageSize = Array.from({ length: this.pageSize() });
  isGridLoading: WritableSignal<boolean> = signal(false);
  data: WritableSignal<ILogToReturnDto[]> = signal([]);
  dataCount: WritableSignal<number> = signal(0);

  //Logics
  ngOnInit() {
    if(this.roomId)this.savedId.set(this.roomId);
    this.getGrid(this.pageNum(), this.pageSize());
  }


  getGrid(pageNum: number, pageSize: 5 | 10 | 15): void {
    this.pageNum.set(pageNum);
    this.pageSize.set(pageSize);
    this.isGridLoading.set(true);
    this.fetchSubs.add(
      this._RoomService.GetRoomLog(pageNum, pageSize, this.savedId()).subscribe({
        next: (res: IGridToReturnDto<ILogToReturnDto>) => {
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

  closeModal(): void {
    this.close.emit("");
  }
  ngOnDestroy(): void {

  }
}
