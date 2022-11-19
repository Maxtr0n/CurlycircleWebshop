package hu.schutz.curlycircleandroidapp.data.source.remote

import hu.schutz.curlycircleandroidapp.data.EntityCreatedViewModel
import hu.schutz.curlycircleandroidapp.data.Order
import hu.schutz.curlycircleandroidapp.data.OrderUpsertDto
import hu.schutz.curlycircleandroidapp.data.Result
import hu.schutz.curlycircleandroidapp.data.source.OrdersDataSource
import kotlinx.coroutines.CoroutineDispatcher
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.flow.Flow
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.runBlocking
import kotlinx.coroutines.withContext

class OrdersRemoteDataSource(
    private val api: CurlyCircleApi,
    private val ioDispatcher: CoroutineDispatcher = Dispatchers.IO
) : OrdersDataSource {

    override suspend fun getOrders(userId: Int): Result<List<Order>> = withContext(ioDispatcher) {
        return@withContext try {
            Result.Success(api.getUserOrders(userId).orders)
        } catch (e: Exception) {
            Result.Error(e)
        }
    }

    override fun getOrdersStream(userId: Int): Flow<Result<List<Order>>> {
        return MutableStateFlow(
            runBlocking {
                getOrders(userId)
            }
        )
    }

    override suspend fun placeOrder(orderUpsertDto: OrderUpsertDto): Result<EntityCreatedViewModel>
    = withContext(ioDispatcher) {
        return@withContext try {
            Result.Success(api.placeOrder(orderUpsertDto))
        } catch (e: Exception) {
            Result.Error(e)
        }
    }

    override suspend fun saveOrder(order: Order) {
        // NO-OP
    }

    override suspend fun deleteOrders() {
        // NO-OP
    }
}