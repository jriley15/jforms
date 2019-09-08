using Microsoft.EntityFrameworkCore.Migrations;

namespace JForms.Data.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "MultipleOptions",
                table: "FormFieldTypes",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MultipleOptions",
                table: "FormFieldTypes");
        }
    }
}
