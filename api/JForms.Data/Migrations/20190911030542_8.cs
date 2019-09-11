using Microsoft.EntityFrameworkCore.Migrations;

namespace JForms.Data.Migrations
{
    public partial class _8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "FormFieldTypes",
                columns: new[] { "FormFieldTypeId", "MultipleOptions", "Name" },
                values: new object[] { 7, false, "CheckBox" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FormFieldTypes",
                keyColumn: "FormFieldTypeId",
                keyValue: 7);
        }
    }
}
