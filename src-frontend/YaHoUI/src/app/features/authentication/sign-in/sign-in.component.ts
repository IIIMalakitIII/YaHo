import { Component, OnInit, OnDestroy } from '@angular/core';
import { Validators, FormGroup, FormBuilder } from '@angular/forms';
import { Subject } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from '../authentication.service';
import { ToastrService } from 'ngx-toastr';
import { configureToastr, toastrTitle } from 'src/app/core/helpers';
import { finalize, first } from 'rxjs/operators';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']
})
export class SignInComponent implements OnInit, OnDestroy {

  loginForm: FormGroup;
  loading = false;
  returnUrl: string;

  private destroy$ = new Subject<void>();
  constructor(private formBuilder: FormBuilder,
              private route: ActivatedRoute,
              private router: Router,
              private authenticationService: AuthenticationService,
              public toastr: ToastrService) {
    if (this.authenticationService.currentUserValue) {
      this.router.navigate(['/']);
    }
  }

  ngOnInit() {
    configureToastr(this.toastr);
    this.generateForm();
    this.returnUrl = this.route.snapshot.queryParams.returnUrl || '/';
  }

  generateForm() {
    this.loginForm = this.formBuilder.group(
      {
        email: [null, [Validators.required, Validators.email]],
        password: [null, Validators.required]
      }
    );
  }

  get f() { return this.loginForm.controls; }

  onSubmit() {
    if (this.loginForm.valid) {
    this.loading = true;
    this.authenticationService.login(this.loginForm)
    .pipe(
      finalize(() => {
        this.loading = false;
      }),
      first())
    .subscribe(
        () => {
          this.toastr.success(toastrTitle.Success);
          this.router.navigate(['/']);
        },
        () => {
          this.toastr.error(`Something is wrong`, toastrTitle.Error);
        });
    } else {
      this.loginForm.markAllAsTouched();
    }
  }

  hasCustomError = (form: FormGroup, control: string): boolean =>
    form.get(`${control}`).invalid && (form.get(`${control}`).dirty || form.get(`${control}`).touched)

  hasPatternError = (form: FormGroup, control: string): boolean =>
    (form.get(`${control}`).invalid && form.get(`${control}`).dirty)

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

}
