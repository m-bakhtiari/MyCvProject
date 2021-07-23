using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCvProject.Infra.Data.Migrations
{
    public partial class SeedPrimaryData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "PermissionId", "ParentID", "PermissionTitle" },
                values: new object[] { 1001, null, "پنل مدیریت" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "IsDelete", "RoleTitle" },
                values: new object[,]
                {
                    { 1001, false, "کاربر" },
                    { 1002, false, "مدیر" },
                    { 1003, false, "استاد" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "ActiveCode", "Email", "IsActive", "IsDelete", "Password", "RegisterDate", "UserAvatar", "UserName" },
                values: new object[] { 1, "2232f3a19716448888a528e4f2bcca5a", "admin@gmail.com", true, false, "20-2C-B9-62-AC-59-07-5B-96-4B-07-15-2D-23-4B-70", new DateTime(2021, 7, 23, 19, 19, 21, 770, DateTimeKind.Local).AddTicks(6063), "Defult.jpg", "admin" });

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "PermissionId", "ParentID", "PermissionTitle" },
                values: new object[,]
                {
                    { 1002, 1001, "مدیریت کاربران" },
                    { 1006, 1001, "مدیریت نقش ها" }
                });

            migrationBuilder.InsertData(
                table: "RolePermission",
                columns: new[] { "RP_Id", "PermissionId", "RoleId" },
                values: new object[] { 1, 1001, 1002 });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "UR_Id", "RoleId", "UserId" },
                values: new object[] { 1, 1002, 1 });

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "PermissionId", "ParentID", "PermissionTitle" },
                values: new object[,]
                {
                    { 1003, 1002, "افزودن کاربران" },
                    { 1004, 1002, "ویرایش کاربران" },
                    { 1005, 1002, "حذف کاربران" },
                    { 1007, 1006, "افزودن نقش ها" },
                    { 1008, 1006, "ویرایش نقش ها" },
                    { 1009, 1006, "حذف نقش ها" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "PermissionId",
                keyValue: 1003);

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "PermissionId",
                keyValue: 1004);

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "PermissionId",
                keyValue: 1005);

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "PermissionId",
                keyValue: 1007);

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "PermissionId",
                keyValue: 1008);

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "PermissionId",
                keyValue: 1009);

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumn: "RP_Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 1001);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 1003);

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "UR_Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "PermissionId",
                keyValue: 1002);

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "PermissionId",
                keyValue: 1006);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 1002);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "PermissionId",
                keyValue: 1001);
        }
    }
}
