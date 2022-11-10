package hu.schutz.curlycircleandroidapp.ui

import androidx.compose.foundation.clickable
import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.lazy.items
import androidx.compose.material.*
import androidx.compose.runtime.Composable
import androidx.compose.runtime.LaunchedEffect
import androidx.compose.runtime.getValue
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp
import androidx.hilt.navigation.compose.hiltViewModel
import androidx.lifecycle.compose.ExperimentalLifecycleComposeApi
import androidx.lifecycle.compose.collectAsStateWithLifecycle
import hu.schutz.curlycircleandroidapp.R
import hu.schutz.curlycircleandroidapp.data.ProductCategory
import hu.schutz.curlycircleandroidapp.ui.shop.ShopViewModel
import hu.schutz.curlycircleandroidapp.ui.theme.CurlyCircleAndroidAppTheme

@OptIn(ExperimentalLifecycleComposeApi::class)
@Composable
fun ShopScreen(
    onProductCategoryClick: (ProductCategory) -> Unit,
    modifier: Modifier = Modifier,
    scaffoldState: ScaffoldState = rememberScaffoldState(),
    viewModel: ShopViewModel = hiltViewModel(),
) {
    val uiState by viewModel.uiState.collectAsStateWithLifecycle()

    ShopContent(
        loading = uiState.isLoading,
        productCategories = uiState.productCategories,
        onProductCategoryClick = onProductCategoryClick,
    )

    uiState.userMessage?.let { message ->
        val snackBarText = stringResource(id = message)
        LaunchedEffect(scaffoldState, viewModel, message, snackBarText) {
            scaffoldState.snackbarHostState.showSnackbar(snackBarText)
            viewModel.snackbarMessageShown()
        }
    }
}

@Composable
fun ShopContent(
    loading: Boolean,
    productCategories: List<ProductCategory>,
    onProductCategoryClick: (ProductCategory) -> Unit,
    modifier: Modifier = Modifier
) {
    Column(
        modifier = modifier
            .fillMaxSize()
            .padding(horizontal = 16.dp)
    ) {
        Text(
            text = stringResource(R.string.product_categories_label),
            modifier = Modifier.padding(
                horizontal = 8.dp,
                vertical = 16.dp
            ),
            style = MaterialTheme.typography.h5
        )
        LazyColumn {
            items(productCategories) { productCategory ->
                ProductCategoryItem(
                    productCategory = productCategory,
                    onProductCategoryClick = onProductCategoryClick
                )
            }
        }
    }
}

@Composable
fun ProductCategoryItem(
    productCategory: ProductCategory,
    onProductCategoryClick: (ProductCategory) -> Unit
) {
    Row(
        verticalAlignment = Alignment.CenterVertically,
        modifier = Modifier
            .fillMaxWidth()
            .padding(
                horizontal = 16.dp,
                vertical = 8.dp
            )
            .clickable { onProductCategoryClick(productCategory) }
    ) {
        Text(
            text = productCategory.name,
            style = MaterialTheme.typography.h6,

        )
    }
}

@Preview
@Composable
private fun ShopContentPreview() {
    CurlyCircleAndroidAppTheme {
        Surface {
            ShopContent(
                loading = false,
                productCategories = listOf(
                    ProductCategory(name = "Name1", description = "Lorem ipsum 1"),
                    ProductCategory(name = "Name2", description = "Lorem ipsum 2"),
                    ProductCategory(name = "Name3", description = "Lorem ipsum 3"),
                    ProductCategory(name = "Name4", description = "Lorem ipsum 4"),
                    ProductCategory(name = "Name5", description = "Lorem ipsum 5"),
                ),
                onProductCategoryClick = {}
            )
        }
    }
}

@Preview
@Composable
private fun ProductCategoryItemPreview() {
    CurlyCircleAndroidAppTheme {
        Surface {
            ProductCategoryItem(
                productCategory = ProductCategory(name = "Curlyk", description = "Description lorum ipsum"),
                onProductCategoryClick = { }
            )
        }
    }
}

/*
@Preview
@Composable
fun ShopScreenPreview() {
    CurlyCircleAndroidAppTheme {
        ShopScreen()
    }
}
*/
