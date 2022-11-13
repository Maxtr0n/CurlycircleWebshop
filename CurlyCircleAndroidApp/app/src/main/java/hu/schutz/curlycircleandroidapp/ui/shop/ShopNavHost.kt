package hu.schutz.curlycircleandroidapp.ui.shop

import androidx.compose.material.ScaffoldState
import androidx.compose.runtime.Composable
import androidx.compose.runtime.remember
import androidx.compose.ui.Modifier
import androidx.navigation.NavHostController
import androidx.navigation.NavType
import androidx.navigation.compose.NavHost
import androidx.navigation.compose.composable
import androidx.navigation.compose.rememberNavController
import androidx.navigation.navArgument
import hu.schutz.curlycircleandroidapp.ui.shop.ShopDestinationArgs.PRODUCT_CATEGORY_ID_ARG
import hu.schutz.curlycircleandroidapp.ui.shop.ShopDestinationArgs.PRODUCT_ID_ARG
import hu.schutz.curlycircleandroidapp.ui.shop.productcategories.ProductCategoriesScreen
import hu.schutz.curlycircleandroidapp.ui.shop.productdetails.ProductDetailsScreen
import hu.schutz.curlycircleandroidapp.ui.shop.products.ProductsScreen

@Composable
fun ShopNavHost(
    scaffoldState: ScaffoldState,
    modifier: Modifier = Modifier,
    navController: NavHostController = rememberNavController(),
    startDestination: String = ShopDestinations.PRODUCT_CATEGORIES_ROUTE,
    navActions: ShopNavigationActions = remember(navController) {
        ShopNavigationActions(navController)
    }
) {
    NavHost(
        navController = navController,
        startDestination = startDestination,
        modifier = modifier
    ) {
        composable(
            route = ShopDestinations.PRODUCT_CATEGORIES_ROUTE
        ) {
            ProductCategoriesScreen(
                scaffoldState = scaffoldState,
                onProductCategoryClick = { id -> navActions.navigateToProductsScreen(id) }
            )
        }

        composable(
            route = ShopDestinations.PRODUCTS_ROUTE,
            arguments = listOf(
                navArgument(PRODUCT_CATEGORY_ID_ARG) { type = NavType.IntType }
            )
        ) { backStackEntry ->
            ProductsScreen(
                scaffoldState = scaffoldState,
                onProductClick = { id -> navActions.navigateToProductDetailsScreen(id) }
            )
        }

        composable(
            route = ShopDestinations.PRODUCT_DETAILS_ROUTE,
            arguments = listOf(
                navArgument(PRODUCT_ID_ARG) { type = NavType.IntType }
            )
        ) { backStackEntry ->
            ProductDetailsScreen(
                scaffoldState = scaffoldState,
            )
        }
    }
}
