package hu.schutz.curlycircleandroidapp.data

import androidx.room.Entity
import androidx.room.PrimaryKey

@Entity(tableName = "productCategories")
data class ProductCategory(
    var name: String = "",
    var description: String = "",
    var thumbnailImageUrl: String = "",
    var isAvailable: Boolean = true,
    @PrimaryKey var id: String = ""
    ) {

}