using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class FixUserDataSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "705b5027-49cc-4945-8800-379f0f77dff3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "f918b4ad-eb63-4299-898d-86e99f068f99");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ee302849-cf37-4231-a0ad-46f779801fb9", "ADMIN@ADMIN.COM", "ADMIN", "AQAAAAEAACcQAAAAEMbYRwInWv7WFvhAY2N0ZJyY0q9+AhiVdtNa+fQY/yA6D2nCAZzNb3ktBXpO6FNW7A==", "6a2d0820-1704-4e55-9e9c-c4db1a108ec2" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f8defb11-15e9-44f9-99cf-6375a4f031c2", "USER@USER.COM", "USER", "AQAAAAEAACcQAAAAEIt2l0IpNMuPj2KDNEqSt5kogBQ95EBg9ZKR4kG4xBluhX/q0mz0PtirGtsSuivMZw==", "33aa571a-021b-45bf-a773-7e2fbc4557b6" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "8ae17508-0538-460d-b4bf-8dc2fa8b3a26");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "4413728b-61ae-45a5-878f-edb412260c36");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "641453bb-5fcb-4cff-af34-a0a681bddf25", null, null, "AQAAAAEAACcQAAAAEKV4KO/AaG0phWb/aXVevBBvMzJoOslb0r0QvOR9PKwWWrFi3jcWUBiT+FWOLsjteA==", "cc1f7cb0-40a0-40ff-ba35-ea547573d0d8" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f7ed3396-8d7b-402b-8a1d-724c8baa6285", null, null, "AQAAAAEAACcQAAAAEIe9HCE1d3n4tchve5/TZt6cTlmMF3x13zSmCX3egeEM34wV8hwiKOgVGjEN8gVz9w==", "02f0c003-b270-4f26-924c-220b6191b916" });
        }
    }
}
