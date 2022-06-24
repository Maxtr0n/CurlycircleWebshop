import { Pipe, PipeTransform } from "@angular/core";
import { ShippingMethod } from "src/app/models/models";

@Pipe({ name: 'shipping' })
export class ShippingMethodPipe implements PipeTransform {
    transform(value: number): string {
        switch (ShippingMethod[value].toString()) {
            case ShippingMethod.Foxpost.toString():
                return 'Foxpost';
            case ShippingMethod.MagyarPostaPont.toString():
                return 'Magyar Posta Pont';
            case ShippingMethod.MagyarPostaCsomagPont.toString():
                return 'Magyar Posta Csomag Pont';
            case ShippingMethod.HomeDelivery.toString():
                return 'Házhozszállítás';
            case ShippingMethod.PersonalDelivery.toString():
                return 'Személyes átvétel';
            default:
                return 'Ismeretlen';
        }
    }
}