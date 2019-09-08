using Microsoft.EntityFrameworkCore.Migrations;

namespace JForms.Data.Migrations
{
    public partial class _4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "FormFieldType",
                columns: new[] { "FormFieldTypeId", "Value" },
                values: new object[,]
                {
                    { 1, "String" },
                    { 2, "Number" },
                    { 3, "Date" },
                    { 4, "RadioButton" },
                    { 5, "DropDown" },
                    { 6, "MultiSelect" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FormFieldType",
                keyColumn: "FormFieldTypeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "FormFieldType",
                keyColumn: "FormFieldTypeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "FormFieldType",
                keyColumn: "FormFieldTypeId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "FormFieldType",
                keyColumn: "FormFieldTypeId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "FormFieldType",
                keyColumn: "FormFieldTypeId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "FormFieldType",
                keyColumn: "FormFieldTypeId",
                keyValue: 6);
        }
    }
}
