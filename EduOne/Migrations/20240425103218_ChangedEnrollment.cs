using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduOne.Migrations
{
    /// <inheritdoc />
    public partial class ChangedEnrollment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Instructors_InstructorId",
                table: "Enrollments");

            migrationBuilder.RenameColumn(
                name: "InstructorId",
                table: "Enrollments",
                newName: "CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollments_InstructorId",
                table: "Enrollments",
                newName: "IX_Enrollments_CourseId");

            migrationBuilder.AlterColumn<string>(
                name: "Grade",
                table: "Enrollments",
                type: "nvarchar(1)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1)");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Courses_CourseId",
                table: "Enrollments",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Courses_CourseId",
                table: "Enrollments");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "Enrollments",
                newName: "InstructorId");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollments_CourseId",
                table: "Enrollments",
                newName: "IX_Enrollments_InstructorId");

            migrationBuilder.AlterColumn<string>(
                name: "Grade",
                table: "Enrollments",
                type: "nvarchar(1)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(1)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Instructors_InstructorId",
                table: "Enrollments",
                column: "InstructorId",
                principalTable: "Instructors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
