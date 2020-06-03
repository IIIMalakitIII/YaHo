import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { urls } from 'src/app/core/extension/urls';
import { IDeliveryOrderInfo, ICustomerOrderInfo, IOrderRequest } from 'src/app/core/interfaces/order.interface';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  constructor(private http: HttpClient) { }



  getMyOrderLikeCustomer(): Observable<ICustomerOrderInfo[]> {
    return this.http.get<ICustomerOrderInfo[]>(urls.GET_MY_ORDER_LIKE_CUSTOMER());
  }

  getMyOrderLikeDelivery(): Observable<IDeliveryOrderInfo[]> {
    return this.http.get<IDeliveryOrderInfo[]>(urls.GET_MY_ORDER_LIKE_DELIVERY());
  }

  getOrderRequests(orderId: number): Observable<IOrderRequest[]> {
    return this.http.get<IOrderRequest[]>(urls.GET_ORDER_REQUEST(orderId));
  }

  rejectThisRequest(requestId: number) {
    return this.http.put(urls.PUT_REJECT_ORDER_REQUEST(), requestId);
  }

  approveThisRequest(requestId: number) {
    return this.http.put(urls.PUT_APPROVE_ORDER_REQUEST(), {requestId});
  }

  deleteOrderRequestLikeCustomer(requestId: number) {
    return this.http.delete(urls.DELETE_ORDER_REQUEST_LIKE_CUSTOMER() + requestId);
  }

  deleteOrderRequest(requestId: number) {
    return this.http.delete(urls.DELETE_MY_ORDER_REQUEST() + requestId);
  }

}
