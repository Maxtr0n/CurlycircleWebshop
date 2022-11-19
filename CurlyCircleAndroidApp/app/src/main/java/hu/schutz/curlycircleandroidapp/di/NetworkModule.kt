package hu.schutz.curlycircleandroidapp.di

import com.google.gson.GsonBuilder
import dagger.Module
import dagger.Provides
import dagger.hilt.InstallIn
import dagger.hilt.components.SingletonComponent
import hu.schutz.curlycircleandroidapp.data.SessionManager
import hu.schutz.curlycircleandroidapp.data.repository.AuthRepository
import hu.schutz.curlycircleandroidapp.data.repository.DefaultAuthRepository
import hu.schutz.curlycircleandroidapp.data.source.remote.AuthApi
import hu.schutz.curlycircleandroidapp.data.source.remote.CurlyCircleApi
import hu.schutz.curlycircleandroidapp.util.TokenAuthenticator
import hu.schutz.curlycircleandroidapp.util.TokenInterceptor
import okhttp3.OkHttpClient
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory
import javax.inject.Singleton

@Module
@InstallIn(SingletonComponent::class)
object NetworkModule {

    @Singleton
    @Provides
    fun provideCurlyCircleOkHttpClient(
        tokenInterceptor: TokenInterceptor,
        tokenAuthenticator: TokenAuthenticator
    ): OkHttpClient = OkHttpClient.Builder()
        .addInterceptor(tokenInterceptor)
        .authenticator(tokenAuthenticator)
        .build()

    @Provides
    @Singleton
    fun provideCurlyCircleApi(
        okHttpClient: OkHttpClient
    ): CurlyCircleApi {
        return Retrofit.Builder()
            .baseUrl(CurlyCircleApi.BASE_URL)
            .client(okHttpClient)
            .addConverterFactory(
                GsonConverterFactory
                    .create(GsonBuilder().setDateFormat("yyyy-MM-dd'T'HH:mm:ss").create())
            )
            .build()
            .create(CurlyCircleApi::class.java)
    }

    @Provides
    @Singleton
    fun provideAuthApi(): AuthApi {
        return Retrofit.Builder()
            .baseUrl(CurlyCircleApi.BASE_URL)
            .addConverterFactory(
                GsonConverterFactory
                    .create(GsonBuilder().setDateFormat("yyyy-MM-dd'T'HH:mm:ss").create())
            )
            .build()
            .create(AuthApi::class.java)
    }


    @Singleton
    @Provides
    fun provideSessionManager(
        authRepository: AuthRepository
    ): SessionManager = SessionManager(
        authRepository = authRepository
    )


    @Singleton
    @Provides
    fun provideTokenInterceptor(
        sessionManager: SessionManager
    ): TokenInterceptor = TokenInterceptor(sessionManager = sessionManager)

    @Singleton
    @Provides
    fun provideTokenAuthenticator(
        sessionManager: SessionManager
    ): TokenAuthenticator = TokenAuthenticator(sessionManager = sessionManager)

}