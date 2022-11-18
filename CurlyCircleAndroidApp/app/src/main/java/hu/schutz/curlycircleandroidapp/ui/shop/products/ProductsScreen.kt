package hu.schutz.curlycircleandroidapp.ui.shop.products

import androidx.compose.foundation.clickable
import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.grid.GridCells
import androidx.compose.foundation.lazy.grid.LazyVerticalGrid
import androidx.compose.foundation.lazy.grid.items
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
import hu.schutz.curlycircleandroidapp.data.Product
import hu.schutz.curlycircleandroidapp.ui.components.LoadingContent
import hu.schutz.curlycircleandroidapp.ui.theme.CurlyCircleAndroidAppTheme
import hu.schutz.curlycircleandroidapp.util.Constants

@OptIn(ExperimentalLifecycleComposeApi::class)
@Composable
fun ProductsScreen(
    onProductClick: (Int) -> Unit,
    scaffoldState: ScaffoldState,
    viewModel: ProductsViewModel = hiltViewModel()
) {
    val uiState by viewModel.uiState.collectAsStateWithLifecycle()

    ProductsContent(
        loading = uiState.isLoading,
        empty = uiState.products.isEmpty() && !uiState.isLoading,
        products = uiState.products,
        onProductClick = onProductClick
    )

    uiState.userMessage?.let { message ->
        val snackBarText = stringResource(id = message)
        LaunchedEffect(scaffoldState, viewModel, message, snackBarText) {
            scaffoldState.snackbarHostState.showSnackbar(snackBarText)
            viewModel.snackBarMessageShown()
        }
    }
}

@Composable
fun ProductsContent(
    loading: Boolean,
    empty: Boolean,
    products: List<Product>,
    onProductClick: (Int) -> Unit,
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
            Box(
                modifier = commonModifier.fillMaxSize(),
                contentAlignment = Alignment.Center
            ) {
                Text(
                    text = stringResource(R.string.no_data_products),
                    modifier = Modifier.align(Alignment.Center)
                )
            }
        }) {
        Column(modifier = commonModifier) {
            Text(
                text = stringResource(R.string.products_label),
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
                items(products) { product ->
                    ProductItem(
                        product = product,
                        onProductClick = onProductClick
                    )
                }
            }
        }
    }
}

@Composable
fun ProductItem(
    product: Product,
    onProductClick: (Int) -> Unit
) {
    Card(
      modifier = Modifier
          .clickable { onProductClick(product.id) }
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
                text = product.name,
                style = MaterialTheme.typography.h6,
            )
            AsyncImage(
                model = if (product.thumbnailImageUrl == "") Constants.API_BASE_URL
                        + Constants.NO_IMAGE_URL
                    else Constants.API_BASE_URL + Constants.PRODUCT_THUMBNAILS_URL +
                        product.thumbnailImageUrl,
                contentDescription = "${product.name} termék kategória képe.",
                placeholder = painterResource(id = R.drawable.placeholder3),
                error = painterResource(id = R.drawable.placeholder3),
                fallback = painterResource(id = R.drawable.placeholder3),
                modifier = Modifier
                    .padding(vertical = 8.dp)
                    .clip(MaterialTheme.shapes.medium)
            )
            Text(
                text = product.description,
                style = MaterialTheme.typography.body1
            )
        }
    }
}

@Preview
@Composable
private fun ProductsContentPreview() {
    CurlyCircleAndroidAppTheme {
        Surface {
            ProductsContent(
                loading = false,
                empty = false,
                products = listOf(
                    Product(id = 1, name = "Name1", description = "Lorem ipsum 1", price = 2000.0,
                    productCategoryId = 1),
                    Product(id = 2, name = "Name2", description = "Lorem ipsum 2", price = 2000.0,
                        productCategoryId = 1),
                    Product(id = 3, name = "Name3", description = "Lorem ipsum 3", price = 2000.0,
                        productCategoryId = 1),
                    Product(id = 4, name = "Name4", description = "Lorem ipsum 4", price = 2000.0,
                        productCategoryId = 1),
                    Product(id = 5, name = "Name5", description = "Lorem ipsum 5", price = 2000.0,
                        productCategoryId = 1),
                ),
                onProductClick = {}
            )
        }
    }
}

@Preview
@Composable
private fun ProductsContentEmptyPreview() {
    CurlyCircleAndroidAppTheme {
        Surface {
            ProductsContent(
                loading = false,
                empty = true,
                products = listOf(),
                onProductClick = {}
            )
        }
    }
}


@Preview
@Composable
private fun ProductItemPreview() {
    CurlyCircleAndroidAppTheme {
        Surface {
            ProductItem(
                product = Product(
                    id = 1,
                    name = "Curly1",
                    description = "Description lorum ipsum",
                    price = 2000.0,
                    productCategoryId = 1
                ),
                onProductClick = { }
            )
        }
    }
}

