package hu.schutz.curlycircleandroidapp.util

import androidx.room.TypeConverter
import com.google.gson.Gson
import com.google.gson.reflect.TypeToken
import hu.schutz.curlycircleandroidapp.data.Role

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
}