package hu.schutz.curlycircleandroidapp.ui.shop.productcategories

import androidx.compose.foundation.clickable
import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.lazy.grid.GridCells
import androidx.compose.foundation.lazy.grid.LazyVerticalGrid
import androidx.compose.foundation.lazy.grid.items
import androidx.compose.foundation.lazy.items
import androidx.compose.foundation.shape.CircleShape
import androidx.compose.material.*
import androidx.compose.runtime.Composable
import androidx.compose.runtime.LaunchedEffect
import androidx.compose.runtime.getValue
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.draw.clip
import androidx.compose.ui.res.painterResource
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp
import androidx.hilt.navigation.compose.hiltViewModel
import androidx.lifecycle.compose.ExperimentalLifecycleComposeApi
import androidx.lifecycle.compose.collectAsStateWithLifecycle
import coil.compose.AsyncImage
import hu.schutz.curlycircleandroidapp.R
import hu.schutz.curlycircleandroidapp.data.ProductCategory
import hu.schutz.curlycircleandroidapp.ui.theme.CurlyCircleAndroidAppTheme
import hu.schutz.curlycircleandroidapp.util.Constants.API_BASE_URL
import hu.schutz.curlycircleandroidapp.util.Constants.PRODUCT_CATEGORIES_THUMBNAILS_URL
import hu.schutz.curlycircleandroidapp.util.LoadingContent

@OptIn(ExperimentalLifecycleComposeApi::class)
@Composable
fun ProductCategoriesScreen(
    onProductCategoryClick: (Int) -> Unit,
    scaffoldState: ScaffoldState,
    viewModel: ProductCategoriesViewModel = hiltViewModel(),
) {
    val uiState by viewModel.uiState.collectAsStateWithLifecycle()

    ProductCategoriesContent(
        loading = uiState.isLoading,
        empty = uiState.productCategories.isEmpty() && !uiState.isLoading,
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
fun ProductCategoriesContent(
    loading: Boolean,
    empty: Boolean,
    productCategories: List<ProductCategory>,
    onProductCategoryClick: (Int) -> Unit,
    modifier: Modifier = Modifier
) {
    val screenPadding = Modifier.padding(
        horizontal = 16.dp
    )

    val commonModifier = modifier
        .fillMaxWidth()
        .then(screenPadding)

    LoadingContent(
        loading = loading, 
        empty = empty, 
        emptyContent = { 
            Text(
                text = stringResource(R.string.no_data_product_categories),
                modifier = commonModifier
            )
        }) {
        Column(
            modifier = commonModifier
        ) {
            Text(
                text = stringResource(R.string.product_categories_label),
                modifier = Modifier.padding(
                    top = 16.dp,
                    bottom = 8.dp,
                    start = 8.dp,
                    end = 8.dp
                ),
                style = MaterialTheme.typography.h5
            )
            LazyVerticalGrid(
                columns = GridCells.Adaptive(minSize = 256.dp),
                verticalArrangement = Arrangement.spacedBy(8.dp),
                horizontalArrangement = Arrangement.spacedBy(8.dp),
                contentPadding = PaddingValues(vertical = 8.dp),
                modifier = Modifier.fillMaxSize()
            ) {
                items(productCategories) { productCategory ->
                    ProductCategoryItem(
                        productCategory = productCategory,
                        onProductCategoryClick = onProductCategoryClick
                    )
                }
            }
        }
    }
}

@Composable
fun ProductCategoryItem(
    productCategory: ProductCategory,
    onProductCategoryClick: (Int) -> Unit
) {
    Card(
        modifier = Modifier
            .clickable { onProductCategoryClick(productCategory.id) }
    ) {
        Column(
            modifier = Modifier
                .padding(
                    horizontal = 16.dp,
                    vertical = 8.dp
                ),
            horizontalAlignment = Alignment.Start
        ) {
            Text(
                text = productCategory.name,
                style = MaterialTheme.typography.h6,
                )
            AsyncImage(
                model =  API_BASE_URL + PRODUCT_CATEGORIES_THUMBNAILS_URL +
                        productCategory.thumbnailImageUrl,
                contentDescription = "${productCategory.name} termék kategória képe.",
                placeholder = painterResource(id = R.drawable.placeholder3),
                error = painterResource(id = R.drawable.placeholder3),
                fallback = painterResource(id = R.drawable.placeholder3),
                modifier = Modifier
                    .padding(vertical = 8.dp)
                    .clip(MaterialTheme.shapes.medium)
            )
            Text(
                text = productCategory.description,
                style = MaterialTheme.typography.body1
            )
        }
    }
}

@Preview
@Composable
private fun ProductCategoriesContentPreview() {
    CurlyCircleAndroidAppTheme {
        Surface {
            ProductCategoriesContent(
                loading = false,
                empty = false,
                productCategories = listOf(
                    ProductCategory(id = 1, name = "Name1", description = "Lorem ipsum 1"),
                    ProductCategory(id = 2, name = "Name2", description = "Lorem ipsum 2"),
                    ProductCategory(id = 3, name = "Name3", description = "Lorem ipsum 3"),
                    ProductCategory(id = 4, name = "Name4", description = "Lorem ipsum 4"),
                    ProductCategory(id = 5, name = "Name5", description = "Lorem ipsum 5"),
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
                productCategory = ProductCategory(id = 1, name = "Curlyk", description = "Description lorum ipsum"),
                onProductCategoryClick = { }
            )
        }
    }
}

