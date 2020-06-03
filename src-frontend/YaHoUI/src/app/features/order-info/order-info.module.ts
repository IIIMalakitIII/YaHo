import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { OrderInfoRoutingModule } from './order-info-routing.module';
import { DeliveryOrdersComponent } from './delivery-orders/delivery-orders.component';
import { CustomerOrdersComponent } from './customer-orders/customer-orders.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { CreateOrderComponent } from './create-order/create-order.component';


@NgModule({
  declarations: [DeliveryOrdersComponent, CustomerOrdersComponent, CreateOrderComponent],
  imports: [
    CommonModule,
    OrderInfoRoutingModule,
    SharedModule
  ]
})
export class OrderInfoModule { }
