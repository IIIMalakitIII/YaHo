import { environment } from 'src/environments/environment';

export const apiUrl = environment.apiUrl;
// tslint:disable-next-line:no-namespace
export namespace urls {
    export enum ACCOUNT {
        POST_SIGN_IN = `/sign-in/`,
        POST_SIGN_UP = `/sign-up/`,
        PUT_CHANGE_PASSWORD = `/change-password/`,
        PUT_UPDATE_USER = `/update-user/`,
        PUT_USER_TELEGRAMID = `/update-user-telegramId/`,
        POST_GO_TO_LIQ_PAY = `/go-to-liq-pay/`,
        POST_LIQ_PAY_RESULT = `/liq-pay-result/`
    }

    export enum CONFIRM {
        POST_CHANGE_DELIVERY_CHARGE = `/confirm-change-delivery-charge/`,
        GET_CONFIRMS_DELIVERY_CHARGE = `/confirms-delivery-charge/`,
        PUT_UPDATE_CONFIRM_DELIVERY_CHARGE = `/update-confirm-delivery-charge/`,
        DELETE_CONFIRM_DELIVERY_CHARGE = `/delete-confirm-delivery-charge/`,
        POST_CHANGE_EXPECTED_DATE_LIKE_CUSTOMER = `/confirm-change-expected-date-like-customer/`,
        POST_CHANGE_EXPECTED_DATE_LIKE_DELIVERY = `/confirm-change-expected-date-like-delivery/`,
        GET_CONFIRMS_DELIVERY_DATE = `/confirms-expected-date/`,
        PUT_CONFIRM_EXPECTED_DATE_LIKE_CUSTOMER = `/update-confirm-expected-date-like-customer/`,
        PUT_CONFIRM_EXPECTED_DATE_LIKE_DELIVERY = `/update-confirm-expected-date-like-delivery/`,
        DELETE_CONFIRM_EXPECTED_DATE = `/delete-confirm-expected-date/`,
        POST_CONFIRM_CHANGE_ORDER_STATUS_LIKE_CUSTOMER = `/confirm-change-order-status-like-customer/`,
        POST_CONFIRM_CHANGE_ORDER_STATUS_LIKE_DELIVERY = `/confirm-change-order-status-like-delivery/`,
        GET_CONFIRMS_ORDER_STATUS = `/confirms-order-status/`,
        PUT_UPDATE_CONFIRM_ORDER_STATUS_LIKE_CUSTOMER = `/update-confirm-order-status-like-customer/`,
        PUT_UPDATE_CONFIRM_ORDER_STATUS_LIKE_DELIVERY = `/update-confirm-order-status-like-delivery/`,
        DELETE_CONFIRM_ORDER_STATUS = `/delete-confirm-order-status/`
    }

    export enum CUSTOMER {
        PUT_CUSTOMERS = `/`,
        GET_CUSTOMERS = `/`,
        GET_MY_CUSTOMER_INFO = `/my-customer-info/`,
        GET_CUSTOMER_INFO_BY_USER_ID = `/customer-info-by-user-id/`,
        GET_ALL_CUSTOMERS = `/AllCustomers/`
    }

    export enum CUSTOMER_REVIEW {
        POST_CUSTOMER_REWIEWS = `/`,
        GET_CUSTOMER_REWIEWS = `/get-customer-reviews/`
    }

    export enum DELIVERY {
        PUT_DELIVERIES = `/`,
        GET_DELIVERIES = `/`,
        GET_MY_DELIVERY_INFO = `/my-delivery-info/`,
        GET_DELIVERY_BY_USER_ID = `/delivery-info-by-user-id/`,
        GET_ALL_DELIVERIES = `/AllDeliveries/`
    }

    export enum DELIVERY_REVIEW {
        POST_DELIVERY_REVIEWS = `/`,
        GET_DELIVERY_REVIEWS = `/get-delivery-reviews/`
    }

