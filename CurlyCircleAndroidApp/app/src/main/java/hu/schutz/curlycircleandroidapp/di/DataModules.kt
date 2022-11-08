package hu.schutz.curlycircleandroidapp.di

import dagger.Module
import dagger.Provides
import dagger.hilt.InstallIn
import dagger.hilt.components.SingletonComponent
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
object RepositoryModule {

}