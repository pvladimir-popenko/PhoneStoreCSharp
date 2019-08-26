using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MvcApplicationCSharp5.Migrations
{
    public partial class CreateCompaniesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Company",
                table: "Phones");

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Phones",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Phones_CompanyId",
                table: "Phones",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Phones_Companies_CompanyId",
                table: "Phones",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Phones_Companies_CompanyId",
                table: "Phones");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Phones_CompanyId",
                table: "Phones");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Phones");

            migrationBuilder.AddColumn<string>(
                name: "Company",
                table: "Phones",
                nullable: true);
        }
    }
}
