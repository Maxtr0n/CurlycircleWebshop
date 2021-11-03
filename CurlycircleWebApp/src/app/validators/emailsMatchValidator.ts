import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export const emailsMatchValidator: ValidatorFn = (control: AbstractControl):
    ValidationErrors | null => {
    const email = control.get('email');
    const emailConfirm = control.get('emailConfirm');

    return email && emailConfirm && email.value === emailConfirm.value ? null : { emailsDiffer: true }
}