    export enum MEDIA {
        POST_MEDIA = `/`,
        DELETE_BY_MEDIA_ID_AND_PRODUCT_ID = `/`
    }

    export enum ORDER {
        POST_CREATE_ORDER = `/create-order/`,
        GET_ORDER_LIST = `/order-list/`,
        GET_ORDER_BY_ID = `/order-by-id/`,
        GET_MY_ORDER_LIKE_CUSTOMER = `/my-order-like-customer/`,
        GET_MY_ORDER_LIKE_DELIVERY = `/my-order-like-delivery/`,
        PUT_UPDATE_ORDER_INFO = `/update-order-info/`,
        PUT_UPDATE_ORDER_STATUS = `/update-order-status/`,
        DELETE_ORDER = `/delete-order/`
    }

    export enum ORDER_REQUEST {
        POST_CREATE_ORDER_REQUEST = `/create-order-request/`,
        PUT_APPROVE_ORDER_REQUEST = `/approve-order-request/`,
        PUT_REJECT_ORDER_REQUEST = `/reject-order-request/`,
        DELETE_MY_ORDER_REQUEST = `/delete-my-order-request/`,
        DELETE_ORDER_REQUEST_LIKE_CUSTOMER = `/delete-order-request-like-customer`,
        GET_MY_REQUEST = `/get-my-request/`,
        GET_ORDER_REQUEST = `/get-order-request/`,
        DELETE_REJECT_ALL_ORDER_REQUEST = `/reject-all-order-request/`
    }
    export enum PRODUCT {
        POST_PRODUCTS = `/`,
        GET_PRODUCTS_BY_ORDER_ID = `/products-by-order-id/`,
        GET_PRODUCT_BY_ID = `/product-by-id/`,
        PUT_PRODUCT_BY_ID = `/product-by-id/`,
        DELETE_PRODUCT = `/delete-product/`
    }
    export enum USER_INFO {
        GET_USER_INFO = `/`,
        GET_ALL_USER = `/allUser/`,
        GET_MY_INFO = `/my-user-info/`
    }

    export enum ControlName {
        ACCOUNT = `Account`,
        CONFIRM = `Confirm`,
        CUSTOMER = `Customer`,
        CUSTOMER_REVIEW = `CustomerReview`,
        DELIVERY = `Delivery`,
        DELIVERY_REVIEW = `DeliveryReview`,
        MEDIA = `Media`,
        ORDER = `Order`,
        ORDER_REQUEST = `OrderRequest`,
        PRODUCT = `Product`,
        USER_INFO = `UserInfo`
    }

    // ACCOUNT
    export const POST_SIGN_IN = (): string => apiUrl + ControlName.ACCOUNT + ACCOUNT.POST_SIGN_IN;
    export const POST_SIGN_UP = (): string => apiUrl + ControlName.ACCOUNT + ACCOUNT.POST_SIGN_UP;
    export const PUT_CHANGE_PASSWORD = (): string => apiUrl + ControlName.ACCOUNT + ACCOUNT.PUT_CHANGE_PASSWORD;
    export const PUT_UPDATE_USER = (): string => apiUrl + ControlName.ACCOUNT + ACCOUNT.PUT_UPDATE_USER;
    export const PUT_USER_TELEGRAMID = (): string => apiUrl + ControlName.ACCOUNT + ACCOUNT.PUT_USER_TELEGRAMID;
    export const POST_GO_TO_LIQ_PAY = (money: number): string => apiUrl + ControlName.ACCOUNT + ACCOUNT.POST_GO_TO_LIQ_PAY + `?money=${money}`;
    export const POST_LIQ_PAY_RESULT = (): string => apiUrl + ControlName.ACCOUNT + ACCOUNT.POST_LIQ_PAY_RESULT;

