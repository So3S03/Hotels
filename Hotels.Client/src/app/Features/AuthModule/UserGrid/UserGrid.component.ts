import { Component, ElementRef, inject, OnDestroy, OnInit, signal, ViewChild, WritableSignal } from '@angular/core';
import { BehaviorSubject, debounceTime, distinctUntilChanged, Subscription } from 'rxjs';
import { Filter, LucideAngularModule, UserCheck, UserX } from "lucide-angular";
import { AuthService } from '../../../Core/Services/AuthServices/Auth.service';
import { HttpErrorResponse } from '@angular/common/http';
import { IUserToReturnDto } from '../../../Core/Interfaces/AuthModule/IUserToReturnDto';
import { IGridToReturnDto } from '../../../Core/Interfaces/_Common/IGridToReturnDto';
import { PaginationComponent } from "../../_Common/Pagination/Pagination.component";
import { IActionStatusDto } from '../../../Core/Interfaces/_Common/IActionStatusDto';
import { ToastService } from '../../../Core/Services/ToastServices/Toast.service';

@Component({
  selector: 'app-UserGrid',
  templateUrl: './UserGrid.component.html',
  styleUrls: ['./UserGrid.component.css'],
  imports: [LucideAngularModule, PaginationComponent]
})
export class UserGridComponent implements OnInit, OnDestroy {
  //DI Container
  private readonly _AuthService: AuthService = inject(AuthService);
  private readonly _ToastService: ToastService = inject(ToastService);


  //Common Vars
  fetchSubs: Subscription = new Subscription();
  searchValue: BehaviorSubject<string> = new BehaviorSubject<string>("");
  pageNum: WritableSignal<number> = signal(1);
  pageSize: WritableSignal<5 | 10 | 15> = signal(5);
  iteratedPageSize = Array.from({ length: this.pageSize() });
  isGridLoading: WritableSignal<boolean> = signal(false);
  data: WritableSignal<IUserToReturnDto[]> = signal([]);
  dataCount: WritableSignal<number> = signal(0);
  icons = {
    Filter,
    UserX,
    UserCheck
  };
  @ViewChild("search") searchInput!: ElementRef<HTMLInputElement>

  //Logics
  ngOnInit() {
    this.getGrid(this.pageNum(), this.pageSize());
    this.searchValue.pipe(
      debounceTime(400),
      distinctUntilChanged()
    ).subscribe(value => this.getGrid(1, this.pageSize(), value))
  }

  getGrid(pageNum: number, pageSize: 5 | 10 | 15, Name: string = ""): void
  {
    this.pageNum.set(pageNum);
    this.pageSize.set(pageSize);
    this.isGridLoading.set(true);
    this.fetchSubs.add(
      this._AuthService.GetAllUsers(pageNum, pageSize, Name).subscribe({
        next: (res: IGridToReturnDto<IUserToReturnDto>) => {
          this.isGridLoading.set(false);
          this.data.set(res.data);
          this.dataCount.set(res.total);
        },
        error: (err: HttpErrorResponse) => {
          this.isGridLoading.set(false);
          console.log(err);
        }
      })
    )
  }

  onSearch(e: Event): void
  {
    const value = (e.target as HTMLInputElement).value;
    this.searchValue.next(value);
  }

  activateDeactivateUser(userId: string, isActive: boolean): void
  {
    this._AuthService.ActivateDeActivateUser(userId, isActive).subscribe({
      next: (res: IActionStatusDto) => {
        this._ToastService.showSuccess(res.message);
        this.getGrid(this.pageNum(), this.pageSize());
      },
      error: (err: HttpErrorResponse) => {
        console.log(err);
        this._ToastService.showError(err.message ?? "Something Went Wrong!");
      }
    })
  }

  clear(): void
  {
    this.searchValue.next("");
    this.searchInput.nativeElement.value = "";
  }

  ngOnDestroy(): void {
    this.fetchSubs.unsubscribe();
  }
}
