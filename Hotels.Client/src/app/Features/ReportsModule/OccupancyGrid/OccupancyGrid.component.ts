import { Component, inject, OnInit, signal, WritableSignal } from '@angular/core';
import { Filter, LucideAngularModule } from "lucide-angular";
import { PaginationComponent } from "../../_Common/Pagination/Pagination.component";
import { ReportService } from '../../../Core/Services/ReportsServices/Report.service';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { debounceTime, distinctUntilChanged, Subscription } from 'rxjs';
import { IOccupancyRoomsToReturnDto } from '../../../Core/Interfaces/ReportModule/IOccupancyRoomsToReturnDto';
import { IGridToReturnDto } from '../../../Core/Interfaces/_Common/IGridToReturnDto';
import { HttpErrorResponse } from '@angular/common/http';
import { NgClass } from '@angular/common';

@Component({
  selector: 'app-OccupancyGrid',
  templateUrl: './OccupancyGrid.component.html',
  styleUrls: ['./OccupancyGrid.component.css'],
  imports: [LucideAngularModule, PaginationComponent, ReactiveFormsModule]
})
export class OccupancyGridComponent implements OnInit {
  //DI Container
  private readonly _ReportService: ReportService = inject(ReportService);
  private readonly _FormBuilder: FormBuilder = inject(FormBuilder);

  //Common vars
  fetchSubs: Subscription = new Subscription();
  pageNum: WritableSignal<number> = signal(1);
  pageSize: WritableSignal<5 | 10 | 15> = signal(5);
  iteratedPageSize = Array.from({ length: this.pageSize() });
  isGridLoading: WritableSignal<boolean> = signal(false);
  data: WritableSignal<IOccupancyRoomsToReturnDto[]> = signal([]);
  dataCount: WritableSignal<number> = signal(0);
  icons = {
    Filter
  }
  searchForm: FormGroup = this._FormBuilder.group({
    startDate: [null],
    endDate: [null],
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
          values.startDate,
          values.endDate
        );
      })
    );
  }

  getGrid(pageNum: number, pageSize: 5 | 10 | 15, from?: string, to?: string): void {
    this.pageNum.set(pageNum);
    this.pageSize.set(pageSize);
    this.isGridLoading.set(true);
    this.fetchSubs.add(
      this._ReportService.GetRoomOcuupancy(pageNum, pageSize, from, to).subscribe({
        next: (res: IGridToReturnDto<IOccupancyRoomsToReturnDto>) => {
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
