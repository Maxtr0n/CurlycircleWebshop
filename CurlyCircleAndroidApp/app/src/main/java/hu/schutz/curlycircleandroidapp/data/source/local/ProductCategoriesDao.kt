package hu.schutz.curlycircleandroidapp.data.source.local

import androidx.room.Dao
import androidx.room.Query
import hu.schutz.curlycircleandroidapp.data.ProductCategory
import kotlinx.coroutines.flow.Flow

@Dao
interface ProductCategoriesDao {

    @Query("SELECT * FROM productCategories")
    fun observeProductCategories(): Flow<List<ProductCategory>>
}