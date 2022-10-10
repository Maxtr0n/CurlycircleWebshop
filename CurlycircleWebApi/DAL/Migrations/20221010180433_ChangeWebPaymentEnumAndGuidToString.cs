using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class ChangeWebPaymentEnumAndGuidToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "BarionPaymentId",
                table: "WebPayments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "7c245e47-7c83-4c62-928e-709e6bafe0d4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "eb581194-67a6-443e-b5f6-cb96e367b423");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5a90a8d1-b8e0-49f1-84eb-fb589f922dc5", "AQAAAAEAACcQAAAAEHJLnn7xuw40TbrM1PEqMH6smSgt9ly4BGzoTMGmzcscI/DVxhtL/+d13AYcgi03IA==", "6b987eb7-5285-4e5c-9d11-a5f762e54c52" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7d7f0b4d-c401-4953-b272-ea2b52de2b5c", "AQAAAAEAACcQAAAAEBAqhmzLxA5gfS64nujqwdIkLhNm5Knz76uLnE0OfXSOLqx/QO+oZ6Vmqwb8stL7Og==", "a4abc4af-f465-4b7d-899b-e9e57bfc6f0f" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "BarionPaymentId",
                table: "WebPayments",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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
    }
}
