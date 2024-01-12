using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RelationalHumanResources.Migrations
{
    /// <inheritdoc />
    public partial class d2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentCategory",
                table: "Departments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DepartmentCategory",
                table: "Departments");
        }
    }
}
