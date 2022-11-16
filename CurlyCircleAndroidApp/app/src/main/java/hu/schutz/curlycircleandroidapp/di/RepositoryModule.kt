package hu.schutz.curlycircleandroidapp.di

import dagger.Module
import dagger.Provides
import dagger.hilt.InstallIn
import dagger.hilt.components.SingletonComponent
import hu.schutz.curlycircleandroidapp.data.repository.*
import hu.schutz.curlycircleandroidapp.data.source.*
import hu.schutz.curlycircleandroidapp.data.source.local.CurlyCircleDatabase
import hu.schutz.curlycircleandroidapp.data.source.remote.AuthApi
import hu.schutz.curlycircleandroidapp.data.source.remote.CurlyCircleApi
import kotlinx.coroutines.CoroutineDispatcher
import javax.inject.Singleton

@Module
@InstallIn(SingletonComponent::class)
object RepositoryModule {

    @Singleton
    @Provides
    fun provideProductCategoriesRepository(
        @RemoteDataSource remoteDataSource: ProductCategoriesDataSource,
        @LocalDataSource localDataSource: ProductCategoriesDataSource,
        @IoDispatcher ioDispatcher: CoroutineDispatcher
    ): ProductCategoriesRepository {
        return DefaultProductCategoriesRepository(remoteDataSource, localDataSource, ioDispatcher)
    }

    @Singleton
    @Provides
    fun provideProductsRepository(
        @RemoteDataSource remoteDataSource: ProductsDataSource,
        @LocalDataSource localDataSource: ProductsDataSource,
        @IoDispatcher ioDispatcher: CoroutineDispatcher
    ): ProductsRepository {
        return DefaultProductsRepository(remoteDataSource, localDataSource, ioDispatcher)
    }

    @Singleton
    @Provides
    fun provideColorsRepository(
        @RemoteDataSource remoteDataSource: ColorsDataSource,
        @LocalDataSource localDataSource: ColorsDataSource,
        @IoDispatcher ioDispatcher: CoroutineDispatcher
    ): ColorsRepository {
        return DefaultColorsRepository(remoteDataSource, localDataSource, ioDispatcher)
    }

    @Singleton
    @Provides
    fun provideMaterialsRepository(
        @RemoteDataSource remoteDataSource: MaterialsDataSource,
        @LocalDataSource localDataSource: MaterialsDataSource,
        @IoDispatcher ioDispatcher: CoroutineDispatcher
    ): MaterialsRepository {
        return DefaultMaterialsRepository(remoteDataSource, localDataSource, ioDispatcher)
    }

    @Singleton
    @Provides
    fun providePatternsRepository(
        @RemoteDataSource remoteDataSource: PatternsDataSource,
        @LocalDataSource localDataSource: PatternsDataSource,
        @IoDispatcher ioDispatcher: CoroutineDispatcher
    ): PatternsRepository {
        return DefaultPatternsRepository(remoteDataSource, localDataSource, ioDispatcher)
    }

    @Singleton
    @Provides
    fun provideUserRepository(
        database: CurlyCircleDatabase,
        api: CurlyCircleApi,
        @IoDispatcher ioDispatcher: CoroutineDispatcher
    ) : UserRepository {
        return DefaultUserRepository(api, database.userDao(), ioDispatcher)
    }

    @Singleton
    @Provides
    fun provideAuthRepository(
        database: CurlyCircleDatabase,
        api: AuthApi,
        @IoDispatcher ioDispatcher: CoroutineDispatcher
    ) : AuthRepository {
        return DefaultAuthRepository(api, database.userDao(), ioDispatcher)
    }
}