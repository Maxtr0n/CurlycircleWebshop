package hu.schutz.curlycircleandroidapp.data.source.local.dao

import androidx.room.Dao
import androidx.room.Insert
import androidx.room.OnConflictStrategy
import androidx.room.Query
import hu.schutz.curlycircleandroidapp.data.Product
import kotlinx.coroutines.flow.Flow

@Dao
interface ProductsDao {

    @Query("SELECT * FROM products")
    fun getProductsStream(): Flow<List<Product>>

    @Query("SELECT * FROM products")
    fun getProducts(): List<Product>

    @Query("SELECT * FROM products WHERE id = :id")
    fun getProductStream(id: Int): Flow<Product>

    @Query("SELECT * FROM products WHERE id = :id")
    fun getProduct(id: Int): Product

    @Insert(onConflict = OnConflictStrategy.REPLACE)
    suspend fun insertProduct(product: Product)

    @Query("DELETE FROM products")
    suspend fun deleteProducts()
}