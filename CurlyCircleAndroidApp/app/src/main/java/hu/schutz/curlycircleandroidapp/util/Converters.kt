package hu.schutz.curlycircleandroidapp.util

import androidx.room.TypeConverter
import com.google.gson.Gson
import com.google.gson.reflect.TypeToken
import hu.schutz.curlycircleandroidapp.data.PaymentMethod
import hu.schutz.curlycircleandroidapp.data.Role
import hu.schutz.curlycircleandroidapp.data.ShippingMethod
import java.util.*

inline fun <reified T> Gson.fromJson(json: String): T =
    fromJson(json, object : TypeToken<T>() {}.type)

class Converters {

    @TypeConverter
    fun stringToListOfStrings(value: String): List<String> {
        return try {
            Gson().fromJson(value)
        } catch (e: Exception) {
            listOf()
        }
    }

    @TypeConverter
    fun listOfStringsToString(value: List<String>): String {
        return Gson().toJson(value)
    }

    @TypeConverter
    fun roleToString(value: Role) = value.name

    @TypeConverter
    fun stringToRole(value: String) = enumValueOf<Role>(value)

    @TypeConverter
    fun shippingMethodToString(value: ShippingMethod) = value.name

    @TypeConverter
    fun stringToShippingMethod(value: String) = enumValueOf<ShippingMethod>(value)

    @TypeConverter
    fun paymentMethodToString(value: PaymentMethod) = value.name

    @TypeConverter
    fun stringToPaymentMethod(value: String) = enumValueOf<PaymentMethod>(value)

    @TypeConverter
    fun fromTimestamp(value: Long?): Date? {
        return value?.let { Date(it) }
    }

    @TypeConverter
    fun dateToTimestamp(date: Date?): Long? {
        return date?.time?.toLong()
    }
}