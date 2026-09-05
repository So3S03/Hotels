import { Component, inject, OnDestroy, OnInit, signal, WritableSignal } from '@angular/core';
import { Subscription } from 'rxjs';
import { Filter, LucideAngularModule, UserCheck, UserX } from "lucide-angular";
import { AuthService } from '../../../Core/Services/AuthServices/Auth.service';
import { HttpErrorResponse } from '@angular/common/http';
import { IUserToReturnDto } from '../../../Core/Interfaces/AuthModule/IUserToReturnDto';
import { IGridToReturnDto } from '../../../Core/Interfaces/_Common/IGridToReturnDto';
import { PaginationComponent } from "../../_Common/Pagination/Pagination.component";

@Component({
  selector: 'app-UserGrid',
  templateUrl: './UserGrid.component.html',
  styleUrls: ['./UserGrid.component.css'],
  imports: [LucideAngularModule, PaginationComponent]
})
export class UserGridComponent implements OnInit, OnDestroy {
  //DI Container
  private readonly _AuthService: AuthService = inject(AuthService);


  //Common Vars
  fetchSubs: Subscription = new Subscription();
  searchValue: WritableSignal<string> = signal("");
  pageNum: WritableSignal<number> = signal(1);
  pageSize: WritableSignal<5 | 10 | 15> = signal(5);
  isGridLoading: WritableSignal<boolean> = signal(false);
  data: WritableSignal<IUserToReturnDto[]> = signal([]);
  dataCount: WritableSignal<number> = signal(0);
  icons = {
    Filter,
    UserX,
    UserCheck
  }

  //Logics
  ngOnInit() {
    this.getGrid(this.pageNum(), this.pageSize())
  }

  private getGrid(pageNum: number, pageSize: number, Name: string = ""): void
  {
    this.isGridLoading.set(true);
    this.fetchSubs.add(
      this._AuthService.GetAllUsers(pageNum, pageSize, Name).subscribe({
        next: (res: IGridToReturnDto<IUserToReturnDto>) => {
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

  clear(): void
  {

  }

  ngOnDestroy(): void {
    this.fetchSubs.unsubscribe();
  }
}
