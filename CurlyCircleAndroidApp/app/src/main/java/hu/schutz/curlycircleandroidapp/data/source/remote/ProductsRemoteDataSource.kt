package hu.schutz.curlycircleandroidapp.data.source.remote

import hu.schutz.curlycircleandroidapp.data.Product
import hu.schutz.curlycircleandroidapp.data.ProductQueryParameters
import hu.schutz.curlycircleandroidapp.data.Result
import hu.schutz.curlycircleandroidapp.data.source.ProductsDataSource
import kotlinx.coroutines.CoroutineDispatcher
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.flow.Flow
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.runBlocking
import kotlinx.coroutines.withContext

class ProductsRemoteDataSource(
    private val api: CurlyCircleApi,
    private val ioDispatcher: CoroutineDispatcher = Dispatchers.IO
    ) : ProductsDataSource {

    override fun getProductsStream(productQueryParameters: ProductQueryParameters): Flow<Result<List<Product>>> {
        return MutableStateFlow(
            runBlocking {
                getProducts(productQueryParameters)
            }
        )
    }

    override suspend fun getProducts(productQueryParameters: ProductQueryParameters): Result<List<Product>> = withContext(ioDispatcher) {
        return@withContext try {
            val productViewModels = api.getProducts(
                productQueryParameters.productCategoryId
            ).products
            val products: List<Product> = productViewModels.map { productViewModel ->
                Product(
                    id = productViewModel.id,
                    name = productViewModel.name,
                    description = productViewModel.description,
                    material = productViewModel.material?.name ?: "",
                    pattern = productViewModel.pattern?.name ?: "",
                    colors = productViewModel.colors.map { it.name },
                    thumbnailImageUrl = productViewModel.thumbnailImageUrl,
                    imageUrls = productViewModel.imageUrls,
                    price = productViewModel.price,
                    productCategoryId = productViewModel.productCategoryId,
                    isAvailable = productViewModel.isAvailable
                )
            }
            Result.Success(products)
        } catch (e: Exception) {
            Result.Error(e)
        }
    }

    override fun getProductStream(productId: Int): Flow<Result<Product>> {
        return MutableStateFlow(
            runBlocking {
                getProduct(productId)
            }
        )
    }

    override suspend fun getProduct(productId: Int): Result<Product> = withContext(ioDispatcher) {
        return@withContext try {
            val productViewModel = api.getProductById(productId)
            val product = Product(
                id = productViewModel.id,
                name = productViewModel.name,
                description = productViewModel.description,
                material = productViewModel.material?.name ?: "",
                pattern = productViewModel.pattern?.name ?: "",
                colors = productViewModel.colors.map { it.name },
                thumbnailImageUrl = productViewModel.thumbnailImageUrl,
                imageUrls = productViewModel.imageUrls,
                price = productViewModel.price,
                productCategoryId = productViewModel.productCategoryId,
                isAvailable = productViewModel.isAvailable
            )
            Result.Success(product)
        } catch (e: Exception) {
            Result.Error(e)
        }
    }

    override suspend fun saveProduct(product: Product) {
        // NO-OP
    }

    override suspend fun deleteAllProducts() {
        // NO-OP
    }
}