using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeTaskAPI.Migrations
{
    public partial class InitalCrat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    employeeId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    firstName = table.Column<string>(nullable: false),
                    lastName = table.Column<string>(nullable: false),
                    hiredDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.employeeId);
                });

            migrationBuilder.CreateTable(
                name: "Task",
                columns: table => new
                {
                    taskId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    taskName = table.Column<string>(nullable: false),
                    startTime = table.Column<DateTime>(nullable: false),
                    dueTime = table.Column<DateTime>(nullable: false),
                    employeeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task", x => x.taskId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Task");
        }
    }
}
