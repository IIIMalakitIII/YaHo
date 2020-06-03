import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CustomerOrdersComponent } from './customer-orders/customer-orders.component';
import { DeliveryOrdersComponent } from './delivery-orders/delivery-orders.component';


const routes: Routes = [
  { path: '', redirectTo: 'info', pathMatch: 'full'},
  { path: 'customer-orders', component: CustomerOrdersComponent},
  { path: 'delivery-orders', component: DeliveryOrdersComponent},
];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class OrderInfoRoutingModule { }
