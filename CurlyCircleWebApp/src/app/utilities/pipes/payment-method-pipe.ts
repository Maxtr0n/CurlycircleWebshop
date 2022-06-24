import { Pipe, PipeTransform } from "@angular/core";
import { PaymentMethod } from "src/app/models/models";

@Pipe({ name: 'payment' })
export class PaymentMethodPipe implements PipeTransform {
    transform(value: PaymentMethod): string {
        console.log(value);
        switch (PaymentMethod[value].toString()) {
            case PaymentMethod.MoneyTransfer.toString():
                return 'Átutalás';
            case PaymentMethod.CashOnDelivery.toString():
                return 'Készpénz';
            case PaymentMethod.WebPayment.toString():
                return 'Webes fizetés';
            default:
                return 'Ismeretlen';
        }
    }
}