    // CONFIRM
    export const POST_CHANGE_DELIVERY_CHARGE = (): string => apiUrl + ControlName.CONFIRM + CONFIRM.POST_CHANGE_DELIVERY_CHARGE;
    // tslint:disable-next-line:max-line-length
    export const GET_CONFIRMS_DELIVERY_CHARGE = (id: number): string => apiUrl + ControlName.CONFIRM + CONFIRM.GET_CONFIRMS_DELIVERY_CHARGE + `${id}`;
    // tslint:disable-next-line:max-line-length
    export const PUT_UPDATE_CONFIRM_DELIVERY_CHARGE = (): string => apiUrl + ControlName.CONFIRM + CONFIRM.PUT_UPDATE_CONFIRM_DELIVERY_CHARGE;
    // tslint:disable-next-line:max-line-length
    export const DELETE_CONFIRM_DELIVERY_CHARGE = (id: number): string => apiUrl + ControlName.CONFIRM + CONFIRM.DELETE_CONFIRM_DELIVERY_CHARGE + `${id}`;
    // tslint:disable-next-line:max-line-length
    export const POST_CHANGE_EXPECTED_DATE_LIKE_CUSTOMER = (): string => apiUrl + ControlName.CONFIRM + CONFIRM.POST_CHANGE_EXPECTED_DATE_LIKE_CUSTOMER;
    // tslint:disable-next-line:max-line-length
    export const POST_CHANGE_EXPECTED_DATE_LIKE_DELIVERY = (): string => apiUrl + ControlName.CONFIRM + CONFIRM.POST_CHANGE_EXPECTED_DATE_LIKE_DELIVERY;
    // tslint:disable-next-line:max-line-length
    export const GET_CONFIRMS_DELIVERY_DATE = (id: number): string => apiUrl + ControlName.CONFIRM + CONFIRM.GET_CONFIRMS_DELIVERY_DATE + `${id}`;
    // tslint:disable-next-line:max-line-length
    export const PUT_CONFIRM_EXPECTED_DATE_LIKE_CUSTOMER = (): string => apiUrl + ControlName.CONFIRM + CONFIRM.PUT_CONFIRM_EXPECTED_DATE_LIKE_CUSTOMER;
    // tslint:disable-next-line:max-line-length
    export const PUT_CONFIRM_EXPECTED_DATE_LIKE_DELIVERY = (): string => apiUrl + ControlName.CONFIRM + CONFIRM.PUT_CONFIRM_EXPECTED_DATE_LIKE_DELIVERY;
    // tslint:disable-next-line:max-line-length
    export const DELETE_CONFIRM_EXPECTED_DATE = (id: number): string => apiUrl + ControlName.CONFIRM + CONFIRM.DELETE_CONFIRM_EXPECTED_DATE + `${id}`;
    // tslint:disable-next-line:max-line-length
    export const POST_CONFIRM_CHANGE_ORDER_STATUS_LIKE_CUSTOMER = (): string => apiUrl + ControlName.CONFIRM + CONFIRM.POST_CONFIRM_CHANGE_ORDER_STATUS_LIKE_CUSTOMER;
    // tslint:disable-next-line:max-line-length
    export const POST_CONFIRM_CHANGE_ORDER_STATUS_LIKE_DELIVERY = (): string => apiUrl + ControlName.CONFIRM + CONFIRM.POST_CONFIRM_CHANGE_ORDER_STATUS_LIKE_DELIVERY;
    export const GET_CONFIRMS_ORDER_STATUS = (id: number): string => apiUrl + ControlName.CONFIRM + CONFIRM.GET_CONFIRMS_ORDER_STATUS + `${id}`;
    // tslint:disable-next-line:max-line-length
    export const PUT_UPDATE_CONFIRM_ORDER_STATUS_LIKE_CUSTOMER = (): string => apiUrl + ControlName.CONFIRM + CONFIRM.PUT_UPDATE_CONFIRM_ORDER_STATUS_LIKE_CUSTOMER;
    // tslint:disable-next-line:max-line-length
    export const PUT_UPDATE_CONFIRM_ORDER_STATUS_LIKE_DELIVERY = (): string => apiUrl + ControlName.CONFIRM + CONFIRM.PUT_UPDATE_CONFIRM_ORDER_STATUS_LIKE_DELIVERY;
    export const DELETE_CONFIRM_ORDER_STATUS = (id: number): string => apiUrl + ControlName.CONFIRM + CONFIRM.DELETE_CONFIRM_ORDER_STATUS + `${id}`;


