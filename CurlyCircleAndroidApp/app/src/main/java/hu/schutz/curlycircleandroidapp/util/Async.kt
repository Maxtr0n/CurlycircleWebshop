package hu.schutz.curlycircleandroidapp.util

import hu.schutz.curlycircleandroidapp.data.Result

/**
 * A generic class that holds a loading signal or a [Result].
 * Taken from: Taken from: https://github.com/android/architecture-samples
 */
sealed class Async<out T> {
    object Loading : Async<Nothing>()
    data class Success<out T>(val data: T) : Async<T>()
}
