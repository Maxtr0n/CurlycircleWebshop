package hu.schutz.curlycircleandroidapp.data.source.local

import hu.schutz.curlycircleandroidapp.data.EntityCreatedViewModel
import hu.schutz.curlycircleandroidapp.data.Order
import hu.schutz.curlycircleandroidapp.data.OrderUpsertDto
import hu.schutz.curlycircleandroidapp.data.Result
import hu.schutz.curlycircleandroidapp.data.source.OrdersDataSource
import hu.schutz.curlycircleandroidapp.data.source.local.dao.OrdersDao
import kotlinx.coroutines.CoroutineDispatcher
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.flow.Flow
import kotlinx.coroutines.flow.map
import kotlinx.coroutines.withContext

class OrdersLocalDataSource(
    private val dao: OrdersDao,
    private val ioDispatcher: CoroutineDispatcher = Dispatchers.IO
) : OrdersDataSource {

    override suspend fun getOrders(userId: Int): Result<List<Order>> = withContext(ioDispatcher) {
        return@withContext try {
            Result.Success(dao.getOrders())
        } catch (e: Exception) {
            Result.Error(e)
        }
    }

    override fun getOrdersStream(userId: Int): Flow<Result<List<Order>>> {
        return dao.getOrdersStream().map {
            Result.Success(it)
        }
    }

    override suspend fun placeOrder(orderUpsertDto: OrderUpsertDto): Result<EntityCreatedViewModel> {
        // NO-OP: placing orders is an online only function of the app,
        // writes in the app are online-only at the moment
        return Result.Error(Exception())
    }

    override suspend fun saveOrder(order: Order) {
        dao.instertOrder(order)
    }

    override suspend fun deleteOrders() {
        dao.deleteOrders()
    }
}