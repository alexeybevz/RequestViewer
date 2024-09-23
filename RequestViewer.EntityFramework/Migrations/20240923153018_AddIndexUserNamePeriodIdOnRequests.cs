using Microsoft.EntityFrameworkCore.Migrations;

namespace RequestViewer.EntityFramework.Migrations
{
    public partial class AddIndexUserNamePeriodIdOnRequests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Requests_UserName_PeriodId",
                table: "Requests",
                columns: new[] { "UserName", "PeriodId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Requests_UserName_PeriodId",
                table: "Requests");
        }
    }
}
