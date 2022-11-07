package hu.schutz.curlycircleandroidapp

import androidx.annotation.StringRes
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.AccountCircle
import androidx.compose.material.icons.filled.Shop
import androidx.compose.material.icons.filled.ShoppingBag
import androidx.compose.ui.graphics.vector.ImageVector

sealed class Screen(val route: String, @StringRes val resourceId: Int, val icon: ImageVector) {
    object Shop : Screen("shop", R.string.bottom_navigation_shop, Icons.Default.Shop)
    object Cart : Screen("cart", R.string.bottom_navigation_cart, Icons.Default.ShoppingBag)
    object Account : Screen("account", R.string.bottom_navigation_account, Icons.Default.AccountCircle)
}

val bottomNavScreens = listOf(
    Screen.Shop,
    Screen.Cart,
    Screen.Account
)