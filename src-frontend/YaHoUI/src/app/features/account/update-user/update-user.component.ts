import { Component, OnInit, OnDestroy } from '@angular/core';
import { toastrTitle, configureToastr } from 'src/app/core/helpers';
import { IUserInfo, IUser } from 'src/app/core/interfaces/user.interface';
import { Subject } from 'rxjs';
import { AccountService } from '../common/account.api.service';
import { MatDialogRef } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { AuthenticationService } from '../../authentication/authentication.service';
import { takeUntil, finalize } from 'rxjs/operators';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-update-user',
  templateUrl: './update-user.component.html',
  styleUrls: ['./update-user.component.css']
})
export class UpdateUserComponent implements OnInit, OnDestroy {

  userInfo: IUserInfo;
  currentUser: IUser;
  updateForm: FormGroup;
  loading = false;

  private destroy$ = new Subject<void>();
  constructor(private accountService: AccountService,
              private toastr: ToastrService,
              private formBuilder: FormBuilder,
              public dialogRef: MatDialogRef<any>,
              private authService: AuthenticationService) {
    this.authService.currentUser.subscribe(x => this.currentUser = x);
  }

  ngOnInit(): void {
    configureToastr(this.toastr);
    this.getMyInfo();
  }

  getMyInfo(): void {
    this.accountService.getMyInfo()
      .pipe(takeUntil(this.destroy$))
      .subscribe(
        res => {
          this.createFormGroup(res);
          this.userInfo = res;
        },
        err => {
          this.toastr.error(err, toastrTitle.Error);
          window.history.back();
        }
      );
  }

  get f() { return this.updateForm.controls; }

  createFormGroup(model: IUserInfo): void  {
    this.updateForm = this.formBuilder.group({
      firstName: [model.firstName, Validators.required],
      lastName: [model.lastName, Validators.required],
      phoneNumber: [model.phoneNumber, Validators.required],
      description: [model.description],
    });
  }

  onSubmit(): void {
    if (this.updateForm.valid) {
      this.loading = true;
      this.updateUserInfo();
    } else {
      this.updateForm.markAllAsTouched();
    }
  }

  updateUserInfo(): void {
    this.accountService.updateUserInfo(this.updateForm.value)
      .pipe(
        finalize(() => this.loading = true),
        takeUntil(this.destroy$))
      .subscribe(
        () => {
          this.toastr.success(`Update success`, toastrTitle.Success);
          this.dialogRef.close();
        },
        () => {
          this.toastr.error(`Something is wrong`, toastrTitle.Error);
        }
      );
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  hasCustomError = (form: FormGroup, control: string): boolean =>
    form.get(`${control}`).invalid && (form.get(`${control}`).dirty || form.get(`${control}`).touched)

}
