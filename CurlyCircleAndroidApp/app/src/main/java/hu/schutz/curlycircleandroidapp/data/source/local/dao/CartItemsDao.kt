package hu.schutz.curlycircleandroidapp.data.source.local.dao

import androidx.room.Insert
import androidx.room.OnConflictStrategy
import androidx.room.Query
import hu.schutz.curlycircleandroidapp.data.CartItem
import kotlinx.coroutines.flow.Flow

interface CartItemsDao {

    @Query("SELECT * FROM cartItems")
    fun getCartItemsStream(): Flow<List<CartItem>>

    @Query("SELECT * FROM cartItems")
    suspend fun getCartItems(): List<CartItem>

    @Insert(onConflict = OnConflictStrategy.REPLACE)
    suspend fun insertCartItem(cartItem: CartItem)

    @Query("DELETE FROM cartItems")
    suspend fun deleteCartItems()
}