package hu.schutz.curlycircleandroidapp.data.source.local.dao

import androidx.room.Dao
import androidx.room.Insert
import androidx.room.OnConflictStrategy
import androidx.room.Query
import hu.schutz.curlycircleandroidapp.data.CartItem
import hu.schutz.curlycircleandroidapp.data.Product
import kotlinx.coroutines.flow.Flow

@Dao
interface CartItemsDao {

    @Query(
        "SELECT * FROM cartItems JOIN products ON cartItems.productId = products.id"
    )
    fun getCartItemsStream(): Flow<Map<CartItem, Product>>

    @Query("SELECT * FROM cartItems JOIN products ON cartItems.productId = products.id")
    suspend fun getCartItems(): Map<CartItem, Product>

    @Insert(onConflict = OnConflictStrategy.REPLACE)
    suspend fun insertCartItem(cartItem: CartItem)

    @Query("DELETE FROM cartItems")
    suspend fun deleteCartItems()
}