using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CalendarDbContext.Migrations
{
    /// <inheritdoc />
    public partial class delete_id_from_DateEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "DateEvent");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "DateEvent",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
