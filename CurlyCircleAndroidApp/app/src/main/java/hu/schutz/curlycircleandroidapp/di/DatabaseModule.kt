package hu.schutz.curlycircleandroidapp.di

import android.content.Context
import androidx.room.Room
import dagger.Module
import dagger.Provides
import dagger.hilt.InstallIn
import dagger.hilt.android.qualifiers.ApplicationContext
import dagger.hilt.components.SingletonComponent
import hu.schutz.curlycircleandroidapp.data.source.local.CurlyCircleDatabase
import javax.inject.Singleton

@Module
@InstallIn(SingletonComponent::class)
object DatabaseModule {

    @Singleton
    @Provides
    fun provideDataBase(@ApplicationContext context: Context): CurlyCircleDatabase {
        return Room.databaseBuilder(
            context.applicationContext,
            CurlyCircleDatabase::class.java,
            "CurlyCircle.db"
        ).build()
    }
}