package hu.schutz.curlycircleandroidapp.data.source.local.dao

import androidx.room.Insert
import androidx.room.OnConflictStrategy
import androidx.room.Query
import hu.schutz.curlycircleandroidapp.data.Order
import kotlinx.coroutines.flow.Flow

interface OrdersDao {

    @Query("SELECT * FROM orders")
    fun getOrdersStream(): Flow<List<Order>>

    @Query("SELECT * FROM orders")
    fun getOrders(): List<Order>

    @Insert(onConflict = OnConflictStrategy.REPLACE)
    suspend fun instertOrder(order: Order)

    @Query("DELETE FROM orders")
    suspend fun deleteOrders()
}