using Microsoft.EntityFrameworkCore.Migrations;

namespace JForms.Data.Migrations
{
    public partial class _5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormSubmission_Forms_FormId",
                table: "FormSubmission");

            migrationBuilder.DropForeignKey(
                name: "FK_FormSubmissionValue_FormSubmission_SubmissionFormSubmission~",
                table: "FormSubmissionValue");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FormSubmission",
                table: "FormSubmission");

            migrationBuilder.RenameTable(
                name: "FormSubmission",
                newName: "FormSubmissions");

            migrationBuilder.RenameIndex(
                name: "IX_FormSubmission_FormId",
                table: "FormSubmissions",
                newName: "IX_FormSubmissions_FormId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FormSubmissions",
                table: "FormSubmissions",
                column: "FormSubmissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_FormSubmissions_Forms_FormId",
                table: "FormSubmissions",
                column: "FormId",
                principalTable: "Forms",
                principalColumn: "FormId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FormSubmissionValue_FormSubmissions_SubmissionFormSubmissio~",
                table: "FormSubmissionValue",
                column: "SubmissionFormSubmissionId",
                principalTable: "FormSubmissions",
                principalColumn: "FormSubmissionId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormSubmissions_Forms_FormId",
                table: "FormSubmissions");

            migrationBuilder.DropForeignKey(
                name: "FK_FormSubmissionValue_FormSubmissions_SubmissionFormSubmissio~",
                table: "FormSubmissionValue");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FormSubmissions",
                table: "FormSubmissions");

            migrationBuilder.RenameTable(
                name: "FormSubmissions",
                newName: "FormSubmission");

            migrationBuilder.RenameIndex(
                name: "IX_FormSubmissions_FormId",
                table: "FormSubmission",
                newName: "IX_FormSubmission_FormId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FormSubmission",
                table: "FormSubmission",
                column: "FormSubmissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_FormSubmission_Forms_FormId",
                table: "FormSubmission",
                column: "FormId",
                principalTable: "Forms",
                principalColumn: "FormId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FormSubmissionValue_FormSubmission_SubmissionFormSubmission~",
                table: "FormSubmissionValue",
                column: "SubmissionFormSubmissionId",
                principalTable: "FormSubmission",
                principalColumn: "FormSubmissionId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
