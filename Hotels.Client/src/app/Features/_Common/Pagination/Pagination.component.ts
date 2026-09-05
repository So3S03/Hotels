import { Component, computed, input, InputSignal, output } from '@angular/core';
import { ChevronLeft, ChevronRight, ChevronsLeft, ChevronsRight, LucideAngularModule } from 'lucide-angular';

@Component({
  selector: 'app-Pagination',
  templateUrl: './Pagination.component.html',
  styleUrls: ['./Pagination.component.css'],
  standalone: true,
  imports: [LucideAngularModule]
})
export class PaginationComponent {
  //Common Vars
  pageNum: InputSignal<number> = input<number>(1);
  pageSize: InputSignal<5 | 10 | 15> = input<5 | 10 | 15>(5);
  itemCount: InputSignal<number> = input<number>(0);
  icons = {
    ChevronLeft,
    ChevronsLeft,
    ChevronRight,
    ChevronsRight
  }

  pageChange = output<number>();
  pageSizeChange = output<5 | 10 | 15>();

  pagesCount = computed(() =>
    this.itemCount() === 0 ? 1 : Math.ceil(this.itemCount() / this.pageSize())
  );

  startIndex = computed(() =>
    this.itemCount() === 0 ? 0 : (this.pageNum() - 1) * this.pageSize() + 1
  );

  endIndex = computed(() => Math.min(this.pageNum() * this.pageSize(), this.itemCount()));

  visiblePages = computed(() => {
    const total = this.pagesCount();
    const current = this.pageNum();
    const maxPagesToShow = 5;

    if (total <= maxPagesToShow) {
      return Array.from({ length: total }, (_, i) => i + 1);
    }

    let start = Math.max(1, current - 2);
    let end = Math.min(total, current + 2);

    if (current <= 3) {
      start = 1;
      end = maxPagesToShow;
    } else if (current > total - 2) {
      start = total - maxPagesToShow + 1;
      end = total;
    }

    return Array.from({ length: end - start + 1 }, (_, i) => start + i);
  });

  //Logic
  goToPage(p: number) {
    if (p >= 1 && p <= this.pagesCount()) {
      this.pageChange.emit(p);
    }
  }

  nextPage() {
    if (this.pageNum() < this.pagesCount()) {
      this.pageChange.emit(this.pageNum() + 1);
    }
  }

  prevPage() {
    if (this.pageNum() > 1) {
      this.pageChange.emit(this.pageNum() - 1);
    }
  }

  changePageSize(event: Event): void {
    const value = Number((event.target as HTMLSelectElement).value) as 5 | 10 | 15;
    this.pageSizeChange.emit(value);
  }

}
