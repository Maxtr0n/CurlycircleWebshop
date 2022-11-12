package hu.schutz.curlycircleandroidapp

import androidx.annotation.StringRes
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.AccountCircle
import androidx.compose.material.icons.filled.Shop
import androidx.compose.material.icons.filled.ShoppingBag
import androidx.compose.ui.graphics.vector.ImageVector

sealed class BottomNavScreen(val route: String, @StringRes val resourceId: Int, val icon: ImageVector) {
    object Shop : BottomNavScreen("shop", R.string.bottom_navigation_shop, Icons.Default.Shop)
    object Cart : BottomNavScreen("cart", R.string.bottom_navigation_cart, Icons.Default.ShoppingBag)
    object Account : BottomNavScreen("account", R.string.bottom_navigation_account, Icons.Default.AccountCircle)
}

val bottomNavScreens = listOf(
    BottomNavScreen.Shop,
    BottomNavScreen.Cart,
    BottomNavScreen.Account
)