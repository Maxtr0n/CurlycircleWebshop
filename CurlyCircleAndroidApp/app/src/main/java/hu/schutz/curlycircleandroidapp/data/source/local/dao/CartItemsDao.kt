package hu.schutz.curlycircleandroidapp.data.source.local.dao

import androidx.room.Dao
import androidx.room.Insert
import androidx.room.OnConflictStrategy
import androidx.room.Query
import androidx.room.Transaction
import hu.schutz.curlycircleandroidapp.data.CartItem
import hu.schutz.curlycircleandroidapp.data.CartItemAndProduct
import hu.schutz.curlycircleandroidapp.data.Product
import kotlinx.coroutines.flow.Flow

@Dao
interface CartItemsDao {

    @Transaction
    @Query(
        "SELECT * FROM cartItems, products WHERE cartItems.productId = products.id"
    )
    fun getCartItemsStream(): Flow<List<CartItemAndProduct>>

    @Transaction
    @Query("SELECT * FROM cartItems, products WHERE cartItems.productId = products.id")
    suspend fun getCartItems(): List<CartItemAndProduct>

    @Insert(onConflict = OnConflictStrategy.REPLACE)
    suspend fun insertCartItem(cartItem: CartItem)

    @Query("DELETE FROM cartItems")
    suspend fun deleteCartItems()
}