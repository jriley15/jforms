using Microsoft.EntityFrameworkCore.Migrations;

namespace JForms.Data.Migrations
{
    public partial class _4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "FormValidationRuleTypes",
                keyColumn: "FormValidationRuleTypeId",
                keyValue: 2,
                column: "Name",
                value: "Minimum_Value");

            migrationBuilder.UpdateData(
                table: "FormValidationRuleTypes",
                keyColumn: "FormValidationRuleTypeId",
                keyValue: 3,
                column: "Name",
                value: "Maxmimum__Value");

            migrationBuilder.UpdateData(
                table: "FormValidationRuleTypes",
                keyColumn: "FormValidationRuleTypeId",
                keyValue: 4,
                column: "Name",
                value: "Minimum_Length");

            migrationBuilder.UpdateData(
                table: "FormValidationRuleTypes",
                keyColumn: "FormValidationRuleTypeId",
                keyValue: 5,
                column: "Name",
                value: "Maxmimum_Length");

            migrationBuilder.UpdateData(
                table: "FormValidationRuleTypes",
                keyColumn: "FormValidationRuleTypeId",
                keyValue: 6,
                column: "Name",
                value: "Minimum_Date");

            migrationBuilder.UpdateData(
                table: "FormValidationRuleTypes",
                keyColumn: "FormValidationRuleTypeId",
                keyValue: 7,
                column: "Name",
                value: "Maxmimum_Date");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "FormValidationRuleTypes",
                keyColumn: "FormValidationRuleTypeId",
                keyValue: 2,
                column: "Name",
                value: "NumberMin");

            migrationBuilder.UpdateData(
                table: "FormValidationRuleTypes",
                keyColumn: "FormValidationRuleTypeId",
                keyValue: 3,
                column: "Name",
                value: "NumberMax");

            migrationBuilder.UpdateData(
                table: "FormValidationRuleTypes",
                keyColumn: "FormValidationRuleTypeId",
                keyValue: 4,
                column: "Name",
                value: "StringMin");

            migrationBuilder.UpdateData(
                table: "FormValidationRuleTypes",
                keyColumn: "FormValidationRuleTypeId",
                keyValue: 5,
                column: "Name",
                value: "StringMax");

            migrationBuilder.UpdateData(
                table: "FormValidationRuleTypes",
                keyColumn: "FormValidationRuleTypeId",
                keyValue: 6,
                column: "Name",
                value: "DateMin");

            migrationBuilder.UpdateData(
                table: "FormValidationRuleTypes",
                keyColumn: "FormValidationRuleTypeId",
                keyValue: 7,
                column: "Name",
                value: "DateMax");
        }
    }
}
