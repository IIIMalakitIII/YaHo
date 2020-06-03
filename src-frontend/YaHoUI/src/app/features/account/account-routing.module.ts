import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AccountInfoComponent } from './account-info/account-info.component';
import { LiqPayComponent } from './liq-pay/liq-pay.component';


const routes: Routes = [
  { path: '', redirectTo: 'info', pathMatch: 'full'},
  { path: 'info', component: AccountInfoComponent},
  { path: 'liq-pay', component: LiqPayComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AccountRoutingModule { }
