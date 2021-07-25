using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCvProject.Infra.Data.Migrations
{
    public partial class RegisterAdminData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "RegisterDate",
                value: new DateTime(2021, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "RegisterDate",
                value: new DateTime(2021, 7, 25, 17, 56, 38, 861, DateTimeKind.Local).AddTicks(5332));
        }
    }
}
