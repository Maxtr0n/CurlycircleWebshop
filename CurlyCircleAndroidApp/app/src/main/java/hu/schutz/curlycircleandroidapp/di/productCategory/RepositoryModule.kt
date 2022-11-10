package hu.schutz.curlycircleandroidapp.di.productCategory

import dagger.Module
import dagger.Provides
import dagger.hilt.InstallIn
import dagger.hilt.components.SingletonComponent
import hu.schutz.curlycircleandroidapp.data.repository.DefaultProductCategoriesRepository
import hu.schutz.curlycircleandroidapp.data.repository.ProductCategoriesRepository
import hu.schutz.curlycircleandroidapp.data.source.ProductCategoriesDataSource
import hu.schutz.curlycircleandroidapp.data.source.local.ProductCategoriesLocalDataSource
import hu.schutz.curlycircleandroidapp.data.source.remote.ProductCategoriesRemoteDataSource
import hu.schutz.curlycircleandroidapp.di.IoDispatcher
import kotlinx.coroutines.CoroutineDispatcher
import javax.inject.Singleton

@Module
@InstallIn(SingletonComponent::class)
object RepositoryModule {

    @Singleton
    @Provides
    fun provideProductCategoriesRepository(
        @RemoteProductCategoriesDataSource remoteDataSource: ProductCategoriesDataSource,
        @LocalProductCategoriesDataSource localDataSource: ProductCategoriesDataSource,
        @IoDispatcher ioDispatcher: CoroutineDispatcher
    ): ProductCategoriesRepository {
        return DefaultProductCategoriesRepository(remoteDataSource, localDataSource, ioDispatcher)
    }
}