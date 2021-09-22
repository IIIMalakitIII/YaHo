import { IDeliveryInfo } from './delivery.interface';
import { ICustomerInfo } from './customer.interface';

export interface IUser {
    token?: string;
    id: string;
    customer: number;
    delivery: number;
    email: string;
    lastName: string;
    firstName: string;
}

export interface IUserInfo {
    balance: number;
    hold: number;
    initialDate: Date;
    customer: ICustomerInfo;
    delivery: IDeliveryInfo;
    id: string;
    email: string;
    firstName: string;
    lastName: string;
    phoneNumber: string;
    description: string;
    telegramId: number;
}

export interface IUserPartialInfo {
    id: string;
    firstName: string;
    lastName: string;
    description: string;
}
