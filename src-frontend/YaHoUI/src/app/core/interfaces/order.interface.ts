import { IProductInfo } from './product.interface';
import { ICustomerInfo } from './customer.interface';
import { IDeliveryInfo } from './delivery.interface';

export interface ICustomerOrderInfo {
    orderId: number;
    initialDate: Date;
    deliveryPlace: string;
    deliveryDate?: Date;
    expectedDate: Date;
    customerId: number;
    deliveryCharge: number;
    title: string;
    comment: string;
    deliveryFrom: string;
    orderStatus: string;
    products: IProductInfo[];
    orderRequests: IOrderRequest[];
}

export interface IDeliveryOrderInfo {
    orderId: number;
    initialDate: Date;
    deliveryPlace: string;
    deliveryDate?: Date;
    expectedDate: Date;
    customerId: number;
    deliveryCharge: number;
    title: string;
    comment: string;
    deliveryFrom: string;
    orderStatus: string;
    customer: ICustomerInfo;
}

export interface IOrderRequest {
    orderRequestId: number;
    orderId: number;
    deliveryId: number;
    delivery?: IDeliveryInfo;
    approved?: boolean;
    initialDate: string;
}
