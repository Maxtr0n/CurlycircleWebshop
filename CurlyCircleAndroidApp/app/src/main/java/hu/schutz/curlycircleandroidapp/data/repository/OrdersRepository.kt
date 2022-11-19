package hu.schutz.curlycircleandroidapp.data.repository

import hu.schutz.curlycircleandroidapp.data.EntityCreatedViewModel
import hu.schutz.curlycircleandroidapp.data.Order
import hu.schutz.curlycircleandroidapp.data.OrderUpsertDto
import hu.schutz.curlycircleandroidapp.data.Result
import kotlinx.coroutines.flow.Flow

interface OrdersRepository {

    fun getOrdersStream(userId: Int): Flow<Result<List<Order>>>

    suspend fun getOrders(userId: Int, forceUpdate: Boolean = false): Result<List<Order>>

    suspend fun placeOrder(orderUpsertDto: OrderUpsertDto): Result<EntityCreatedViewModel>
}