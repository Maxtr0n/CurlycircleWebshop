package hu.schutz.curlycircleandroidapp.util

import androidx.room.TypeConverter
import com.google.gson.Gson
import com.google.gson.reflect.TypeToken

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
}