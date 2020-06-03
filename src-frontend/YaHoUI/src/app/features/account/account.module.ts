import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AccountRoutingModule } from './account-routing.module';
import { AccountInfoComponent } from './account-info/account-info.component';
import { UpdateUserComponent } from './update-user/update-user.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { LiqPayComponent } from './liq-pay/liq-pay.component';


@NgModule({
  declarations: [AccountInfoComponent, UpdateUserComponent, LiqPayComponent],
  imports: [
    CommonModule,
    AccountRoutingModule,
    SharedModule
  ]
})
export class AccountModule { }
