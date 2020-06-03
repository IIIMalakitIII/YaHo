import { IUser } from './interfaces/user.interface';
import * as jwt_decode from 'jwt-decode';
import { FormGroup } from '@angular/forms';

export const configureToastr = (toastr) => {
    toastr.toastrConfig.maxOpened = 1;
    toastr.toastrConfig.timeOut = 1000;
};

export const enum toastrTitle {
    Error = 'Error',
    Success = 'Success',
    Warning = 'Warning'
}

export const getTokenValue = (): IUser => {
    const tokenValue = JSON.parse(localStorage.getItem('currentUser'));
    if (tokenValue !== null) {
      const model = jwt_decode(tokenValue?.token);
      const userValue = {
        token: tokenValue.token,
        id: model.id,
        customer: model.customer,
        delivery: model.delivery,
        email: model.email,
        lastName: model.last_name,
        firstName: model.first_name
      };
      return userValue;
    }
    return tokenValue;
};

// custom validator to check that two fields match
export function MustMatch(controlName: string, matchingControlName: string) {
    return (formGroup: FormGroup) => {
        const control = formGroup.controls[controlName];
        const matchingControl = formGroup.controls[matchingControlName];

        if (matchingControl.errors && !matchingControl.errors.mustMatch) {
            return;
        }

        if (control.value !== matchingControl.value) {
            matchingControl.setErrors({ mustMatch: true });
        } else {
            matchingControl.setErrors(null);
        }
    };
}
