using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class SeedDataForColorMaterialPattern : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "14d621fd-1289-4ad4-bf0b-d52084d1d567");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "83bfa89f-7951-4218-8de2-1dcb492363ae");

            migrationBuilder.InsertData(
                table: "Color",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Kék" },
                    { 2, "Piros" }
                });

            migrationBuilder.InsertData(
                table: "Material",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Műanyag" },
                    { 2, "Szövet" }
                });

            migrationBuilder.InsertData(
                table: "Pattern",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Sima" },
                    { 2, "Csíkos" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ef1763c3-7f3c-4d5a-a64f-fd98e448f48c", "AQAAAAEAACcQAAAAEKdKG2fcD0A/BqAZLP8bEm7kKmbi+p1qS+M9vongEX/RU/qcWtnDsqL8pKn4163OPA==", "b728bcf0-d7b9-4bc9-ba04-595a38ad3f4a" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "077f79b1-32db-4e4d-b986-248820c62c12", "AQAAAAEAACcQAAAAEDjs448nJv7gHWnz6N3z5YjUk789nTAs7tMUOXN6SfLhqjESXSthqwub9JJaxofhKQ==", "f133a87b-a1b0-4703-860e-a3b15c3f4788" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Color",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Color",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Material",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Material",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Pattern",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Pattern",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "899a646b-1a3d-4400-b14c-c6078e23118d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "7e4d6556-bafa-4e4f-928a-4667a2b0df0e");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3531604c-3918-4fc4-9fa3-90b5a5012fc6", "AQAAAAEAACcQAAAAEKhXbVRqg7Jfmj97xTzJcfSz/1lqVIv77zCpHgfuES4cudzpMS+t/bvRyI4k8v1Dcg==", "62638fd2-4aa7-4837-959b-4c8975a1d1df" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2b4ed5d0-85d4-40af-b117-9e0104411676", "AQAAAAEAACcQAAAAEP296Xey3Ql7eZAa5sRWElpUEdaexNpKQR1EcjpPp7KW63Ua6L62InyvPLyv4Gt5zg==", "901a4d39-bdf7-4ef2-8d07-52458edc111c" });
        }
    }
}
