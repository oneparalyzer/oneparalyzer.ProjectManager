using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace oneparalyzer.ProjectManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNewRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ProjectTasks_ResponsibleEmployeeId",
                table: "ProjectTasks",
                column: "ResponsibleEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_OfficeId",
                table: "Departments",
                column: "OfficeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Offices_OfficeId",
                table: "Departments",
                column: "OfficeId",
                principalTable: "Offices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTasks_Employees_ResponsibleEmployeeId",
                table: "ProjectTasks",
                column: "ResponsibleEmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Offices_OfficeId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTasks_Employees_ResponsibleEmployeeId",
                table: "ProjectTasks");

            migrationBuilder.DropIndex(
                name: "IX_ProjectTasks_ResponsibleEmployeeId",
                table: "ProjectTasks");

            migrationBuilder.DropIndex(
                name: "IX_Departments_OfficeId",
                table: "Departments");
        }
    }
}
