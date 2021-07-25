using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCvProject.Infra.Data.Migrations
{
    public partial class IsDelete_Course : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Courses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "ActiveCode", "RegisterDate" },
                values: new object[] { "fea2f9e251d54dc8b572b7996abf9e87", new DateTime(2021, 7, 25, 17, 56, 38, 861, DateTimeKind.Local).AddTicks(5332) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Courses");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "ActiveCode", "RegisterDate" },
                values: new object[] { "d9193db80a314fa3b5ca4a40891c49cd", new DateTime(2021, 7, 25, 16, 7, 35, 120, DateTimeKind.Local).AddTicks(2901) });
        }
    }
}
