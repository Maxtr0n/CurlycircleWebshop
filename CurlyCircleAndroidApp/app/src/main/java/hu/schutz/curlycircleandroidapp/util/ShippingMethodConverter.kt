package hu.schutz.curlycircleandroidapp.util

import hu.schutz.curlycircleandroidapp.data.ShippingMethod

fun convertShippingMethod(shippingMethod: ShippingMethod): String {
    return when (shippingMethod) {
        ShippingMethod.Foxpost -> "Foxpost"
        ShippingMethod.HomeDelivery -> "Házhoz szállítás"
        ShippingMethod.MagyarPostaPont -> "Magyar Posta Pont"
        ShippingMethod.MagyarPostaCsomagPont -> "Magyar Posta CsomagPont"
        ShippingMethod.PersonalDelivery -> "Személyes átvétel"
    }
}