using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class RemovePatternAndMaterialProductReferences : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "2e3c3bf3-d033-41ac-99ca-a94ea6af3682");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "62c8ab52-c8a7-4f5f-8067-7feb6772d7a6");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5f717ed3-a3f7-43f8-ac12-a6a1285f511f", "AQAAAAEAACcQAAAAEJ/oRxoeGpFzwN13oHR9tP9Y7pF2oahnEmx17mJN9JLU999i23cT7rQp9MtqsGcRig==", "1823f5ff-70c4-42b7-b160-38c8eb3a57ba" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7ec7d9ee-fa12-4e0f-80b4-b1ca9e8034f5", "AQAAAAEAACcQAAAAEJoMj+tTqJ5E/UrkwKNWNpkit5Lc8sBEXK63wb0psp3iFxQOjMphofFWb/UnHrvPDw==", "df4e605b-963a-4941-8520-4b3a77d6af26" });
        }
    }
}
