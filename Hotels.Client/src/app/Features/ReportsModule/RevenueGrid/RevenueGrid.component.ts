import { Component, inject, OnInit, signal, WritableSignal } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { Filter, LucideAngularModule } from "lucide-angular";
import { ToastService } from '../../../Core/Services/ToastServices/Toast.service';
import { ReportService } from '../../../Core/Services/ReportsServices/Report.service';
import { debounceTime, distinctUntilChanged, Subscription } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { IRevenueToReturnDto } from '../../../Core/Interfaces/ReportModule/IRevenueToReturnDto';

@Component({
  selector: 'app-RevenueGrid',
  templateUrl: './RevenueGrid.component.html',
  styleUrls: ['./RevenueGrid.component.css'],
  imports: [LucideAngularModule, ReactiveFormsModule]
})
export class RevenueGridComponent implements OnInit {
  //DI Container
  private readonly _ReportService: ReportService = inject(ReportService);
  private readonly _ToastService: ToastService = inject(ToastService);
  private readonly _FormBuilder: FormBuilder = inject(FormBuilder);

  //Common vars
  fetchSubs: Subscription = new Subscription();
  iteratedPageSize = Array.from({length: 5});
  isGridLoading: WritableSignal<boolean> = signal(false);
  data: WritableSignal<IRevenueToReturnDto[]> = signal([]);
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
    this.getGrid();
    this.fetchSubs.add(
      this.searchForm.valueChanges.pipe(
        debounceTime(400),
        distinctUntilChanged((prev, curr) => JSON.stringify(prev) === JSON.stringify(curr))
      ).subscribe(values => {
        this.getGrid(
          values.startDate,
          values.endDate,
        );
      })
    );
  }

  getGrid(startDate?: string, endDate?: string): void {
    this.isGridLoading.set(true);
    this.fetchSubs.add(
      this._ReportService.GetRevenue(startDate, endDate).subscribe({
        next: (res: IRevenueToReturnDto[]) => {
          this.isGridLoading.set(false);
          this.data.set(res);
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
