package hu.schutz.curlycircleandroidapp

import androidx.annotation.StringRes
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.AccountCircle
import androidx.compose.material.icons.filled.Shop
import androidx.compose.material.icons.filled.ShoppingBag
import androidx.compose.ui.graphics.vector.ImageVector
import androidx.navigation.NavController
import androidx.navigation.NavGraph.Companion.findStartDestination
import hu.schutz.curlycircleandroidapp.CurlyCircleDestinations.ACCOUNT_ROUTE
import hu.schutz.curlycircleandroidapp.CurlyCircleDestinations.CART_ROUTE
import hu.schutz.curlycircleandroidapp.CurlyCircleDestinations.PROFILE_NAVIGATION_ROUTE
import hu.schutz.curlycircleandroidapp.CurlyCircleDestinations.SHOP_ROUTE
import hu.schutz.curlycircleandroidapp.CurlyCircleNavigationArgs.USER_MESSAGE_ARG
import hu.schutz.curlycircleandroidapp.Screens.ACCOUNT_SCREEN
import hu.schutz.curlycircleandroidapp.Screens.CART_SCREEN
import hu.schutz.curlycircleandroidapp.Screens.PROFILE_NAVIGATION
import hu.schutz.curlycircleandroidapp.Screens.REGISTRATION_SCREEN
import hu.schutz.curlycircleandroidapp.Screens.SHOP_SCREEN

sealed class BottomNavScreen(val route: String, @StringRes val resourceId: Int, val icon: ImageVector) {
    object Shop : BottomNavScreen(SHOP_ROUTE, R.string.bottom_navigation_shop, Icons.Default.Shop)
    object Cart : BottomNavScreen(CART_ROUTE, R.string.bottom_navigation_cart, Icons.Default.ShoppingBag)
    object Profile : BottomNavScreen(PROFILE_NAVIGATION_ROUTE, R.string.bottom_navigation_account, Icons.Default.AccountCircle)
}

private object Screens {
    const val PROFILE_NAVIGATION = "profile"
    const val ACCOUNT_SCREEN = "account"
    const val SHOP_SCREEN = "shop"
    const val CART_SCREEN = "cart"
    const val REGISTRATION_SCREEN = "registration"
}


object CurlyCircleNavigationArgs {
 const val USER_MESSAGE_ARG = "userMessage"
}

object CurlyCircleDestinations {
    const val PROFILE_NAVIGATION_ROUTE = PROFILE_NAVIGATION
    const val ACCOUNT_ROUTE = "$ACCOUNT_SCREEN?$USER_MESSAGE_ARG={$USER_MESSAGE_ARG}"
    const val SHOP_ROUTE = SHOP_SCREEN
    const val CART_ROUTE = CART_SCREEN
    const val REGISTRATION_ROUTE = REGISTRATION_SCREEN
}


val bottomNavScreens = listOf(
    BottomNavScreen.Shop,
    BottomNavScreen.Cart,
    BottomNavScreen.Profile
)

class CurlyCircleNavigationActions(private val navController: NavController) {

    fun navigateToRegistrationScreen() {
        navController.navigate(Screens.REGISTRATION_SCREEN) {
            launchSingleTop = true
        }
    }

    fun navigateBackToAccountScreen(userMessage: Int = 0) {
        navController.navigate(
            ACCOUNT_SCREEN.let {
                if (userMessage != 0) "$it?$USER_MESSAGE_ARG=$userMessage" else it
            }
        ) {
            popUpTo(navController.graph.findStartDestination().id)
        }
    }
}






