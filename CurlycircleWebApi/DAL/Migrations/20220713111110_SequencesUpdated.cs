using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class SequencesUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterSequence(
                name: "productseq",
                oldIncrementBy: 10);

            migrationBuilder.AlterSequence(
                name: "productcategoryseq",
                oldIncrementBy: 10);

            migrationBuilder.AlterSequence(
                name: "orderseq",
                oldIncrementBy: 10);

            migrationBuilder.AlterSequence(
                name: "orderitemseq",
                oldIncrementBy: 10);

            migrationBuilder.AlterSequence(
                name: "cartseq",
                oldIncrementBy: 10);

            migrationBuilder.AlterSequence(
                name: "cartitemseq",
                oldIncrementBy: 10);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterSequence(
                name: "productseq",
                incrementBy: 10);

            migrationBuilder.AlterSequence(
                name: "productcategoryseq",
                incrementBy: 10);

            migrationBuilder.AlterSequence(
                name: "orderseq",
                incrementBy: 10);

            migrationBuilder.AlterSequence(
                name: "orderitemseq",
                incrementBy: 10);

            migrationBuilder.AlterSequence(
                name: "cartseq",
                incrementBy: 10);

            migrationBuilder.AlterSequence(
                name: "cartitemseq",
                incrementBy: 10);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "a0023800-d12c-425a-83d8-3b3988e240c3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "76f1ddbd-201e-4821-96eb-4e15e9d594a5");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6c7f22fe-1b79-4555-9e56-e99d0dc98414", "AQAAAAEAACcQAAAAEA0W1egr+i9yj/oSmfKBzGfF7U5Z7aQJaggqPH9zHTQv7pgfAFKLmOjErlLhCGCBrg==", "bb196ef3-00fe-4439-aa4c-c66c57a50a77" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e85ad6ec-e1a7-4658-9a0c-996e319c6713", "AQAAAAEAACcQAAAAEKFRA0DLQcxS3HhQOMhjU+k5HjsXGwO4HGpRClK9fDZe5Tr1bXYvQHEx0B1AvgtJzw==", "8b012450-b364-4493-99fa-f2d5f31d432f" });
        }
    }
}
