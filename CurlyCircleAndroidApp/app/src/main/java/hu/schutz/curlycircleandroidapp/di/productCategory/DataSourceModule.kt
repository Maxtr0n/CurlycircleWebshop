package hu.schutz.curlycircleandroidapp.di.productCategory

import dagger.Module
import dagger.Provides
import dagger.hilt.InstallIn
import dagger.hilt.components.SingletonComponent
import hu.schutz.curlycircleandroidapp.data.source.ProductCategoriesDataSource
import hu.schutz.curlycircleandroidapp.data.source.local.CurlyCircleDatabase
import hu.schutz.curlycircleandroidapp.data.source.local.ProductCategoriesLocalDataSource
import hu.schutz.curlycircleandroidapp.data.source.remote.CurlyCircleApi
import hu.schutz.curlycircleandroidapp.data.source.remote.ProductCategoriesRemoteDataSource
import hu.schutz.curlycircleandroidapp.di.IoDispatcher
import kotlinx.coroutines.CoroutineDispatcher
import javax.inject.Qualifier
import javax.inject.Singleton

@Qualifier
@Retention(AnnotationRetention.RUNTIME)
annotation class RemoteProductCategoriesDataSource

@Qualifier
@Retention(AnnotationRetention.RUNTIME)
annotation class LocalProductCategoriesDataSource


@Module
@InstallIn(SingletonComponent::class)
object DataSourceModule {

    @Singleton
    @RemoteProductCategoriesDataSource
    @Provides
    fun provideRemoteProductCategoriesDataSource(
        api: CurlyCircleApi,
        @IoDispatcher ioDispatcher: CoroutineDispatcher
    ): ProductCategoriesDataSource {
        return ProductCategoriesRemoteDataSource(api, ioDispatcher)
    }

    @Singleton
    @LocalProductCategoriesDataSource
    @Provides
    fun provideLocalProductCategoriesDataSource(
        database: CurlyCircleDatabase,
        @IoDispatcher ioDispatcher: CoroutineDispatcher
        ): ProductCategoriesDataSource {
        return ProductCategoriesLocalDataSource(database.productCategoriesDao(), ioDispatcher)
    }
}

