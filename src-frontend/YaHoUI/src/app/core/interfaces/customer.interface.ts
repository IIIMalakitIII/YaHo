import { IUserPartialInfo } from './user.interface';

export interface ICustomerInfo {
    customerId: number;
    userId: string;
    description: string;
    rating: number;
    totalReviewCount: number;
}

export interface ICustomerReview {
    reviewId: number;
    description: string;
    userId: string;
    customerId: number;
    mark: number;
    user: IUserPartialInfo;
}
