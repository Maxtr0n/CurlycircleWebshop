using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class ThumbnailImageUrlAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrls",
                table: "ProductCategories",
                newName: "ThumbnailImageUrl");

            migrationBuilder.AddColumn<string>(
                name: "ThumbnailImageUrl",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "8edc03ae-3425-4d56-bcf0-088d6cf5d397");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "c496372b-fd0d-4f4f-ba34-b362c2825ef6");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "ThumbnailImageUrl",
                value: "placeholder.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "ThumbnailImageUrl",
                value: "placeholder.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "ThumbnailImageUrl",
                value: "");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "ThumbnailImageUrl",
                value: "placeholder.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "ThumbnailImageUrl",
                value: "placeholder.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                column: "ThumbnailImageUrl",
                value: "placeholder.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                column: "ThumbnailImageUrl",
                value: "placeholder.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                column: "ThumbnailImageUrl",
                value: "placeholder.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                column: "ThumbnailImageUrl",
                value: "placeholder.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                column: "ThumbnailImageUrl",
                value: "placeholder.jpg");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "26d31aec-4d3a-42dc-a086-21f4aa6b37a3", "AQAAAAEAACcQAAAAEFFyTN5nEdasBYOtGHU0bChj5xBdv/9eVfwiniaKWk1Axg5Ru0uqzOYsY3FW/uKXjQ==", "b99e1c70-9305-4072-bab3-66a58958b50f" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2e427c16-1706-4a99-afac-d84f5eca05b8", "AQAAAAEAACcQAAAAEH+FSAssjZX1th+jEa8WWgeESBx58yIs+nJEabr5geTFfq6/dF7gN5qCUd4RhQrr8g==", "5c410e62-9b86-4e4d-a3ea-3b8c6220ee37" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThumbnailImageUrl",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "ThumbnailImageUrl",
                table: "ProductCategories",
                newName: "ImageUrls");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "8560bf5b-b1ba-478b-97c9-7f130422a7d4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "2a5ada19-ea3b-4ab9-811a-a7951d58bbc2");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2cf3606b-c69a-49ab-a9f4-e15e3362f87b", "AQAAAAEAACcQAAAAEETWfFwkHCkUH6tDMmN78/XLxDtODzQ9lH5mZ8oAfcqfcDrqW2vDI1sBx99gW4JyAQ==", "9efb42a0-a06e-4153-ba33-655f05f8cf5c" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cccc03fc-7c3e-4cc7-85ef-ad10bc8a3ad8", "AQAAAAEAACcQAAAAEMVcpuVVwd8568Qs1MJ1q6DFCJGLqVCecjMUFEv0/T1NjDVwvKJWGPdPts0iykj5uw==", "33c4dda7-1044-4b2b-97ea-52706cc81b35" });
        }
    }
}
