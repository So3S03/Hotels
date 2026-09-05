import { Component, inject, OnDestroy, OnInit, PLATFORM_ID, signal, WritableSignal } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { ArrowRight, LoaderCircle, LucideAngularModule } from 'lucide-angular';
import { Subscription } from 'rxjs';
import { AuthService } from '../../../Core/Services/AuthServices/Auth.service';
import { ISignInStatusDto } from '../../../Core/Interfaces/AuthModule/ISignInStatusDto';
import { HttpErrorResponse } from '@angular/common/http';
import { isPlatformBrowser } from '@angular/common';
import { jwtDecode } from 'jwt-decode';
import { ToastService } from '../../../Core/Services/ToastServices/Toast.service';

@Component({
  selector: 'app-Login',
  templateUrl: './Login.component.html',
  styleUrls: ['./Login.component.css'],
  standalone: true,
  imports: [ReactiveFormsModule, RouterLink, LucideAngularModule],
  host: { class: 'flex justify-center w-full' }
})
export class LoginComponent implements OnInit, OnDestroy {
  //DI Container
  private readonly _FormBuilder: FormBuilder = inject(FormBuilder);
  private readonly _AuthService: AuthService = inject(AuthService);
  private readonly _ToastService: ToastService = inject(ToastService);
  private readonly _Router: Router = inject(Router);
  private readonly _PLATFORM_ID = inject(PLATFORM_ID);

  //Common Vars
  fetchSubs: Subscription = new Subscription();
  icons = {
    ArrowRight,
    LoaderCircle
  }
  form!: FormGroup;
  isLoading: WritableSignal<boolean> = signal(false);

  //Logic
  ngOnInit() {
    this.initForm();
  }

  autoFill(): void {
    this.form.patchValue({
      email: "Admin@hotels.com",
      password: "@Admin@1111"
    });
    this.form.markAllAsTouched();
  }

  signIn(): void {
    if (this.form.invalid) {
      this.form.markAllAsDirty();
      this.form.markAllAsTouched();
      return;
    }
    this.isLoading.set(true);
    this.fetchSubs.add(
      this._AuthService.Login(this.form.value).subscribe({
        next: (res: ISignInStatusDto) => {
          this.isLoading.set(false);
          if(isPlatformBrowser(this._PLATFORM_ID))
            {
              localStorage.setItem("token", res.token);
              var decodedToken = jwtDecode(res.token);
              localStorage.setItem("userData", JSON.stringify(decodedToken));
            }
            this._Router.navigate(["/Rooms"]);
          },
          error: (err: HttpErrorResponse) => {
          this.isLoading.set(false);
          console.log(err);
          this._ToastService.showError(err.error.details)
        }
      })
    )
  }

  private initForm(): void {
    this.form = this._FormBuilder.group({
      email: [null, [Validators.required, Validators.email]],
      password: [null, Validators.required]
    })
  }

  ngOnDestroy(): void {
    this.fetchSubs.unsubscribe();
  }
}
