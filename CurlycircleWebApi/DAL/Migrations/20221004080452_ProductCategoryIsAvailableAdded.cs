using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class ProductCategoryIsAvailableAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "ProductCategories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "9960dddd-46d3-48b8-89e1-b5cd6eb00401");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "852dfe4b-d6b5-4ab7-932f-16786ece99df");

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsAvailable",
                value: true);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsAvailable",
                value: true);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 3,
                column: "IsAvailable",
                value: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "80812df6-933e-431b-be4e-8f7686802e2e", "AQAAAAEAACcQAAAAEJ0EwNmQAT246u+RvodWOFWO6y6/MXuIzTMi8Dk6w2uvnCI63jZyZlhAUqmh1LynRA==", "9a46b492-89db-48b2-9663-63d39b864717" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e6bfed8d-e534-4be6-a08d-6102a736da39", "AQAAAAEAACcQAAAAEI4eEv20cafCUzB2onnnOx4hJJW9hKHhqts51AUH9aGrK4Hf7gfoUZKALDqemQyotg==", "de87f9f7-f621-4af4-bdd4-160413b5b5b6" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "ProductCategories");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "722751db-5975-4261-aeaf-0a5cdb6b7e2d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "503167eb-0863-43a9-ace1-1e2e7a3c4b7a");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "17f01a2e-f213-4084-8e0a-8f47e8833efb", "AQAAAAEAACcQAAAAEFPyNfZB7ZP3mbaAaO8yaGTkO31hKXSxZ6z/K8eeKUdXLlrLO0Tt437U+xfoUbE5Iw==", "1d18c77e-73fb-46da-9f0e-69e1a6bf07c5" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4fa0659b-6634-4a45-bf7f-7ca8715e5f29", "AQAAAAEAACcQAAAAEPGnHg8vqIXdRu3rGYRyxI8kINZko92WE2ZFfyV092DZVuBjhcABnjd15tWaHt5+bw==", "3d592148-9c89-408d-809b-ad45a35ea24a" });
        }
    }
}
