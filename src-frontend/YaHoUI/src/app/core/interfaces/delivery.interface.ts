import { IUserPartialInfo } from './user.interface';

export interface IDeliveryInfo {
    deliveryId: number;
    userId: string;
    description: string;
    rating: number;
    totalReviewCount: number;
}

export interface IDeliveryReview {
    reviewId: number;
    description: string;
    userId: string;
    deliveryId: number;
    mark: number;
    user: IUserPartialInfo;
}

