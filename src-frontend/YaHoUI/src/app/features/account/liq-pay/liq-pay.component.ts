import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subject } from 'rxjs';
import { AccountService } from '../common/account.api.service';
import { ToastrService } from 'ngx-toastr';
import { takeUntil } from 'rxjs/operators';
import { toastrTitle } from 'src/app/core/helpers';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-liq-pay',
  templateUrl: './liq-pay.component.html',
  styleUrls: ['./liq-pay.component.css']
})
export class LiqPayComponent implements OnInit, OnDestroy {

  isLinear = true;
  money: number;
  config: any;
  firstFormGroup: FormGroup;

  private destroy$ = new Subject<void>();
  constructor(private accountService: AccountService,
              private toastr: ToastrService,
              private formBuilder: FormBuilder,
              private route: ActivatedRoute) {}



  ngOnInit(): void {
    this.firstFormGroup = this.formBuilder.group({
      money: [null, Validators.required]
    });
  }

  getLiqPayConfig() {
    this.accountService.getLiqPayConfig(this.firstFormGroup.get('money').value)
      .pipe(takeUntil(this.destroy$))
      .subscribe(
        res => {
          this.config = res;
        },
        err => {
          this.toastr.error(err.error.error, toastrTitle.Error);
          window.location.reload();
        }
      );
  }

  resetPay() {
    this.config = null;
  }

  onSubmit(form: any, e: any): void {
    e.target.submit();
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

}
