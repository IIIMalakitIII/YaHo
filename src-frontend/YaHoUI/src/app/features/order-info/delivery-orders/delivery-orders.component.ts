import { Component, OnInit, OnDestroy } from '@angular/core';
import { IUser } from 'src/app/core/interfaces/user.interface';
import { ICustomerOrderInfo, IOrderRequest, IDeliveryOrderInfo } from 'src/app/core/interfaces/order.interface';
import { Subject } from 'rxjs';
import { OrderService } from '../common/order.api.service';
import { MatDialog } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute } from '@angular/router';
import { FormBuilder } from '@angular/forms';
import { AuthenticationService } from '../../authentication/authentication.service';
import { takeUntil, finalize } from 'rxjs/operators';
import { toastrTitle } from 'src/app/core/helpers';

@Component({
  selector: 'app-delivery-orders',
  templateUrl: './delivery-orders.component.html',
  styleUrls: ['./delivery-orders.component.css']
})
export class DeliveryOrdersComponent implements OnInit, OnDestroy {

  currentUser: IUser;
  orders: IDeliveryOrderInfo[];
  orderRequests: IOrderRequest[];
  actualOrderId: number;


  private destroy$ = new Subject<void>();
  constructor(private orderService: OrderService,
              public dialog: MatDialog,
              private toastr: ToastrService,
              private route: ActivatedRoute,
              private formBuilder: FormBuilder,
              private authService: AuthenticationService) {
    this.authService.currentUser.subscribe(x => this.currentUser = x);
  }

  ngOnInit(): void {
    this.getMyOrders();
  }

  getMyOrders(): void {
    this.orderService.getMyOrderLikeDelivery()
      .pipe(takeUntil(this.destroy$))
      .subscribe(
        res => this.orders = res,
        err => this.toastr.error(err.error.error, toastrTitle.Error)
      );
  }

  getOrderRequests(orderId: number): void {
    this.orderRequests = null;
    this.actualOrderId = orderId;
    this.orderService.getOrderRequests(orderId)
      .pipe(takeUntil(this.destroy$))
      .subscribe(
        res => this.orderRequests = res,
        err => this.toastr.error(err.error.error, toastrTitle.Error)
      );
  }

  approveThisRequest(requestId: number): void {
    this.orderService.approveThisRequest(requestId)
      .pipe(
        finalize(() => {
          this.getMyOrders();
          this.getOrderRequests(this.actualOrderId);
        }),
        takeUntil(this.destroy$))
      .subscribe(
        () => this.toastr.success(`This request approved`, toastrTitle.Success),
        err => this.toastr.error(err.error.error, toastrTitle.Error)
      );
  }

  rejectThisRequest(requestId: number): void {
    this.orderService.rejectThisRequest(requestId)
      .pipe(
        finalize(() => {
          this.getOrderRequests(this.actualOrderId);
        }),
        takeUntil(this.destroy$))
      .subscribe(
        () => this.toastr.success(`This request rejected`, toastrTitle.Success),
        err => this.toastr.error(err.error.error, toastrTitle.Error)
      );
  }

  deleteThisRequest(requestId: number): void {
    this.orderService.deleteOrderRequestLikeCustomer(requestId)
    .pipe(
      finalize(() => {
        this.getOrderRequests(this.actualOrderId);
      }),
      takeUntil(this.destroy$))
    .subscribe(
      () => this.toastr.success(`This request deleted`, toastrTitle.Success),
      err => this.toastr.error(err.error.error, toastrTitle.Error)
    );
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
