package hu.schutz.curlycircleandroidapp.di

import dagger.Module
import dagger.Provides
import dagger.hilt.InstallIn
import dagger.hilt.components.SingletonComponent
import hu.schutz.curlycircleandroidapp.data.source.*
import hu.schutz.curlycircleandroidapp.data.source.local.*
import hu.schutz.curlycircleandroidapp.data.source.remote.*
import kotlinx.coroutines.CoroutineDispatcher
import javax.inject.Qualifier
import javax.inject.Singleton

@Qualifier
@Retention(AnnotationRetention.RUNTIME)
annotation class RemoteDataSource

@Qualifier
@Retention(AnnotationRetention.RUNTIME)
annotation class LocalDataSource


@Module
@InstallIn(SingletonComponent::class)
object DataSourceModule {

    @Singleton
    @RemoteDataSource
    @Provides
    fun provideRemoteProductCategoriesDataSource(
        api: CurlyCircleApi,
        @IoDispatcher ioDispatcher: CoroutineDispatcher
    ): ProductCategoriesDataSource {
        return ProductCategoriesRemoteDataSource(api, ioDispatcher)
    }

    @Singleton
    @LocalDataSource
    @Provides
    fun provideLocalProductCategoriesDataSource(
        database: CurlyCircleDatabase,
        @IoDispatcher ioDispatcher: CoroutineDispatcher
        ): ProductCategoriesDataSource {
        return ProductCategoriesLocalDataSource(database.productCategoriesDao(), ioDispatcher)
    }

    @Singleton
    @RemoteDataSource
    @Provides
    fun provideRemoteProductsDataSource(
        api: CurlyCircleApi,
        @IoDispatcher ioDispatcher: CoroutineDispatcher
    ): ProductsDataSource {
        return ProductsRemoteDataSource(api, ioDispatcher)
    }

    @Singleton
    @LocalDataSource
    @Provides
    fun provideLocalProductsDataSource(
        database: CurlyCircleDatabase,
        @IoDispatcher ioDispatcher: CoroutineDispatcher
    ): ProductsDataSource {
        return ProductsLocalDataSource(database.productsDao(), ioDispatcher)
    }

    @Singleton
    @RemoteDataSource
    @Provides
    fun provideRemoteColorsDataSource(
        api: CurlyCircleApi,
        @IoDispatcher ioDispatcher: CoroutineDispatcher
    ): ColorsDataSource {
        return ColorsRemoteDataSource(api, ioDispatcher)
    }

    @Singleton
    @LocalDataSource
    @Provides
    fun provideLocalColorsDataSource(
        database: CurlyCircleDatabase,
        @IoDispatcher ioDispatcher: CoroutineDispatcher
    ): ColorsDataSource {
        return ColorsLocalDataSource(database.colorsDao(), ioDispatcher)
    }

    @Singleton
    @RemoteDataSource
    @Provides
    fun provideRemoteMaterialsDataSource(
        api: CurlyCircleApi,
        @IoDispatcher ioDispatcher: CoroutineDispatcher
    ): MaterialsDataSource {
        return MaterialsRemoteDataSource(api, ioDispatcher)
    }

    @Singleton
    @LocalDataSource
    @Provides
    fun provideLocalMaterialsDataSource(
        database: CurlyCircleDatabase,
        @IoDispatcher ioDispatcher: CoroutineDispatcher
    ): MaterialsDataSource {
        return MaterialsLocalDataSource(database.materialsDao(), ioDispatcher)
    }

    @Singleton
    @RemoteDataSource
    @Provides
    fun provideRemotePatternsDataSource(
        api: CurlyCircleApi,
        @IoDispatcher ioDispatcher: CoroutineDispatcher
    ): PatternsDataSource {
        return PatternsRemoteDataSource(api, ioDispatcher)
    }

    @Singleton
    @LocalDataSource
    @Provides
    fun provideLocalPatternsDataSource(
        database: CurlyCircleDatabase,
        @IoDispatcher ioDispatcher: CoroutineDispatcher
    ): PatternsDataSource {
        return PatternsLocalDataSource(database.patternsDao(), ioDispatcher)
    }

    @Singleton
    @RemoteDataSource
    @Provides
    fun provideRemoteOrdersDataSource(
        api: CurlyCircleApi,
        @IoDispatcher ioDispatcher: CoroutineDispatcher
    ): OrdersDataSource {
        return OrdersRemoteDataSource(api, ioDispatcher)
    }

    @Singleton
    @LocalDataSource
    @Provides
    fun provideLocalOrdersDataSource(
        database: CurlyCircleDatabase,
        @IoDispatcher ioDispatcher: CoroutineDispatcher
    ): OrdersDataSource {
        return OrdersLocalDataSource(database.ordersDao(), ioDispatcher)
    }
}

