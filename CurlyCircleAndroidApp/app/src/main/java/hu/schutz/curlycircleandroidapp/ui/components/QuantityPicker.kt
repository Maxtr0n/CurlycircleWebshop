package hu.schutz.curlycircleandroidapp.ui.components

import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.padding
import androidx.compose.material.*
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Add
import androidx.compose.material.icons.filled.Remove
import androidx.compose.runtime.Composable
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.text.style.TextAlign
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp
import hu.schutz.curlycircleandroidapp.ui.theme.CurlyCircleAndroidAppTheme

@Composable
fun QuantityPicker(
    quantity: Int,
    increaseQuantity: () -> Unit,
    decreaseQuantity: () -> Unit,
    modifier: Modifier = Modifier
) {
    Row(
        verticalAlignment = Alignment.CenterVertically,
        horizontalArrangement = Arrangement.Center,
        modifier = modifier
    ) {
        IconButton(onClick = decreaseQuantity, modifier = Modifier.padding(4.dp).weight(1f),
            quantity > 1) {
            Icon(
                imageVector = Icons.Default.Remove,
                contentDescription = "Mennyiség csökkentése.",
                //tint = MaterialTheme.colors.primary
            )
        }

        Text(text = "$quantity db", modifier = Modifier.padding(4.dp).weight(1f),
            textAlign = TextAlign.Center, style = MaterialTheme.typography.body1)

        IconButton(onClick = increaseQuantity, modifier = Modifier.padding(4.dp).weight(1f),
            enabled = quantity < 10) {
            Icon(
                imageVector = Icons.Default.Add,
                contentDescription = "Mennyiség növelése.",
                //tint = MaterialTheme.colors.primary
            )
        }
    }
}

@Preview
@Composable
private fun QuantityPickerReview() {
    CurlyCircleAndroidAppTheme {
        Surface {
            QuantityPicker(quantity = 1, increaseQuantity = {}, decreaseQuantity = {})
        }
    }
}