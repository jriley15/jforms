using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace JForms.Data.Migrations
{
    public partial class _5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormFieldTypeRuleType_FormValidationRuleTypes_FormValidatio~",
                table: "FormFieldTypeRuleType");

            migrationBuilder.DropTable(
                name: "FormValidationRule");

            migrationBuilder.DropTable(
                name: "FormValidationRuleTypes");

            migrationBuilder.CreateTable(
                name: "FormFieldValidationRuleTypes",
                columns: table => new
                {
                    FormFieldValidationRuleTypeId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormFieldValidationRuleTypes", x => x.FormFieldValidationRuleTypeId);
                });

            migrationBuilder.CreateTable(
                name: "FormFieldValidationRule",
                columns: table => new
                {
                    FormFieldValidationRuleId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TypeFormFieldValidationRuleTypeId = table.Column<int>(nullable: true),
                    Constraint = table.Column<string>(nullable: true),
                    FormFieldValidationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormFieldValidationRule", x => x.FormFieldValidationRuleId);
                    table.ForeignKey(
                        name: "FK_FormFieldValidationRule_FormFieldValidation_FormFieldValida~",
                        column: x => x.FormFieldValidationId,
                        principalTable: "FormFieldValidation",
                        principalColumn: "FormFieldValidationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FormFieldValidationRule_FormFieldValidationRuleTypes_TypeFo~",
                        column: x => x.TypeFormFieldValidationRuleTypeId,
                        principalTable: "FormFieldValidationRuleTypes",
                        principalColumn: "FormFieldValidationRuleTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "FormFieldValidationRuleTypes",
                columns: new[] { "FormFieldValidationRuleTypeId", "Name" },
                values: new object[,]
                {
                    { 1, "Required" },
                    { 2, "Minimum_Value" },
                    { 3, "Maxmimum__Value" },
                    { 4, "Minimum_Length" },
                    { 5, "Maxmimum_Length" },
                    { 6, "Minimum_Date" },
                    { 7, "Maxmimum_Date" },
                    { 8, "Regex" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_FormFieldValidationRule_FormFieldValidationId",
                table: "FormFieldValidationRule",
                column: "FormFieldValidationId");

            migrationBuilder.CreateIndex(
                name: "IX_FormFieldValidationRule_TypeFormFieldValidationRuleTypeId",
                table: "FormFieldValidationRule",
                column: "TypeFormFieldValidationRuleTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_FormFieldTypeRuleType_FormFieldValidationRuleTypes_FormVali~",
                table: "FormFieldTypeRuleType",
                column: "FormValidationRuleTypeId",
                principalTable: "FormFieldValidationRuleTypes",
                principalColumn: "FormFieldValidationRuleTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormFieldTypeRuleType_FormFieldValidationRuleTypes_FormVali~",
                table: "FormFieldTypeRuleType");

            migrationBuilder.DropTable(
                name: "FormFieldValidationRule");

            migrationBuilder.DropTable(
                name: "FormFieldValidationRuleTypes");

            migrationBuilder.CreateTable(
                name: "FormValidationRuleTypes",
                columns: table => new
                {
                    FormValidationRuleTypeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormValidationRuleTypes", x => x.FormValidationRuleTypeId);
                });

            migrationBuilder.CreateTable(
                name: "FormValidationRule",
                columns: table => new
                {
                    FormValidationRuleId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Constraint = table.Column<string>(type: "text", nullable: true),
                    FormFieldValidationId = table.Column<int>(type: "integer", nullable: true),
                    TypeFormValidationRuleTypeId = table.Column<int>(type: "integer", nullable: true)
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
                        name: "FK_FormValidationRule_FormValidationRuleTypes_TypeFormValidati~",
                        column: x => x.TypeFormValidationRuleTypeId,
                        principalTable: "FormValidationRuleTypes",
                        principalColumn: "FormValidationRuleTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "FormValidationRuleTypes",
                columns: new[] { "FormValidationRuleTypeId", "Name" },
                values: new object[,]
                {
                    { 1, "Required" },
                    { 2, "Minimum_Value" },
                    { 3, "Maxmimum__Value" },
                    { 4, "Minimum_Length" },
                    { 5, "Maxmimum_Length" },
                    { 6, "Minimum_Date" },
                    { 7, "Maxmimum_Date" },
                    { 8, "Regex" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_FormValidationRule_FormFieldValidationId",
                table: "FormValidationRule",
                column: "FormFieldValidationId");

            migrationBuilder.CreateIndex(
                name: "IX_FormValidationRule_TypeFormValidationRuleTypeId",
                table: "FormValidationRule",
                column: "TypeFormValidationRuleTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_FormFieldTypeRuleType_FormValidationRuleTypes_FormValidatio~",
                table: "FormFieldTypeRuleType",
                column: "FormValidationRuleTypeId",
                principalTable: "FormValidationRuleTypes",
                principalColumn: "FormValidationRuleTypeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
