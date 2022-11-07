package hu.schutz.curlycircleandroidapp.data.source.local

import androidx.room.Database
import androidx.room.RoomDatabase
import hu.schutz.curlycircleandroidapp.data.ProductCategory

@Database(entities = [ProductCategory::class], version = 1, exportSchema = false)
abstract class CurlyCircleDatabase : RoomDatabase() {

    abstract fun productCategoriesDao(): ProductCategoriesDao
}