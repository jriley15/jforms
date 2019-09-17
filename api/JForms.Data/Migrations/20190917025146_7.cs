using Microsoft.EntityFrameworkCore.Migrations;

namespace JForms.Data.Migrations
{
    public partial class _7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "FormFieldTypeRuleType",
                columns: new[] { "FormFieldTypeId", "FormValidationRuleTypeId" },
                values: new object[,]
                {
                    { 3, 1 },
                    { 3, 6 },
                    { 3, 7 },
                    { 4, 1 },
                    { 5, 1 },
                    { 6, 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FormFieldTypeRuleType",
                keyColumns: new[] { "FormFieldTypeId", "FormValidationRuleTypeId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "FormFieldTypeRuleType",
                keyColumns: new[] { "FormFieldTypeId", "FormValidationRuleTypeId" },
                keyValues: new object[] { 3, 6 });

            migrationBuilder.DeleteData(
                table: "FormFieldTypeRuleType",
                keyColumns: new[] { "FormFieldTypeId", "FormValidationRuleTypeId" },
                keyValues: new object[] { 3, 7 });

            migrationBuilder.DeleteData(
                table: "FormFieldTypeRuleType",
                keyColumns: new[] { "FormFieldTypeId", "FormValidationRuleTypeId" },
                keyValues: new object[] { 4, 1 });

            migrationBuilder.DeleteData(
                table: "FormFieldTypeRuleType",
                keyColumns: new[] { "FormFieldTypeId", "FormValidationRuleTypeId" },
                keyValues: new object[] { 5, 1 });

            migrationBuilder.DeleteData(
                table: "FormFieldTypeRuleType",
                keyColumns: new[] { "FormFieldTypeId", "FormValidationRuleTypeId" },
                keyValues: new object[] { 6, 1 });
        }
    }
}
