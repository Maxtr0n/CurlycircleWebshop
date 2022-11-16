package hu.schutz.curlycircleandroidapp.data.source.local.dao

import androidx.room.Dao
import androidx.room.Insert
import androidx.room.OnConflictStrategy
import androidx.room.Query
import hu.schutz.curlycircleandroidapp.data.Product
import kotlinx.coroutines.flow.Flow

@Dao
interface ProductsDao {

    @Query("SELECT * FROM products WHERE productCategoryId = :productCategoryId")
    fun getProductsStream(productCategoryId: Int): Flow<List<Product>>

    @Query("SELECT * FROM products WHERE productCategoryId = :productCategoryId")
    fun getProducts(productCategoryId: Int): List<Product>

    @Query("SELECT * FROM products WHERE id = :id")
    fun getProductStream(id: Int): Flow<Product>

    @Query("SELECT * FROM products WHERE id = :id")
    fun getProduct(id: Int): Product

    @Insert(onConflict = OnConflictStrategy.REPLACE)
    suspend fun insertProduct(product: Product)

    @Query("DELETE FROM products WHERE productCategoryId = :productCategoryId")
    suspend fun deleteProducts(productCategoryId: Int)
}