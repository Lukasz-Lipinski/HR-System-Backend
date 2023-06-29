using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hr_system_backend.Migrations
{
    /// <inheritdoc />
    public partial class AdminMocksAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "Email", "Name", "Password", "Role", "Surname" },
                values: new object[] { new Guid("264a0a5a-0de0-423f-83dc-6737b76368a1"), "test@test.com", "test", "$2a$11$Bh8A1cKl14xAFAr9EH94D.BuOF0My9i9YPpJfaRSRb905d0clGacS", 3, "test" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: new Guid("264a0a5a-0de0-423f-83dc-6737b76368a1"));
        }
    }
}
