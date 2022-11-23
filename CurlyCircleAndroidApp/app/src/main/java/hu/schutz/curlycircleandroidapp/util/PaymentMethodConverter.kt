package hu.schutz.curlycircleandroidapp.util

import hu.schutz.curlycircleandroidapp.data.PaymentMethod
import hu.schutz.curlycircleandroidapp.data.ShippingMethod

fun convertPaymentMethod(paymentMethod: PaymentMethod): String {
    return when (paymentMethod) {
        PaymentMethod.MoneyTransfer -> "Átutalás"
        PaymentMethod.CashOnDelivery -> "Készpénzzel átvételkor"
        PaymentMethod.WebPayment -> "Webes fizetés"
    }
}