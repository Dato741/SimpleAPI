using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDo.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class Sortingparameters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ascending",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "SortBy",
                table: "Tasks");
        }
    }
}
