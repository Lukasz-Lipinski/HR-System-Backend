using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hr_system_backend.Migrations
{
    /// <inheritdoc />
    public partial class SuperiorMockAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: new Guid("3fbe3288-5b9a-4462-bc85-13719521fd58"));

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "Email", "Name", "Password", "Role", "Surname" },
                values: new object[] { new Guid("c4010e66-f170-49ef-a359-c3e973e0cc74"), "test@test.com", "test", "$2a$11$.F4g9i0YeA8XyyjO72qQdeWfvzjDacqUFc4TLGdB1UtGiHcgAc7CK", 3, "test" });

            migrationBuilder.InsertData(
                table: "Superiors",
                columns: new[] { "Id", "Area", "Daysoff", "Email", "EmployeesId", "Name", "Position", "Role", "Status", "Surname" },
                values: new object[] { new Guid("a04cd6b3-ed0d-43d5-b9d8-0ce1d21412a2"), "Casting", 26, "superior@example.com", new Guid("00000000-0000-0000-0000-000000000000"), "Superior name", "Supirior Manager", 1, 1, "Superior surname" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: new Guid("c4010e66-f170-49ef-a359-c3e973e0cc74"));

            migrationBuilder.DeleteData(
                table: "Superiors",
                keyColumn: "Id",
                keyValue: new Guid("a04cd6b3-ed0d-43d5-b9d8-0ce1d21412a2"));

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "Email", "Name", "Password", "Role", "Surname" },
                values: new object[] { new Guid("3fbe3288-5b9a-4462-bc85-13719521fd58"), "test@test.com", "test", "$2a$11$Gkrli/h5WmgBDGvJZ/E5Sup/VxOP9Lozp/EZJZGAn4/e3/IxMwKQm", 3, "test" });
        }
    }
}
