using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDo.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class Priorityint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ascending",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "SortBy",
                table: "Tasks");

            migrationBuilder.AlterColumn<int>(
                name: "Priority",
                table: "Tasks",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Priority",
                table: "Tasks",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<bool>(
                name: "Ascending",
                table: "Tasks",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SortBy",
                table: "Tasks",
                type: "TEXT",
                nullable: true);
        }
    }
}
