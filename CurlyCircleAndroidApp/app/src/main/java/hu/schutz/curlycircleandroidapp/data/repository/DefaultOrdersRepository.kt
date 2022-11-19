package hu.schutz.curlycircleandroidapp.data.repository

import hu.schutz.curlycircleandroidapp.data.*
import hu.schutz.curlycircleandroidapp.data.source.OrdersDataSource
import kotlinx.coroutines.CoroutineDispatcher
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.flow.Flow

class DefaultOrdersRepository(
    private val ordersRemoteDataSource: OrdersDataSource,
    private val ordersLocalDataSource: OrdersDataSource,
    private val ioDispatcher: CoroutineDispatcher = Dispatchers.IO
) : OrdersRepository {

    override fun getOrdersStream(userId: Int): Flow<Result<List<Order>>> {
        return ordersLocalDataSource.getOrdersStream(userId)
    }

    override suspend fun getOrders(
        userId: Int,
        forceUpdate: Boolean
    ): Result<List<Order>> {
        if (forceUpdate) {
            try {
                updateOrdersFromRemoteDataSource(userId)
            } catch (e: Exception) {
                return Result.Error(e)
            }
        }

        return ordersLocalDataSource.getOrders(userId)
    }

    override suspend fun placeOrder(
        orderUpsertDto: OrderUpsertDto
    ): Result<EntityCreatedViewModel> {
        return ordersRemoteDataSource.placeOrder(orderUpsertDto)
    }

    private suspend fun updateOrdersFromRemoteDataSource(
        userId: Int
    ) {
        val remoteOrders = ordersRemoteDataSource.getOrders(userId)

        if (remoteOrders is Result.Success) {
            ordersLocalDataSource.deleteOrders()
            remoteOrders.data.forEach{ order ->
                ordersLocalDataSource.saveOrder(order)
            }
        } else if (remoteOrders is Result.Error) {
            throw remoteOrders.exception
        }
    }
}