package hu.schutz.curlycircleandroidapp

import androidx.annotation.StringRes
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.AccountCircle
import androidx.compose.material.icons.filled.Shop
import androidx.compose.material.icons.filled.ShoppingBag
import androidx.compose.ui.graphics.vector.ImageVector
import androidx.navigation.NavController
import androidx.navigation.NavGraph.Companion.findStartDestination
import hu.schutz.curlycircleandroidapp.CurlyCircleDestinations.CART_ROUTE
import hu.schutz.curlycircleandroidapp.CurlyCircleDestinations.PROFILE_NAVIGATION_ROUTE
import hu.schutz.curlycircleandroidapp.CurlyCircleDestinations.SHOP_ROUTE
import hu.schutz.curlycircleandroidapp.CurlyCircleNavigationArgs.PRODUCT_CATEGORY_ID_ARG
import hu.schutz.curlycircleandroidapp.CurlyCircleNavigationArgs.PRODUCT_ID_ARG
import hu.schutz.curlycircleandroidapp.CurlyCircleNavigationArgs.USER_MESSAGE_ARG
import hu.schutz.curlycircleandroidapp.Screens.ACCOUNT_SCREEN
import hu.schutz.curlycircleandroidapp.Screens.CART_SCREEN
import hu.schutz.curlycircleandroidapp.Screens.PRODUCTS_SCREEN
import hu.schutz.curlycircleandroidapp.Screens.PRODUCT_CATEGORIES_SCREEN
import hu.schutz.curlycircleandroidapp.Screens.PRODUCT_DETAILS_SCREEN
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
    const val PRODUCT_CATEGORIES_SCREEN = "product-categories"
    const val PRODUCTS_SCREEN = "products"
    const val PRODUCT_DETAILS_SCREEN = "product"
}


object CurlyCircleNavigationArgs {
    const val USER_MESSAGE_ARG = "userMessage"
    const val PRODUCT_CATEGORY_ID_ARG = "productCategoryId"
    const val PRODUCT_ID_ARG = "productId"
}

object CurlyCircleDestinations {
    const val PROFILE_NAVIGATION_ROUTE = PROFILE_NAVIGATION
    const val ACCOUNT_ROUTE = "$ACCOUNT_SCREEN?$USER_MESSAGE_ARG={$USER_MESSAGE_ARG}"
    const val SHOP_ROUTE = SHOP_SCREEN
    const val CART_ROUTE = CART_SCREEN
    const val REGISTRATION_ROUTE = REGISTRATION_SCREEN

    // Shop destinations
    const val PRODUCT_CATEGORIES_ROUTE = PRODUCT_CATEGORIES_SCREEN
    const val PRODUCTS_ROUTE = "${PRODUCTS_SCREEN}/{${PRODUCT_CATEGORY_ID_ARG}}"
    const val PRODUCT_DETAILS_ROUTE = "${PRODUCT_DETAILS_SCREEN}/{${PRODUCT_ID_ARG}}"
}


val bottomNavScreens = listOf(
    BottomNavScreen.Shop,
    BottomNavScreen.Cart,
    BottomNavScreen.Profile
)

class CurlyCircleNavigationActions(private val navController: NavController) {

    fun navigateToProductCategoriesScreen() {
        navController.navigate(PRODUCT_CATEGORIES_SCREEN) {
            launchSingleTop = true
        }
    }

    fun navigateToProductsScreen(productCategoryId: Int) {
        navController.navigate("${PRODUCTS_SCREEN}/$productCategoryId") {
            launchSingleTop = true
        }
    }

    fun navigateToProductDetailsScreen(productId: Int) {
        navController.navigate("${PRODUCT_DETAILS_SCREEN}/$productId") {
            launchSingleTop = true
        }
    }

    fun navigateToRegistrationScreen() {
        navController.navigate(REGISTRATION_SCREEN) {
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






