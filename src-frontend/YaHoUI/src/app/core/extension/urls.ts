import { environment } from 'src/environments/environment';

export const apiUrl = environment.apiUrl;
// tslint:disable-next-line:no-namespace
export namespace urls {
    export enum ACCOUNT {
        GET_ALL_USERS = `/all-users/`,
        POST_SIGN_IN = `/sign-in/`,
        POST_SIGN_UP = `/sign-up/`,
        PUT_CHANGE_PASSWORD = `/change-password/`,
        PUT_UPDATE_USER = `/update-user/`,
        PUT_UPDATE_USER_TELEGRAMID = `/update-user-telegramId/`,
        POST_go-to-liq-pay
    }







POST
​/api​/Account​/go-to-liq-pay

POST
​/api​/Account​/liq-pay-result
}