package hu.schutz.curlycircleandroidapp.data.source.local.dao

import androidx.room.Dao
import androidx.room.Insert
import androidx.room.OnConflictStrategy
import androidx.room.Query
import hu.schutz.curlycircleandroidapp.data.Order
import kotlinx.coroutines.flow.Flow

@Dao
interface OrdersDao {

    @Query("SELECT * FROM orders ORDER BY orderDateTime DESC")
    fun getOrdersStream(): Flow<List<Order>>

    @Query("SELECT * FROM orders ORDER BY orderDateTime DESC")
    fun getOrders(): List<Order>

    @Insert(onConflict = OnConflictStrategy.REPLACE)
    suspend fun instertOrder(order: Order)

    @Query("DELETE FROM orders")
    suspend fun deleteOrders()
}