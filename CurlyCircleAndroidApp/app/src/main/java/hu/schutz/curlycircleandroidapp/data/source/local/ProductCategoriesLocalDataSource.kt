package hu.schutz.curlycircleandroidapp.data.source.local

import hu.schutz.curlycircleandroidapp.data.ProductCategory
import hu.schutz.curlycircleandroidapp.data.Result
import hu.schutz.curlycircleandroidapp.data.Result.Success
import hu.schutz.curlycircleandroidapp.data.Result.Error
import hu.schutz.curlycircleandroidapp.data.source.ProductCategoriesDataSource
import kotlinx.coroutines.CoroutineDispatcher
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.flow.Flow
import kotlinx.coroutines.flow.map

class ProductCategoriesLocalDataSource internal constructor(
    private val dao: ProductCategoriesDao,
    private val ioDispatcher: CoroutineDispatcher = Dispatchers.IO
) : ProductCategoriesDataSource {

    override fun getProductCategoriesStream(): Flow<Result<List<ProductCategory>>> {
        return dao.observeProductCategories().map {
            Success(it)
        }
    }

}