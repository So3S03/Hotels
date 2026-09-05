import { CommonModule } from '@angular/common';
import { Component, inject, OnDestroy, OnInit, signal, WritableSignal } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ReactiveFormsModule, ValidationErrors, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { CircleCheck, LoaderCircle, LucideAngularModule } from 'lucide-angular';
import { Subscription } from 'rxjs';
import { AuthService } from '../../../Core/Services/AuthServices/Auth.service';
import { ToastService } from '../../../Core/Services/ToastServices/Toast.service';
import { HttpErrorResponse } from '@angular/common/http';
import { IActionStatusDto } from '../../../Core/Interfaces/_Common/IActionStatusDto';

@Component({
  selector: 'app-Register',
  templateUrl: './Register.component.html',
  styleUrls: ['./Register.component.css'],
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink, LucideAngularModule],
  host: { class: 'flex justify-center w-full' }
})
export class RegisterComponent implements OnInit, OnDestroy {
  //DI Container
  private readonly _AuthService: AuthService = inject(AuthService);
  private readonly _ToastService: ToastService = inject(ToastService);
  private readonly _FormBuilder: FormBuilder = inject(FormBuilder);
  private readonly _Router: Router = inject(Router);

  //Vars
  fetchSubs: Subscription = new Subscription();
  form!: FormGroup;
  phoneNumberPattern: RegExp = /^(?:\+20|0020|0)?01[0125][0-9]{8}$/;
  passwordPattern: RegExp = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z0-9]).{8,}/;
  isLoading: WritableSignal<boolean> = signal(false);
  icons = {
    CircleCheck,
    LoaderCircle
  }

  //Logics
  ngOnInit() {
    this.initForm();
  }

  private initForm(): void
  {
    this.form = this._FormBuilder.group({
      fullName: [null, Validators.required],
      email: [null, [Validators.required, Validators.email]],
      userName: [null, Validators.required],
      phoneNumber: [null, [Validators.required, Validators.pattern(this.phoneNumberPattern)]],
      password: [null, [Validators.required, Validators.pattern(this.passwordPattern)]],
      rePassword: [null, Validators.required],
    }, {validators : this.validRePasswordWithPassword})
  }

  private validRePasswordWithPassword(control: AbstractControl) : ValidationErrors | null
  {
    const password = control.get("password");
    const rePassword = control.get("rePassword");
    if(password?.value != rePassword?.value)
    {
      return {
        "MissMatch": true
      }
    }
    return null
  }

  Register(): void
  {
    if(this.form.invalid)
    {
      this.form.markAllAsDirty();
      this.form.markAllAsTouched();
      return;
    }
    this.isLoading.set(true);
    this.fetchSubs.add(
      this._AuthService.Register(this.form.value).subscribe({
        next: (res: IActionStatusDto) => {
          this.isLoading.set(false);
          this._ToastService.showSuccess(`${res.message}\n You will be directed to login page!`);
          setTimeout(() => {
            this._Router.navigate(["/Login"])
          }, 1500)
        },
        error: (err: HttpErrorResponse) => {
          this.isLoading.set(false);
          console.log(err);
          this._ToastService.showError(err.error.details);
        }
      })
    )
  }
  ngOnDestroy(): void {
    this.fetchSubs.unsubscribe();
  }
}
