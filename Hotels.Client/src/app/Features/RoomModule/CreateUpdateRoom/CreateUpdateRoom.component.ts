import { Component, EventEmitter, inject, Input, OnDestroy, OnInit, Output, signal, WritableSignal } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Loader, LoaderCircle, LucideAngularModule, X } from 'lucide-angular';
import { RoomService } from '../../../Core/Services/RoomServices/Room.service';
import { IActionStatusDto } from '../../../Core/Interfaces/_Common/IActionStatusDto';
import { HttpErrorResponse } from '@angular/common/http';
import { ToastService } from '../../../Core/Services/ToastServices/Toast.service';
import { IRoomToReturnDto } from '../../../Core/Interfaces/RoomModule/IRoomToReturnDto';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-CreateUpdateRoom',
  templateUrl: './CreateUpdateRoom.component.html',
  styleUrls: ['./CreateUpdateRoom.component.css'],
  standalone: true,
  imports: [ReactiveFormsModule, LucideAngularModule]
})
export class CreateUpdateRoomComponent implements OnInit, OnDestroy {
  //DI Container
  private readonly _RoomService: RoomService = inject(RoomService);
  private readonly _FormBuilder: FormBuilder = inject(FormBuilder);
  private readonly _ToastService: ToastService = inject(ToastService);

  //Common vars
  @Input({ required: true }) isAdd!: boolean;
  @Input({ required: true }) roomId!: string;
  @Output() close: EventEmitter<""> = new EventEmitter<"">();
  @Output() refreshGrid: EventEmitter<void> = new EventEmitter<void>();
  title: WritableSignal<"Add Room" | "Update Room"> = signal("Add Room");
  isSubmit: WritableSignal<boolean> = signal(false);
  icons = {
    X,
    LoaderCircle
  };
  fetchSubs: Subscription = new Subscription();
  form: FormGroup = this._FormBuilder.group({
    id: [null],
    roomNumber: [null, Validators.required],
    roomType: ["1", Validators.required],
    pricePerNight: [null, Validators.required],
  });

  //Logics

  ngOnInit() {
    if (!this.isAdd) {
      this.title.set("Update Room");
      this.onUpdate(this.roomId);
    }
  }

  submit(): void {
    if (this.form.invalid) {
      this.form.markAllAsDirty();
      this.form.markAllAsTouched();
      return;
    }
    this.isSubmit.set(true);
    if (this.isAdd) {
      this.fetchSubs.add(
        this._RoomService.AddRoom(this.form.value).subscribe({
          next: (res: IActionStatusDto) => {
            this.isSubmit.set(false);
            this._ToastService.showSuccess(res.message);
            this.refreshGrid.emit()
            this.closeModal();
          },
          error: (err: HttpErrorResponse) => {
            this.isSubmit.set(false);
            console.log(err);
            this._ToastService.showError(err.error.details);
          }
        })
      );
    }
    else {
      this.fetchSubs.add(
        this._RoomService.UpdateRoom(this.form.value).subscribe({
          next: (res: IActionStatusDto) => {
            this.isSubmit.set(false);
            this._ToastService.showSuccess(res.message);
            this.refreshGrid.emit()
            this.closeModal();
          },
          error: (err: HttpErrorResponse) => {
            this.isSubmit.set(false);
            console.log(err);
            this._ToastService.showError(err.error.details);
          }
        })
      );
    }
  }

  private onUpdate(id: string): void {
    this.fetchSubs.add(
      this._RoomService.GetRoomById(id).subscribe({
        next: (res: IRoomToReturnDto) => {
          this.form.patchValue({
            id: res.id,
            roomNumber: res.roomNumber,
            roomType: res.roomTypeId,
            pricePerNight: res.pricePerNight
          })
        },
        error: (err: HttpErrorResponse) => {
          console.log(err);
          this._ToastService.showError("Couldn't Load Room Data")
        }
      })
    );
  }

  closeModal(): void {
    this.close.emit("");
  }
  ngOnDestroy(): void {
    this.fetchSubs.unsubscribe();
  }
}
