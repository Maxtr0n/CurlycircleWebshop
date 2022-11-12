package hu.schutz.curlycircleandroidapp.ui.shop

import androidx.navigation.NavController
import hu.schutz.curlycircleandroidapp.ui.shop.ShopDestinationArgs.PRODUCT_CATEGORY_ID_ARG
import hu.schutz.curlycircleandroidapp.ui.shop.ShopDestinationArgs.PRODUCT_ID_ARG
import hu.schutz.curlycircleandroidapp.ui.shop.ShopScreens.PRODUCTS_SCREEN
import hu.schutz.curlycircleandroidapp.ui.shop.ShopScreens.PRODUCT_CATEGORIES_SCREEN
import hu.schutz.curlycircleandroidapp.ui.shop.ShopScreens.PRODUCT_DETAILS_SCREEN

/**
 * Screens used in [ShopDestinations]
 */
private object ShopScreens {
    const val PRODUCT_CATEGORIES_SCREEN = "product-categories"
    const val PRODUCTS_SCREEN = "products"
    const val PRODUCT_DETAILS_SCREEN = "product"
}

/**
 * Arguments used in [ShopDestinations] routes
 */
object ShopDestinationArgs {
    const val PRODUCT_CATEGORY_ID_ARG = "productCategoryId"
    const val PRODUCT_ID_ARG = "productId"
}

/**
 * Destinations used in [ShopScreen]
 */
object ShopDestinations {
    const val PRODUCT_CATEGORIES_ROUTE = PRODUCT_CATEGORIES_SCREEN
    const val PRODUCTS_ROUTE = "$PRODUCTS_SCREEN/{$PRODUCT_CATEGORY_ID_ARG}"
    const val PRODUCT_DETAILS_ROUTE = "$PRODUCT_DETAILS_SCREEN/{$PRODUCT_ID_ARG}"
}

/**
 * Models the navigation actions in [ShopScreen]
 */
class ShopNavigationActions(private val navController: NavController) {

    fun navigateToProductCategoriesScreen() {
        navController.navigate(PRODUCT_CATEGORIES_SCREEN) {
            launchSingleTop = true
        }
    }

    fun navigateToProductsScreen(productCategoryId: Int) {
        navController.navigate("$PRODUCTS_SCREEN/$productCategoryId") {
            launchSingleTop = true
        }
    }

    fun navigateToProductDetailsScreen(productId: Int) {
        navController.navigate("$PRODUCT_DETAILS_SCREEN/$productId") {
            launchSingleTop = true
        }
    }
}

