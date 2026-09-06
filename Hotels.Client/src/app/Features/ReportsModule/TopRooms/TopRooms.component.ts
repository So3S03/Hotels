import { Component, inject, OnInit, signal, WritableSignal } from '@angular/core';
import { PaginationComponent } from "../../_Common/Pagination/Pagination.component";
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { debounceTime, distinctUntilChanged, Subscription } from 'rxjs';
import { ReportService } from '../../../Core/Services/ReportsServices/Report.service';
import { IGridToReturnDto } from '../../../Core/Interfaces/_Common/IGridToReturnDto';
import { HttpErrorResponse } from '@angular/common/http';
import { ITopNonCancelledRoomToReturnDto } from '../../../Core/Interfaces/ReportModule/ITopNonCancelledRoomToReturnDto';

@Component({
  selector: 'app-TopRooms',
  templateUrl: './TopRooms.component.html',
  styleUrls: ['./TopRooms.component.css'],
  imports: [PaginationComponent, ReactiveFormsModule]
})
export class TopRoomsComponent implements OnInit {
  //DI Container
  private readonly _ReportService: ReportService = inject(ReportService);
  // private readonly _ToastService: ToastService = inject(ToastService);
  private readonly _FormBuilder: FormBuilder = inject(FormBuilder);

  //Common vars
  fetchSubs: Subscription = new Subscription();
  pageNum: WritableSignal<number> = signal(1);
  pageSize: WritableSignal<5 | 10 | 15> = signal(5);
  iteratedPageSize = Array.from({ length: this.pageSize() });
  isGridLoading: WritableSignal<boolean> = signal(false);
  data: WritableSignal<ITopNonCancelledRoomToReturnDto[]> = signal([]);
  dataCount: WritableSignal<number> = signal(0);
  icons = { }
  searchForm: FormGroup = this._FormBuilder.group({
    type: [null],
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
          values.type,
        );
      })
    );
  }

  getGrid(pageNum: number, pageSize: 5 | 10 | 15, type?: number): void {
    this.pageNum.set(pageNum);
    this.pageSize.set(pageSize);
    this.isGridLoading.set(true);
    this.fetchSubs.add(
      this._ReportService.GetTopNonCancelledRooms(pageNum, pageSize, type).subscribe({
        next: (res: IGridToReturnDto<ITopNonCancelledRoomToReturnDto>) => {
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

  clear(): void {
    this.searchForm.reset();
  }

  ngOnDestroy(): void {
    this.fetchSubs.unsubscribe();
  }
}
