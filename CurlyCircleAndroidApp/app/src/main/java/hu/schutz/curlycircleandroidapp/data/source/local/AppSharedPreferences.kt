package hu.schutz.curlycircleandroidapp.data.source.local

import android.content.SharedPreferences
import javax.inject.Inject

class AppSharedPreferences @Inject constructor(
    private val sharedPreferences: SharedPreferences
) {
    fun getCardId(): Int = sharedPreferences.getInt(CART_ID_KEY, 0)

    fun setCartId(cartId: Int) {
        sharedPreferences.edit().putInt(CART_ID_KEY, cartId).apply()
    }

    companion object {
        const val SHARED_PREFS = "APP_SHARED_PREFS"
        const val CART_ID_KEY = "cartId"
    }
}