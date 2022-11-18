package hu.schutz.curlycircleandroidapp.data.source.local

import androidx.room.Database
import androidx.room.RoomDatabase
import androidx.room.TypeConverter
import androidx.room.TypeConverters
import hu.schutz.curlycircleandroidapp.data.*
import hu.schutz.curlycircleandroidapp.data.source.local.dao.*
import hu.schutz.curlycircleandroidapp.util.Converters

@Database(entities = [
    ProductCategory::class,
    Product::class,
    Color::class,
    Material::class,
    Pattern::class,
    User::class,
    Order::class
], version = 3, exportSchema = false)
@TypeConverters(Converters::class)
abstract class CurlyCircleDatabase : RoomDatabase() {

    abstract fun productCategoriesDao(): ProductCategoriesDao

    abstract fun productsDao(): ProductsDao

    abstract fun colorsDao(): ColorsDao

    abstract fun materialsDao(): MaterialsDao

    abstract fun patternsDao(): PatternsDao

    abstract fun userDao(): UserDao
}