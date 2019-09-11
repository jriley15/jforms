using Microsoft.EntityFrameworkCore.Migrations;

namespace JForms.Data.Migrations
{
    public partial class _7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "FormFieldTypes",
                keyColumn: "FormFieldTypeId",
                keyValue: 4,
                column: "MultipleOptions",
                value: true);

            migrationBuilder.UpdateData(
                table: "FormFieldTypes",
                keyColumn: "FormFieldTypeId",
                keyValue: 5,
                column: "MultipleOptions",
                value: true);

            migrationBuilder.UpdateData(
                table: "FormFieldTypes",
                keyColumn: "FormFieldTypeId",
                keyValue: 6,
                column: "MultipleOptions",
                value: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "FormFieldTypes",
                keyColumn: "FormFieldTypeId",
                keyValue: 4,
                column: "MultipleOptions",
                value: false);

            migrationBuilder.UpdateData(
                table: "FormFieldTypes",
                keyColumn: "FormFieldTypeId",
                keyValue: 5,
                column: "MultipleOptions",
                value: false);

            migrationBuilder.UpdateData(
                table: "FormFieldTypes",
                keyColumn: "FormFieldTypeId",
                keyValue: 6,
                column: "MultipleOptions",
                value: false);
        }
    }
}
