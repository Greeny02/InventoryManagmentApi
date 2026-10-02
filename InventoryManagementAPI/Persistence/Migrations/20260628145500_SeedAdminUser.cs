using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManagementAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "company_guid", "first_name", "last_name", "email", "password_hash", "role", "created_date" },
                values: new object[]
                {
                    "00000000-0000-0000-0000-000000000001",
                    "Admin",
                    "User",
                    "admin@inventory.com",
                    "$2a$11$6KJ1oZcQFbKY.lCaUnYEzuMAK3xY7tydSe4Y3FD0a1riY16FzitLW", // Admin123!
                    1, // UserRole.Admin
                    new DateTime(2026, 6, 28, 0, 0, 0, DateTimeKind.Utc)
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "user",
                keyColumn: "email",
                keyValue: "admin@inventory.com");
        }
    }
}
