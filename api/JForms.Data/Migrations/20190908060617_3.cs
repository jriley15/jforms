using Microsoft.EntityFrameworkCore.Migrations;

namespace JForms.Data.Migrations
{
    public partial class _3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormFieldTypeRuleType_FormValidationRuleType_FormValidation~",
                table: "FormFieldTypeRuleType");

            migrationBuilder.DropForeignKey(
                name: "FK_FormValidationRule_FormValidationRuleType_TypeFormValidatio~",
                table: "FormValidationRule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FormValidationRuleType",
                table: "FormValidationRuleType");

            migrationBuilder.RenameTable(
                name: "FormValidationRuleType",
                newName: "FormValidationRuleTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FormValidationRuleTypes",
                table: "FormValidationRuleTypes",
                column: "FormValidationRuleTypeId");

            migrationBuilder.InsertData(
                table: "FormFieldTypeRuleType",
                columns: new[] { "FormFieldTypeId", "FormValidationRuleTypeId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 4 },
                    { 1, 5 },
                    { 2, 1 },
                    { 2, 2 },
                    { 2, 3 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_FormFieldTypeRuleType_FormValidationRuleTypes_FormValidatio~",
                table: "FormFieldTypeRuleType",
                column: "FormValidationRuleTypeId",
                principalTable: "FormValidationRuleTypes",
                principalColumn: "FormValidationRuleTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FormValidationRule_FormValidationRuleTypes_TypeFormValidati~",
                table: "FormValidationRule",
                column: "TypeFormValidationRuleTypeId",
                principalTable: "FormValidationRuleTypes",
                principalColumn: "FormValidationRuleTypeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormFieldTypeRuleType_FormValidationRuleTypes_FormValidatio~",
                table: "FormFieldTypeRuleType");

            migrationBuilder.DropForeignKey(
                name: "FK_FormValidationRule_FormValidationRuleTypes_TypeFormValidati~",
                table: "FormValidationRule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FormValidationRuleTypes",
                table: "FormValidationRuleTypes");

            migrationBuilder.DeleteData(
                table: "FormFieldTypeRuleType",
                keyColumns: new[] { "FormFieldTypeId", "FormValidationRuleTypeId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "FormFieldTypeRuleType",
                keyColumns: new[] { "FormFieldTypeId", "FormValidationRuleTypeId" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.DeleteData(
                table: "FormFieldTypeRuleType",
                keyColumns: new[] { "FormFieldTypeId", "FormValidationRuleTypeId" },
                keyValues: new object[] { 1, 5 });

            migrationBuilder.DeleteData(
                table: "FormFieldTypeRuleType",
                keyColumns: new[] { "FormFieldTypeId", "FormValidationRuleTypeId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "FormFieldTypeRuleType",
                keyColumns: new[] { "FormFieldTypeId", "FormValidationRuleTypeId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "FormFieldTypeRuleType",
                keyColumns: new[] { "FormFieldTypeId", "FormValidationRuleTypeId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.RenameTable(
                name: "FormValidationRuleTypes",
                newName: "FormValidationRuleType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FormValidationRuleType",
                table: "FormValidationRuleType",
                column: "FormValidationRuleTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_FormFieldTypeRuleType_FormValidationRuleType_FormValidation~",
                table: "FormFieldTypeRuleType",
                column: "FormValidationRuleTypeId",
                principalTable: "FormValidationRuleType",
                principalColumn: "FormValidationRuleTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FormValidationRule_FormValidationRuleType_TypeFormValidatio~",
                table: "FormValidationRule",
                column: "TypeFormValidationRuleTypeId",
                principalTable: "FormValidationRuleType",
                principalColumn: "FormValidationRuleTypeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
