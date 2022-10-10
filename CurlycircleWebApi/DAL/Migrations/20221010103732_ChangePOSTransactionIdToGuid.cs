using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class ChangePOSTransactionIdToGuid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "POSTransactionId",
                table: "WebPayments",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "9a8c536e-df60-4076-96f7-4e9ee2853a2b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "131a03d9-dd56-4532-aaee-0b83d7de8b71");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "14f7f7f8-332b-4d1d-a240-3e49ad771fff", "AQAAAAEAACcQAAAAEJLyHW+Ugjyyw3+FyHcynDeEyLRqgRfY/vfS7bfc7jsYWX2hOTDXgbSc9Hqd+BHGfw==", "0a79ffd2-2ede-413f-bb74-480d35c9b2af" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "42ef02e4-4144-4958-bbeb-d8a8c89a7a92", "AQAAAAEAACcQAAAAELA/9xfsX7tZk2c2Qme4UvmrKWfljsV0yRwKAZNfqPH6NVTwhVfW38gCyI6wfKGTOQ==", "ab3c5c9c-761d-47f6-b104-fc05ae003f37" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "POSTransactionId",
                table: "WebPayments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

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
        }
    }
}