    // CUSTOMER
    export const PUT_CUSTOMERS = (): string => apiUrl + ControlName.CUSTOMER + CUSTOMER.PUT_CUSTOMERS;
    export const GET_CUSTOMERS = (id: number): string => apiUrl + ControlName.CUSTOMER + CUSTOMER.GET_CUSTOMERS + `${id}`;
    export const GET_MY_CUSTOMER_INFO = (): string => apiUrl + ControlName.CUSTOMER + CUSTOMER.GET_MY_CUSTOMER_INFO;
    // tslint:disable-next-line:max-line-length
    export const GET_CUSTOMER_INFO_BY_USER_ID = (id: number): string => apiUrl + ControlName.CUSTOMER + CUSTOMER.GET_CUSTOMER_INFO_BY_USER_ID + `${id}`;
    export const GET_ALL_CUSTOMERS = (): string => apiUrl + ControlName.CUSTOMER + CUSTOMER.GET_ALL_CUSTOMERS;

    // DELIVERY
    export const PUT_DELIVERIES = (): string => apiUrl + ControlName.DELIVERY + DELIVERY.PUT_DELIVERIES;
    export const GET_DELIVERY_REVIEWS = (id: number): string => apiUrl + ControlName.DELIVERY + DELIVERY.GET_DELIVERIES + `${id}`;
    export const GET_MY_DELIVERY_INFO = (): string => apiUrl + ControlName.DELIVERY + DELIVERY.GET_MY_DELIVERY_INFO;
    // tslint:disable-next-line:max-line-length
    export const GET_DELIVERY_BY_USER_ID = (id: number): string => apiUrl + ControlName.DELIVERY + DELIVERY.GET_DELIVERY_BY_USER_ID + `${id}`;
    export const GET_ALL_DELIVERIES = (): string => apiUrl + ControlName.DELIVERY + DELIVERY.GET_ALL_DELIVERIES;


    // DELIVERY_REVIEW
    export const POST_DELIVERY_REVIEWS = (): string => apiUrl + ControlName.DELIVERY_REVIEW + DELIVERY_REVIEW.POST_DELIVERY_REVIEWS;
    export const GET_DELIVERIES = (id: number): string => apiUrl + ControlName.DELIVERY_REVIEW + DELIVERY_REVIEW.GET_DELIVERY_REVIEWS + `${id}`;


    // MEDIA
    export const POST_MEDIA = (): string => apiUrl + ControlName.MEDIA + MEDIA.POST_MEDIA;
    // tslint:disable-next-line:max-line-length
    export const DELETE_BY_MEDIA_ID_AND_PRODUCT_ID = (id: number, prodid: number): string => apiUrl + ControlName.MEDIA + MEDIA.DELETE_BY_MEDIA_ID_AND_PRODUCT_ID + `${id}` + `/` + `${prodid}`;


    // ORDER
    export const POST_CREATE_ORDER = (): string => apiUrl + ControlName.ORDER + ORDER.POST_CREATE_ORDER;
    export const GET_ORDER_LIST = (): string => apiUrl + ControlName.ORDER + ORDER.GET_ORDER_LIST;
    export const GET_ORDER_BY_ID = (id: number): string => apiUrl + ControlName.ORDER + ORDER.GET_ORDER_BY_ID + `${id}`;
    export const GET_MY_ORDER_LIKE_CUSTOMER = (): string => apiUrl + ControlName.ORDER + ORDER.GET_MY_ORDER_LIKE_CUSTOMER;
    export const GET_MY_ORDER_LIKE_DELIVERY = (): string => apiUrl + ControlName.ORDER + ORDER.GET_MY_ORDER_LIKE_DELIVERY;
    export const PUT_UPDATE_ORDER_INFO = (): string => apiUrl + ControlName.ORDER + ORDER.PUT_UPDATE_ORDER_INFO;
    export const PUT_UPDATE_ORDER_STATUS = (): string => apiUrl + ControlName.ORDER + ORDER.PUT_UPDATE_ORDER_STATUS;
    export const DELETE_ORDER = (): string => apiUrl + ControlName.ORDER + ORDER.DELETE_ORDER;


