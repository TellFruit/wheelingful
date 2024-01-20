using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Wheelingful.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "AspNetUsers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "AspNetUsers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3f6d7281-1c57-4fd9-895b-9c76f14f3eae", null, "Reader", "READER" },
                    { "7b92af56-2d6e-4c8b-aae5-4f843faa1c79", null, "Admin", "ADMIN" },
                    { "e8f6d91a-9d3b-4a9a-bc32-6b03bf8c25c4", null, "Author", "AUTHOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UpdatedAt", "UserName" },
                values: new object[] { "ff38c4e9-8a0a-4a95-bb82-1f7685db3c59", 0, "cb7a7e3f-94cf-4e20-8e27-1ab264497106", new DateTime(2024, 1, 19, 17, 50, 58, 470, DateTimeKind.Utc).AddTicks(6088), "admin@wheelingful.com", true, false, null, "ADMIN@WHEELINGFUL.COM", "ADMIN@WHEELINGFUL.COM", "AQAAAAIAAYagAAAAECgHupnB8wkJMaI20CCVFYYCvCAFFDsvb0eDQR0+GuK3KjAFYTbRynrrGFk/3NVwIA==", null, false, "WRRZSMKR6S3WSKHAT32VF7EBZV6KJTFL", false, new DateTime(2024, 1, 19, 17, 50, 58, 470, DateTimeKind.Utc).AddTicks(6088), "admin@wheelingful.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "7b92af56-2d6e-4c8b-aae5-4f843faa1c79", "ff38c4e9-8a0a-4a95-bb82-1f7685db3c59" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3f6d7281-1c57-4fd9-895b-9c76f14f3eae");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e8f6d91a-9d3b-4a9a-bc32-6b03bf8c25c4");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "7b92af56-2d6e-4c8b-aae5-4f843faa1c79", "ff38c4e9-8a0a-4a95-bb82-1f7685db3c59" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7b92af56-2d6e-4c8b-aae5-4f843faa1c79");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ff38c4e9-8a0a-4a95-bb82-1f7685db3c59");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "AspNetUsers");
        }
    }
}
