using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCvProject.Infra.Data.Migrations
{
    public partial class PrimaryDataInCourseLevel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CourseLevels",
                columns: new[] { "LevelId", "LevelTitle" },
                values: new object[,]
                {
                    { 1, "مقدماتی" },
                    { 2, "متوسط" },
                    { 3, "پیشرفته" },
                    { 4, "فوق پیشرفته" }
                });

            migrationBuilder.InsertData(
                table: "CourseStatuses",
                columns: new[] { "StatusId", "StatusTitle" },
                values: new object[,]
                {
                    { 1, "در حال برگزاری" },
                    { 2, "کامل شده" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "ActiveCode", "RegisterDate" },
                values: new object[] { "d9193db80a314fa3b5ca4a40891c49cd", new DateTime(2021, 7, 25, 16, 7, 35, 120, DateTimeKind.Local).AddTicks(2901) });

            migrationBuilder.InsertData(
                table: "WalletTypes",
                columns: new[] { "TypeId", "TypeTitle" },
                values: new object[,]
                {
                    { 1, "واریز" },
                    { 2, "برداشت" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CourseLevels",
                keyColumn: "LevelId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CourseLevels",
                keyColumn: "LevelId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CourseLevels",
                keyColumn: "LevelId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "CourseLevels",
                keyColumn: "LevelId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "CourseStatuses",
                keyColumn: "StatusId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CourseStatuses",
                keyColumn: "StatusId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "WalletTypes",
                keyColumn: "TypeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "WalletTypes",
                keyColumn: "TypeId",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "ActiveCode", "RegisterDate" },
                values: new object[] { "2232f3a19716448888a528e4f2bcca5a", new DateTime(2021, 7, 23, 19, 19, 21, 770, DateTimeKind.Local).AddTicks(6063) });
        }
    }
}
