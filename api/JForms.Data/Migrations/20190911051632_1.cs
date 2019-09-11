using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace JForms.Data.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FormFieldTypes",
                columns: table => new
                {
                    FormFieldTypeId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    MultipleOptions = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormFieldTypes", x => x.FormFieldTypeId);
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
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
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
                        name: "FK_FormFieldTypeRuleType_FormFieldTypes_FormFieldTypeId",
                        column: x => x.FormFieldTypeId,
                        principalTable: "FormFieldTypes",
                        principalColumn: "FormFieldTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FormFieldTypeRuleType_FormFieldValidationRuleTypes_FormVali~",
                        column: x => x.FormValidationRuleTypeId,
                        principalTable: "FormFieldValidationRuleTypes",
                        principalColumn: "FormFieldValidationRuleTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormFieldValidationRule",
                columns: table => new
                {
                    FormFieldValidationRuleId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FormFieldValidationRuleTypeId = table.Column<int>(nullable: false),
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
                        name: "FK_FormFieldValidationRule_FormFieldValidationRuleTypes_FormFi~",
                        column: x => x.FormFieldValidationRuleTypeId,
                        principalTable: "FormFieldValidationRuleTypes",
                        principalColumn: "FormFieldValidationRuleTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Forms",
                columns: table => new
                {
                    FormId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forms", x => x.FormId);
                    table.ForeignKey(
                        name: "FK_Forms_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FormField",
                columns: table => new
                {
                    FormFieldId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    FormFieldTypeId = table.Column<int>(nullable: false),
                    ValidationFormFieldValidationId = table.Column<int>(nullable: true),
                    FormId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormField", x => x.FormFieldId);
                    table.ForeignKey(
                        name: "FK_FormField_FormFieldTypes_FormFieldTypeId",
                        column: x => x.FormFieldTypeId,
                        principalTable: "FormFieldTypes",
                        principalColumn: "FormFieldTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FormField_Forms_FormId",
                        column: x => x.FormId,
                        principalTable: "Forms",
                        principalColumn: "FormId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FormField_FormFieldValidation_ValidationFormFieldValidation~",
                        column: x => x.ValidationFormFieldValidationId,
                        principalTable: "FormFieldValidation",
                        principalColumn: "FormFieldValidationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FormOrigin",
                columns: table => new
                {
                    FormOriginId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Url = table.Column<string>(nullable: true),
                    FormId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormOrigin", x => x.FormOriginId);
                    table.ForeignKey(
                        name: "FK_FormOrigin_Forms_FormId",
                        column: x => x.FormId,
                        principalTable: "Forms",
                        principalColumn: "FormId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FormSubmission",
                columns: table => new
                {
                    FormSubmissionId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FormId = table.Column<int>(nullable: true),
                    Success = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormSubmission", x => x.FormSubmissionId);
                    table.ForeignKey(
                        name: "FK_FormSubmission_Forms_FormId",
                        column: x => x.FormId,
                        principalTable: "Forms",
                        principalColumn: "FormId",
                        onDelete: ReferentialAction.Restrict);
                });

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
                name: "FormSubmissionValue",
                columns: table => new
                {
                    FormSubmissionValueId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SubmissionFormSubmissionId = table.Column<int>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    FieldFormFieldId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormSubmissionValue", x => x.FormSubmissionValueId);
                    table.ForeignKey(
                        name: "FK_FormSubmissionValue_FormField_FieldFormFieldId",
                        column: x => x.FieldFormFieldId,
                        principalTable: "FormField",
                        principalColumn: "FormFieldId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FormSubmissionValue_FormSubmission_SubmissionFormSubmission~",
                        column: x => x.SubmissionFormSubmissionId,
                        principalTable: "FormSubmission",
                        principalColumn: "FormSubmissionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "FormFieldTypes",
                columns: new[] { "FormFieldTypeId", "MultipleOptions", "Name" },
                values: new object[,]
                {
                    { 1, false, "String" },
                    { 2, false, "Number" },
                    { 3, false, "Date" },
                    { 4, true, "RadioButton" },
                    { 5, true, "DropDown" },
                    { 6, true, "CheckBox" }
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

            migrationBuilder.InsertData(
                table: "FormFieldTypeRuleType",
                columns: new[] { "FormFieldTypeId", "FormValidationRuleTypeId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 2, 2 },
                    { 2, 3 },
                    { 1, 4 },
                    { 1, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_FormField_FormFieldTypeId",
                table: "FormField",
                column: "FormFieldTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FormField_FormId",
                table: "FormField",
                column: "FormId");

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
                name: "IX_FormFieldValidationRule_FormFieldValidationId",
                table: "FormFieldValidationRule",
                column: "FormFieldValidationId");

            migrationBuilder.CreateIndex(
                name: "IX_FormFieldValidationRule_FormFieldValidationRuleTypeId",
                table: "FormFieldValidationRule",
                column: "FormFieldValidationRuleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FormOrigin_FormId",
                table: "FormOrigin",
                column: "FormId");

            migrationBuilder.CreateIndex(
                name: "IX_Forms_UserId",
                table: "Forms",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FormSubmission_FormId",
                table: "FormSubmission",
                column: "FormId");

            migrationBuilder.CreateIndex(
                name: "IX_FormSubmissionValue_FieldFormFieldId",
                table: "FormSubmissionValue",
                column: "FieldFormFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_FormSubmissionValue_SubmissionFormSubmissionId",
                table: "FormSubmissionValue",
                column: "SubmissionFormSubmissionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FormFieldOption");

            migrationBuilder.DropTable(
                name: "FormFieldTypeRuleType");

            migrationBuilder.DropTable(
                name: "FormFieldValidationRule");

            migrationBuilder.DropTable(
                name: "FormOrigin");

            migrationBuilder.DropTable(
                name: "FormSubmissionValue");

            migrationBuilder.DropTable(
                name: "FormFieldValidationRuleTypes");

            migrationBuilder.DropTable(
                name: "FormField");

            migrationBuilder.DropTable(
                name: "FormSubmission");

            migrationBuilder.DropTable(
                name: "FormFieldTypes");

            migrationBuilder.DropTable(
                name: "FormFieldValidation");

            migrationBuilder.DropTable(
                name: "Forms");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
