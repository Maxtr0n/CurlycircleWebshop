package hu.schutz.curlycircleandroidapp.data.source

import hu.schutz.curlycircleandroidapp.data.ProductCategory
import hu.schutz.curlycircleandroidapp.data.Result
import hu.schutz.curlycircleandroidapp.data.source.local.ProductCategoriesLocalDataSource
import hu.schutz.curlycircleandroidapp.data.source.remote.ProductCategoriesRemoteDataSource
import kotlinx.coroutines.CoroutineDispatcher
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.flow.Flow

class DefaultProductCategoriesRepository(
    private val productCategoriesLocalDataSource: ProductCategoriesLocalDataSource,
    private val productCategoriesRemoteDataSource: ProductCategoriesRemoteDataSource,
    private val ioDispatcher: CoroutineDispatcher = Dispatchers.IO
) : ProductCategoriesRepository {

    override fun getProductCategoriesStream(): Flow<Result<List<ProductCategory>>> {
        TODO("Not yet implemented")
    }

}