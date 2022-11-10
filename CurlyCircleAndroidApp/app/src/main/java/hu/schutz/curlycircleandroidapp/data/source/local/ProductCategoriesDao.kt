package hu.schutz.curlycircleandroidapp.data.source.local

import androidx.room.Dao
import androidx.room.Insert
import androidx.room.OnConflictStrategy
import androidx.room.Query
import hu.schutz.curlycircleandroidapp.data.ProductCategory
import kotlinx.coroutines.flow.Flow

@Dao
interface ProductCategoriesDao {

    @Query("SELECT * FROM productCategories")
    fun observeProductCategories(): Flow<List<ProductCategory>>

    @Query("SELECT * FROM productCategories")
    fun getProductCategories(): List<ProductCategory>

    @Insert(onConflict = OnConflictStrategy.REPLACE)
    suspend fun insertProductCategory(productCategory: ProductCategory)

    @Query("DELETE FROM productCategories")
    suspend fun deleteProductCategories()
}