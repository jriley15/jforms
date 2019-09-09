using Microsoft.EntityFrameworkCore.Migrations;

namespace JForms.Data.Migrations
{
    public partial class _6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormField_FormFieldTypes_TypeFormFieldTypeId",
                table: "FormField");

            migrationBuilder.DropForeignKey(
                name: "FK_FormFieldValidationRule_FormFieldValidationRuleTypes_TypeFo~",
                table: "FormFieldValidationRule");

            migrationBuilder.DropIndex(
                name: "IX_FormFieldValidationRule_TypeFormFieldValidationRuleTypeId",
                table: "FormFieldValidationRule");

            migrationBuilder.DropIndex(
                name: "IX_FormField_TypeFormFieldTypeId",
                table: "FormField");

            migrationBuilder.DropColumn(
                name: "TypeFormFieldValidationRuleTypeId",
                table: "FormFieldValidationRule");

            migrationBuilder.DropColumn(
                name: "TypeFormFieldTypeId",
                table: "FormField");

            migrationBuilder.AddColumn<int>(
                name: "FormFieldValidationRuleTypeId",
                table: "FormFieldValidationRule",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FormFieldTypeId",
                table: "FormField",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_FormFieldValidationRule_FormFieldValidationRuleTypeId",
                table: "FormFieldValidationRule",
                column: "FormFieldValidationRuleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FormField_FormFieldTypeId",
                table: "FormField",
                column: "FormFieldTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_FormField_FormFieldTypes_FormFieldTypeId",
                table: "FormField",
                column: "FormFieldTypeId",
                principalTable: "FormFieldTypes",
                principalColumn: "FormFieldTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FormFieldValidationRule_FormFieldValidationRuleTypes_FormFi~",
                table: "FormFieldValidationRule",
                column: "FormFieldValidationRuleTypeId",
                principalTable: "FormFieldValidationRuleTypes",
                principalColumn: "FormFieldValidationRuleTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormField_FormFieldTypes_FormFieldTypeId",
                table: "FormField");

            migrationBuilder.DropForeignKey(
                name: "FK_FormFieldValidationRule_FormFieldValidationRuleTypes_FormFi~",
                table: "FormFieldValidationRule");

            migrationBuilder.DropIndex(
                name: "IX_FormFieldValidationRule_FormFieldValidationRuleTypeId",
                table: "FormFieldValidationRule");

            migrationBuilder.DropIndex(
                name: "IX_FormField_FormFieldTypeId",
                table: "FormField");

            migrationBuilder.DropColumn(
                name: "FormFieldValidationRuleTypeId",
                table: "FormFieldValidationRule");

            migrationBuilder.DropColumn(
                name: "FormFieldTypeId",
                table: "FormField");

            migrationBuilder.AddColumn<int>(
                name: "TypeFormFieldValidationRuleTypeId",
                table: "FormFieldValidationRule",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TypeFormFieldTypeId",
                table: "FormField",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FormFieldValidationRule_TypeFormFieldValidationRuleTypeId",
                table: "FormFieldValidationRule",
                column: "TypeFormFieldValidationRuleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FormField_TypeFormFieldTypeId",
                table: "FormField",
                column: "TypeFormFieldTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_FormField_FormFieldTypes_TypeFormFieldTypeId",
                table: "FormField",
                column: "TypeFormFieldTypeId",
                principalTable: "FormFieldTypes",
                principalColumn: "FormFieldTypeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FormFieldValidationRule_FormFieldValidationRuleTypes_TypeFo~",
                table: "FormFieldValidationRule",
                column: "TypeFormFieldValidationRuleTypeId",
                principalTable: "FormFieldValidationRuleTypes",
                principalColumn: "FormFieldValidationRuleTypeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
