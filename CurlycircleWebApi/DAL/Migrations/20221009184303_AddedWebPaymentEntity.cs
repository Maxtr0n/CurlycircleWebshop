using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class AddedWebPaymentEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "webpaymentseq",
                startValue: 1000L);

            migrationBuilder.CreateTable(
                name: "WebPayments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    POSTransactionId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BarionPaymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Total = table.Column<double>(type: "float", nullable: false),
                    PaymentStatus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WebPayments_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "4db0f2f8-c0b1-443a-9683-5632f1a42222");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "5bf589db-0cf0-4b75-8b99-41626179ef20");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "78ae3574-20bf-4af3-b0bc-ef3d59001865", "AQAAAAEAACcQAAAAEJrU0sr30dPrf79aba6MYkm5B7RK/N5PD//rcrPVlIkPTnXnEuVOqjIcHkj5zDmXWQ==", "45e9ca3c-b4ff-4b2e-8f74-7c12c5ce57ff" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a003ef6d-2e6a-4793-8825-d6496f1926ae", "AQAAAAEAACcQAAAAEDhQXH3KJaxUCcTiLpbId1puN8rZ0KrjRvEBc1UP7ZRpwgBbJ0LkEyboNlZ2X2gYxQ==", "1a2c683b-efc0-404e-91fe-e3111e8d7bfc" });

            migrationBuilder.CreateIndex(
                name: "IX_WebPayments_OrderId",
                table: "WebPayments",
                column: "OrderId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WebPayments");

            migrationBuilder.DropSequence(
                name: "webpaymentseq");

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
    }
}
