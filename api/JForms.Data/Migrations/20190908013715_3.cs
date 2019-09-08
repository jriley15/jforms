using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace JForms.Data.Migrations
{
    public partial class _3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "FormField");

            migrationBuilder.AddColumn<int>(
                name: "TypeFormFieldTypeId",
                table: "FormField",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ValidationFormFieldValidationId",
                table: "FormField",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FormFieldOption",
                columns: table => new
                {
                    FormFieldOptionId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<string>(nullable: true),
                    FormFieldId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormFieldOption", x => x.FormFieldOptionId);
                    table.ForeignKey(
                        name: "FK_FormFieldOption_FormField_FormFieldId",
                        column: x => x.FormFieldId,
                        principalTable: "FormField",
                        principalColumn: "FormFieldId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FormFieldType",
                columns: table => new
                {
                    FormFieldTypeId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormFieldType", x => x.FormFieldTypeId);
                });

            migrationBuilder.CreateTable(
                name: "FormFieldValidation",
                columns: table => new
                {
                    FormFieldValidationId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<int>(nullable: false),
                    Script = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormFieldValidation", x => x.FormFieldValidationId);
                });

            migrationBuilder.CreateTable(
                name: "FormValidationRuleType",
                columns: table => new
                {
                    FormValidationRuleTypeId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormValidationRuleType", x => x.FormValidationRuleTypeId);
                });

            migrationBuilder.CreateTable(
                name: "FormFieldTypeRuleType",
                columns: table => new
                {
                    FormValidationRuleTypeId = table.Column<int>(nullable: false),
                    FormFieldTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormFieldTypeRuleType", x => new { x.FormFieldTypeId, x.FormValidationRuleTypeId });
                    table.ForeignKey(
                        name: "FK_FormFieldTypeRuleType_FormFieldType_FormFieldTypeId",
                        column: x => x.FormFieldTypeId,
                        principalTable: "FormFieldType",
                        principalColumn: "FormFieldTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FormFieldTypeRuleType_FormValidationRuleType_FormValidation~",
                        column: x => x.FormValidationRuleTypeId,
                        principalTable: "FormValidationRuleType",
                        principalColumn: "FormValidationRuleTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormValidationRule",
                columns: table => new
                {
                    FormValidationRuleId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TypeFormValidationRuleTypeId = table.Column<int>(nullable: true),
                    Constraint = table.Column<string>(nullable: true),
                    FormFieldValidationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormValidationRule", x => x.FormValidationRuleId);
                    table.ForeignKey(
                        name: "FK_FormValidationRule_FormFieldValidation_FormFieldValidationId",
                        column: x => x.FormFieldValidationId,
                        principalTable: "FormFieldValidation",
                        principalColumn: "FormFieldValidationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FormValidationRule_FormValidationRuleType_TypeFormValidatio~",
                        column: x => x.TypeFormValidationRuleTypeId,
                        principalTable: "FormValidationRuleType",
                        principalColumn: "FormValidationRuleTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FormField_TypeFormFieldTypeId",
                table: "FormField",
                column: "TypeFormFieldTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FormField_ValidationFormFieldValidationId",
                table: "FormField",
                column: "ValidationFormFieldValidationId");

            migrationBuilder.CreateIndex(
                name: "IX_FormFieldOption_FormFieldId",
                table: "FormFieldOption",
                column: "FormFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_FormFieldTypeRuleType_FormValidationRuleTypeId",
                table: "FormFieldTypeRuleType",
                column: "FormValidationRuleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FormValidationRule_FormFieldValidationId",
                table: "FormValidationRule",
                column: "FormFieldValidationId");

            migrationBuilder.CreateIndex(
                name: "IX_FormValidationRule_TypeFormValidationRuleTypeId",
                table: "FormValidationRule",
                column: "TypeFormValidationRuleTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_FormField_FormFieldType_TypeFormFieldTypeId",
                table: "FormField",
                column: "TypeFormFieldTypeId",
                principalTable: "FormFieldType",
                principalColumn: "FormFieldTypeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FormField_FormFieldValidation_ValidationFormFieldValidation~",
                table: "FormField",
                column: "ValidationFormFieldValidationId",
                principalTable: "FormFieldValidation",
                principalColumn: "FormFieldValidationId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormField_FormFieldType_TypeFormFieldTypeId",
                table: "FormField");

            migrationBuilder.DropForeignKey(
                name: "FK_FormField_FormFieldValidation_ValidationFormFieldValidation~",
                table: "FormField");

            migrationBuilder.DropTable(
                name: "FormFieldOption");

            migrationBuilder.DropTable(
                name: "FormFieldTypeRuleType");

            migrationBuilder.DropTable(
                name: "FormValidationRule");

            migrationBuilder.DropTable(
                name: "FormFieldType");

            migrationBuilder.DropTable(
                name: "FormFieldValidation");

            migrationBuilder.DropTable(
                name: "FormValidationRuleType");

            migrationBuilder.DropIndex(
                name: "IX_FormField_TypeFormFieldTypeId",
                table: "FormField");

            migrationBuilder.DropIndex(
                name: "IX_FormField_ValidationFormFieldValidationId",
                table: "FormField");

            migrationBuilder.DropColumn(
                name: "TypeFormFieldTypeId",
                table: "FormField");

            migrationBuilder.DropColumn(
                name: "ValidationFormFieldValidationId",
                table: "FormField");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "FormField",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
