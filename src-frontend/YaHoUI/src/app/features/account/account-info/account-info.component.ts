import { Component, OnInit, OnDestroy } from '@angular/core';
import { IUserInfo, IUser } from 'src/app/core/interfaces/user.interface';
import { Subject } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../common/account.api.service';
import { takeUntil, finalize } from 'rxjs/operators';
import { toastrTitle, configureToastr, MustMatch } from 'src/app/core/helpers';
import { AuthenticationService } from '../../authentication/authentication.service';
import { IDeliveryReview } from 'src/app/core/interfaces/delivery.interface';
import { ICustomerReview } from 'src/app/core/interfaces/customer.interface';
import { UpdateUserComponent } from '../update-user/update-user.component';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-account-info',
  templateUrl: './account-info.component.html',
  styleUrls: ['./account-info.component.css']
})
export class AccountInfoComponent implements OnInit, OnDestroy {

  userInfo: IUserInfo;
  currentUser: IUser;
  passwordForm: FormGroup;

  customerReviews: ICustomerReview[];
  deliveryReviews: IDeliveryReview[];

  private destroy$ = new Subject<void>();
  constructor(private accountService: AccountService,
              public dialog: MatDialog,
              private toastr: ToastrService,
              private route: ActivatedRoute,
              private formBuilder: FormBuilder,
              private authService: AuthenticationService) {
    this.authService.currentUser.subscribe(x => this.currentUser = x);
  }


  ngOnInit(): void {
    this.createForm();
    configureToastr(this.toastr);
    this.getMyInfo();
  }

  getMyInfo(): void {
    this.accountService.getMyInfo()
      .pipe(takeUntil(this.destroy$))
      .subscribe(
        res => this.userInfo = res,
        err => {
          this.toastr.error(err.error.error, toastrTitle.Error);
          window.history.back();
        }
      );
  }

  getCustomerReviews(): void {
    this.accountService.getCustomerReviews(this.currentUser.customer)
      .pipe(takeUntil(this.destroy$))
      .subscribe(
        res => this.customerReviews = res,
        err => {
          this.toastr.error(err.error.error, toastrTitle.Error);
          window.history.back();
        }
      );
  }

  getDeliveryReviews(): void {
    this.accountService.getDeliveryReviews(this.currentUser.delivery)
      .pipe(takeUntil(this.destroy$))
      .subscribe(
        res => this.deliveryReviews = res,
        err => {
          this.toastr.error(err.error.error, toastrTitle.Error);
          window.history.back();
        }
      );
  }

  openDialogUpdateUser(): void {
    const matDialogConfig = new MatDialogConfig();
    matDialogConfig.width = '500px';
    const dialogRef = this.dialog.open(UpdateUserComponent, matDialogConfig);
    dialogRef.afterClosed()
    .pipe(
      finalize(() => this.getMyInfo()),
      takeUntil(this.destroy$)
    )
    .subscribe(() => { });
  }

  get f() { return this.passwordForm.controls; }

  get currentPassword() {
    return this.passwordForm.controls.currentPassword;
  }

  get newPassword() {
    return this.passwordForm.controls.newPassword;
  }

  get confirmPassword() {
    return this.passwordForm.controls.confirmPassword;
  }

  createForm(): void  {
    this.passwordForm = this.formBuilder.group({
      currentPassword: [null, Validators.required],
      newPassword: [null, [Validators.required, Validators.minLength(6), Validators.maxLength(30)]],
      confirmPassword: [null, Validators.required]
    }, {
      validator: MustMatch('newPassword', 'confirmPassword')
    });
  }

  onSubmit(): void {
    if (this.passwordForm.valid) {
      this.changePassword();
    } else {
      this.passwordForm.markAllAsTouched();
    }
  }

  changePassword(): void {
    this.authService.changePassword(this.passwordForm.value)
      .pipe(takeUntil(this.destroy$))
      .subscribe(
        () => {
          this.resetForm();
          this.toastr.success(toastrTitle.Success);
        },
        err => {
          this.toastr.error(err.error.error, toastrTitle.Error);
        }
      );
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  resetForm(): void {
    this.passwordForm.reset();
  }

  hasCustomError = (form: FormGroup, control: string): boolean =>
    form.get(`${control}`).invalid && (form.get(`${control}`).dirty || form.get(`${control}`).touched)


}
