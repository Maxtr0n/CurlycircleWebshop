package hu.schutz.curlycircleandroidapp.data.source.local

import androidx.room.Database
import androidx.room.RoomDatabase
import hu.schutz.curlycircleandroidapp.data.*
import hu.schutz.curlycircleandroidapp.data.source.local.dao.*

@Database(entities = [ProductCategory::class, Product::class, Color::class, Material::class, Pattern::class], version = 3, exportSchema = false)
abstract class CurlyCircleDatabase : RoomDatabase() {

    abstract fun productCategoriesDao(): ProductCategoriesDao

    abstract fun productsDao(): ProductsDao

    abstract fun colorsDao(): ColorsDao

    abstract fun materialsDao(): MaterialsDao

    abstract fun patternsDao(): PatternsDao
}