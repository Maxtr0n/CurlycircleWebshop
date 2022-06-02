using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class AddedProductCategoryImageUrlsColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrls",
                table: "ProductCategories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "235038dc-fa7b-4b9d-a25d-79bb36c9a4c3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "d82051e6-89da-42a5-83cf-9c54a341d7ce");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrls",
                value: "placeholder.jpg;placeholder2.jpeg;placeholder3.jpeg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrls",
                value: "placeholder.jpg;placeholder2.jpeg;placeholder3.jpeg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImageUrls",
                value: "placeholder.jpg;placeholder2.jpeg;placeholder3.jpeg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImageUrls",
                value: "placeholder.jpg;placeholder2.jpeg;placeholder3.jpeg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                column: "ImageUrls",
                value: "placeholder.jpg;placeholder2.jpeg;placeholder3.jpeg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                column: "ImageUrls",
                value: "placeholder.jpg;placeholder2.jpeg;placeholder3.jpeg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                column: "ImageUrls",
                value: "placeholder.jpg;placeholder2.jpeg;placeholder3.jpeg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                column: "ImageUrls",
                value: "placeholder.jpg;placeholder2.jpeg;placeholder3.jpeg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                column: "ImageUrls",
                value: "placeholder.jpg;placeholder2.jpeg;placeholder3.jpeg");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7611ff17-5cc0-4deb-baaf-bc727d2c705c", "AQAAAAEAACcQAAAAECTge9AfGaL2DUcJZ2PeJh6boh7jP+WjH0RGRSUtsrth/s4xfRptCopO0AuCIsFAmw==", "b04ea6fe-89e7-4c99-a62a-eb34c2e9aa3f" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f105aee8-4677-45a0-b0b1-153acf146721", "AQAAAAEAACcQAAAAEKF4E9uGscCHXKlRz4ciGQGJDcxgCKRVDC1rTx1goRl+/Tw9ceXOsMgkpRZ7ovHZ+A==", "33370e2b-99b8-4d9e-97e0-76f668de2366" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrls",
                table: "ProductCategories");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "2c47e30c-1a2c-4cae-b10e-82bb4ce94ddc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "acf67c42-cbba-477f-9780-29d3373d5785");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrls",
                value: null);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrls",
                value: null);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImageUrls",
                value: null);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImageUrls",
                value: null);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                column: "ImageUrls",
                value: null);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                column: "ImageUrls",
                value: null);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                column: "ImageUrls",
                value: null);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                column: "ImageUrls",
                value: null);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                column: "ImageUrls",
                value: null);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fb75df90-7707-42f9-8f12-3f7cdce7349d", "AQAAAAEAACcQAAAAEDgdYK/imWr81XC3XCOGLxZl01vp3qk4zrSlSEOSVweB9lHm2rtwvqH/FOMbnHG8ww==", "49872a3a-6899-4237-b8c5-62b8d6101bce" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d42ba653-47da-48b5-a4a6-3016eb314ef6", "AQAAAAEAACcQAAAAEIA5Yhe/p6J8mVx3bzstnNDNR8QShNb57O4FWDcE6VXeVkLDOwTJf1v36veb1wKT+w==", "b78bd386-fe6f-4c65-b803-84b911873ea1" });
        }
    }
}
