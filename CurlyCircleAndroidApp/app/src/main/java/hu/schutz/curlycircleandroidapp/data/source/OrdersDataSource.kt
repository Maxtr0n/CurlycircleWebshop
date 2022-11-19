package hu.schutz.curlycircleandroidapp.data.source

import hu.schutz.curlycircleandroidapp.data.EntityCreatedViewModel
import hu.schutz.curlycircleandroidapp.data.Order
import hu.schutz.curlycircleandroidapp.data.OrderUpsertDto
import hu.schutz.curlycircleandroidapp.data.Result
import kotlinx.coroutines.flow.Flow

interface OrdersDataSource {

    suspend fun getOrders(userId: Int): Result<List<Order>>

    fun getOrdersStream(userId: Int): Flow<Result<List<Order>>>

    suspend fun placeOrder(orderUpsertDto: OrderUpsertDto): Result<EntityCreatedViewModel>

    suspend fun saveOrder(order: Order)

    suspend fun deleteOrders()
}