import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { urls } from 'src/app/core/extension/urls';
import { IUserInfo } from 'src/app/core/interfaces/user.interface';
import { ICustomerReview } from 'src/app/core/interfaces/customer.interface';
import { IDeliveryReview } from 'src/app/core/interfaces/delivery.interface';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  constructor(private http: HttpClient) { }

  getMyInfo(): Observable<IUserInfo> {
    return this.http.get<IUserInfo>(urls.GET_MY_INFO());
  }

  getCustomerReviews(id: number): Observable<ICustomerReview[]> {
    return this.http.get<ICustomerReview[]>(urls.GET_CUSTOMER_REWIEWS(id));
  }

  getDeliveryReviews(id: number): Observable<IDeliveryReview[]> {
    return this.http.get<IDeliveryReview[]>(urls.GET_DELIVERY_REVIEWS(id));
  }

  getLiqPayConfig(money) {
    return this.http.post<any>(urls.POST_GO_TO_LIQ_PAY(money), {});
  }

  updateUserInfo(model) {
    return this.http.put(urls.PUT_UPDATE_USER(), model);
  }
}