    // ORDER_REQUEST
    export const POST_CREATE_ORDER_REQUEST = (): string => apiUrl + ControlName.ORDER_REQUEST + ORDER_REQUEST.POST_CREATE_ORDER_REQUEST;
    export const PUT_APPROVE_ORDER_REQUEST = (): string => apiUrl + ControlName.ORDER_REQUEST + ORDER_REQUEST.PUT_APPROVE_ORDER_REQUEST;
    export const PUT_REJECT_ORDER_REQUEST = (): string => apiUrl + ControlName.ORDER_REQUEST + ORDER_REQUEST.PUT_REJECT_ORDER_REQUEST;
    export const DELETE_MY_ORDER_REQUEST = (): string => apiUrl + ControlName.ORDER_REQUEST + ORDER_REQUEST.DELETE_MY_ORDER_REQUEST;
    // tslint:disable-next-line:max-line-length
    export const DELETE_ORDER_REQUEST_LIKE_CUSTOMER = (): string => apiUrl + ControlName.ORDER_REQUEST + ORDER_REQUEST.DELETE_ORDER_REQUEST_LIKE_CUSTOMER;
    export const GET_MY_REQUEST = (): string => apiUrl + ControlName.ORDER_REQUEST + ORDER_REQUEST.GET_MY_REQUEST;
    export const GET_ORDER_REQUEST = (id: number): string => apiUrl + ControlName.ORDER_REQUEST + ORDER_REQUEST.GET_ORDER_REQUEST + `${id}`;
    // tslint:disable-next-line:max-line-length
    export const DELETE_REJECT_ALL_ORDER_REQUEST = (): string => apiUrl + ControlName.ORDER_REQUEST + ORDER_REQUEST.DELETE_REJECT_ALL_ORDER_REQUEST;


    // PRODUCT
    export const POST_PRODUCTS = (): string => apiUrl + ControlName.PRODUCT + PRODUCT.POST_PRODUCTS;
    export const GET_PRODUCTS_BY_ORDER_ID = (): string => apiUrl + ControlName.PRODUCT + PRODUCT.GET_PRODUCTS_BY_ORDER_ID;
    export const GET_PRODUCT_BY_ID = (): string => apiUrl + ControlName.PRODUCT + PRODUCT.GET_PRODUCT_BY_ID;
    export const PUT_PRODUCT_BY_ID = (): string => apiUrl + ControlName.PRODUCT + PRODUCT.PUT_PRODUCT_BY_ID;
    export const DELETE_PRODUCT = (): string => apiUrl + ControlName.PRODUCT + PRODUCT.DELETE_PRODUCT;

    // USER_INFO
    export const GET_USER_INFO = (id: string): string => apiUrl + ControlName.USER_INFO + USER_INFO.GET_USER_INFO + `${id}`;
    export const GET_ALL_USER = (): string => apiUrl + ControlName.USER_INFO + USER_INFO.GET_ALL_USER;
    export const GET_MY_INFO = (): string => apiUrl + ControlName.USER_INFO + USER_INFO.GET_MY_INFO;

    export const POST_CUSTOMER_REWIEWS = (): string => apiUrl + ControlName.CUSTOMER_REVIEW + CUSTOMER_REVIEW.POST_CUSTOMER_REWIEWS;
    // tslint:disable-next-line:max-line-length
    export const GET_CUSTOMER_REWIEWS = (id: number): string => apiUrl + ControlName.CUSTOMER_REVIEW + CUSTOMER_REVIEW.GET_CUSTOMER_REWIEWS + `${id}`;

}
