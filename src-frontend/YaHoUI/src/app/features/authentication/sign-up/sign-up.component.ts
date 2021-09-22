import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { IUser } from 'src/app/core/interfaces/user.interface';
import { Subject } from 'rxjs';
import { Router } from '@angular/router';
import { AuthenticationService } from '../authentication.service';
import { ToastrService } from 'ngx-toastr';
import { configureToastr, toastrTitle, MustMatch } from 'src/app/core/helpers';
import { takeUntil } from 'rxjs/operators';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit, OnDestroy {

  createForm: FormGroup;
  loading = false;
  currentUser: IUser;

  private destroy$ = new Subject<void>();
  constructor(private formBuilder: FormBuilder,
              private router: Router,
              private authService: AuthenticationService,
              private toastr: ToastrService) {
    this.authService.currentUser.subscribe(x => this.currentUser = x);
  }

  ngOnInit(): void {
    if (this.currentUser){
      this.router.navigate(['/main']);
    }
    this.createFormGroup();
    configureToastr(this.toastr);
  }

  get f() { return this.createForm.controls; }

  onSubmit(): void {
    if (this.createForm.valid) {
      this.createUserAccount();
    } else {
      this.createForm.markAllAsTouched();
    }
  }

  createUserAccount(): void {
    this.authService.registration(this.createForm.value)
      .pipe(takeUntil(this.destroy$))
      .subscribe(
        () => {
          this.router.navigate(['/auth']);
          this.toastr.success(`Account created`, toastrTitle.Success);
        },
        () => {
          this.toastr.error(`Something is wrong`, toastrTitle.Error);
        }
      );
  }

  createFormGroup(): void  {
    this.createForm = this.formBuilder.group({
      firstName: [null, Validators.required],
      lastName: [null, Validators.required],
      phoneNumber: [null, Validators.required],
      description: [null],
      password: [null, [Validators.required, Validators.minLength(6), Validators.maxLength(30)]],
      email: [null, [Validators.required, Validators.email]],
      confirmPassword: [null, [Validators.required]]
    }, {
      validator: MustMatch('password', 'confirmPassword')
    });
  }

  resetForm(): void {
    this.createForm.reset();
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  hasCustomError = (form: FormGroup, control: string): boolean =>
    form.get(`${control}`).invalid && (form.get(`${control}`).dirty || form.get(`${control}`).touched)

  hasPatternError = (form: FormGroup, control: string): boolean =>
    (form.get(`${control}`).invalid &&  form.get(`${control}`).dirty)

}
