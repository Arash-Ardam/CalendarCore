using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CalendarDbContext.Migrations
{
    /// <inheritdoc />
    public partial class initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Calendars",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Weekend = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calendars", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "DateEvent",
                columns: table => new
                {
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    IsHoliday = table.Column<bool>(type: "bit", nullable: false),
                    CalendarName = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DateEvent", x => new { x.Date, x.Description });
                    table.ForeignKey(
                        name: "FK_DateEvent_Calendars_CalendarName",
                        column: x => x.CalendarName,
                        principalTable: "Calendars",
                        principalColumn: "Name");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DateEvent_CalendarName",
                table: "DateEvent",
                column: "CalendarName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DateEvent");

            migrationBuilder.DropTable(
                name: "Calendars");
        }
    }
}
