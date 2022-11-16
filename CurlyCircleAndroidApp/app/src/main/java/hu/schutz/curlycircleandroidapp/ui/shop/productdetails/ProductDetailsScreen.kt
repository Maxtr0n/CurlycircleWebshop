package hu.schutz.curlycircleandroidapp.ui.shop.productdetails

import androidx.compose.foundation.layout.*
import androidx.compose.material.*
import androidx.compose.runtime.Composable
import androidx.compose.runtime.LaunchedEffect
import androidx.compose.runtime.getValue
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.draw.clip
import androidx.compose.ui.res.painterResource
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp
import androidx.hilt.navigation.compose.hiltViewModel
import androidx.lifecycle.compose.ExperimentalLifecycleComposeApi
import androidx.lifecycle.compose.collectAsStateWithLifecycle
import coil.compose.AsyncImage
import hu.schutz.curlycircleandroidapp.R
import hu.schutz.curlycircleandroidapp.data.Product
import hu.schutz.curlycircleandroidapp.ui.theme.CurlyCircleAndroidAppTheme
import hu.schutz.curlycircleandroidapp.util.Constants
import hu.schutz.curlycircleandroidapp.util.LoadingContent

@OptIn(ExperimentalLifecycleComposeApi::class)
@Composable
fun ProductDetailsScreen(
    scaffoldState: ScaffoldState,
    viewModel: ProductDetailsViewModel = hiltViewModel()
) {
    val uiState by viewModel.uiState.collectAsStateWithLifecycle()

    ProductDetailsContent(
        loading = uiState.isLoading,
        product = uiState.product
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
fun ProductDetailsContent(
    loading: Boolean,
    product: Product?,
    modifier: Modifier = Modifier
) {
    val screenPadding = Modifier.padding(
        vertical = 16.dp,
        horizontal = 16.dp
    )

    val commonModifier = modifier
        .fillMaxSize()
        .then(screenPadding)

    LoadingContent(
        loading = loading,
        empty = product == null,
        emptyContent = {
            Box(
                modifier = commonModifier,
                contentAlignment = Alignment.Center
            ) {
                Text(
                    text = stringResource(R.string.no_data_product),
                    modifier = Modifier.align(Alignment.Center)
                )
            }
        }
    ) {
        Column(modifier = commonModifier) { 
            Text(
                text = product!!.name,
                style = MaterialTheme.typography.h5
            )
            AsyncImage(
                model = if (product.imageUrls.isEmpty()) Constants.API_BASE_URL
                        + Constants.NO_IMAGE_URL
                        else Constants.API_BASE_URL + Constants.PRODUCT_IMAGES_URL +
                        product.imageUrls[0],
                contentDescription = "${product.name} termék kategória képe.",
                placeholder = painterResource(id = R.drawable.placeholder3),
                error = painterResource(id = R.drawable.placeholder3),
                fallback = painterResource(id = R.drawable.placeholder3),
                modifier = Modifier
                    .padding(vertical = 8.dp)
                    .clip(MaterialTheme.shapes.medium)
            )
            Text(
                text = product.price.toInt().toString() + " Ft",
                style = MaterialTheme.typography.h5
            )
            Row(
                modifier = Modifier.fillMaxWidth(),
                horizontalArrangement = Arrangement.SpaceBetween
            ) {
                Button(onClick = { /*TODO*/ }) {
                    Text(text = stringResource(R.string.to_cart_button_text))
                }
            }
            Text(
                text = product.description,
                style = MaterialTheme.typography.body1
            )
        }
    }
}

@Preview
@Composable
fun ProductDetailsPreview() {
    CurlyCircleAndroidAppTheme {
        Surface {
            ProductDetailsContent(
                loading = false,
                product = Product(id = 1, name = "Curly1", description = "Lorem ipsum 1",
                productCategoryId = 1, price = 2500.0)
            )
        }
    }
}