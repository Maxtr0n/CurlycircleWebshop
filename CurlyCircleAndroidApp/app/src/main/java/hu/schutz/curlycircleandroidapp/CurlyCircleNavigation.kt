package hu.schutz.curlycircleandroidapp

import androidx.annotation.StringRes
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.AccountCircle
import androidx.compose.material.icons.filled.Shop
import androidx.compose.material.icons.filled.ShoppingBag
import androidx.compose.ui.graphics.vector.ImageVector
import androidx.navigation.NavController
import hu.schutz.curlycircleandroidapp.Screens.REGISTRATION_SCREEN

sealed class BottomNavScreen(val route: String, @StringRes val resourceId: Int, val icon: ImageVector) {
    object Shop : BottomNavScreen("shop", R.string.bottom_navigation_shop, Icons.Default.Shop)
    object Cart : BottomNavScreen("cart", R.string.bottom_navigation_cart, Icons.Default.ShoppingBag)
    object Account : BottomNavScreen("account", R.string.bottom_navigation_account, Icons.Default.AccountCircle)
}

private object Screens {
    const val REGISTRATION_SCREEN = "registration"
}


object CurlyCircleNavigationArgs {

}

object CurlyCircleDestinations {
    const val REGISTRATION_ROUTE = REGISTRATION_SCREEN
}


val bottomNavScreens = listOf(
    BottomNavScreen.Shop,
    BottomNavScreen.Cart,
    BottomNavScreen.Account
)

class CurlyCircleNavigationActions(private val navController: NavController) {

    fun navigateToRegistrationScreen() {
        navController.navigate(Screens.REGISTRATION_SCREEN) {
            launchSingleTop = true
        }
    }

    fun navigateBackToAccountScreen() {
        navController.navigateUp()
    }
}






