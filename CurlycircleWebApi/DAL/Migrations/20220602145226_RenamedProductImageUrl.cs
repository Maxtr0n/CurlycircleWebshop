using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class RenamedProductImageUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Products",
                newName: "ImageUrls");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrls",
                table: "Products",
                newName: "ImageUrl");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "76ac4f9d-fb49-4347-88e2-6cba8424ec8a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "3f8edcaf-77f5-4279-b0ee-b62a925bd616");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "95d62643-2238-4ca2-8116-aabd1d8ac54c", "AQAAAAEAACcQAAAAEHsmrmei/Pj7aHKEuPTWS1xIXAbnZnK30tGGisz679pM1Fn+98DvviEfLNGyroV9Cg==", "7843dcf0-4911-47fc-8499-decbc2cee779" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8ef2e743-217a-47af-84b8-1f38e97d3ed6", "AQAAAAEAACcQAAAAEGHG+HmiqAK1Y3p68C4gntgS6xm2YUWTtdbYKk+X0FkbdDseLQJxAKNg0GYiq0FO2Q==", "5c3eb1f2-c04c-4e88-b4c7-39a42dea6783" });
        }
    }
}
