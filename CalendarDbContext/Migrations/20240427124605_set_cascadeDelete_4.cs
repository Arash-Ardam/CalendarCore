using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CalendarDbContext.Migrations
{
    /// <inheritdoc />
    public partial class set_cascadeDelete_4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DateEvent_Calendars_calendarName",
                table: "DateEvent");

            migrationBuilder.RenameColumn(
                name: "calendarName",
                table: "DateEvent",
                newName: "CalendarName");

            migrationBuilder.RenameIndex(
                name: "IX_DateEvent_calendarName",
                table: "DateEvent",
                newName: "IX_DateEvent_CalendarName");

            migrationBuilder.AddForeignKey(
                name: "FK_DateEvent_Calendars_CalendarName",
                table: "DateEvent",
                column: "CalendarName",
                principalTable: "Calendars",
                principalColumn: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DateEvent_Calendars_CalendarName",
                table: "DateEvent");

            migrationBuilder.RenameColumn(
                name: "CalendarName",
                table: "DateEvent",
                newName: "calendarName");

            migrationBuilder.RenameIndex(
                name: "IX_DateEvent_CalendarName",
                table: "DateEvent",
                newName: "IX_DateEvent_calendarName");

            migrationBuilder.AddForeignKey(
                name: "FK_DateEvent_Calendars_calendarName",
                table: "DateEvent",
                column: "calendarName",
                principalTable: "Calendars",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
