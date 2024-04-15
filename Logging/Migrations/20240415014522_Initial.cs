using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Logging.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_setting",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_setting", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_setting_Key",
                table: "tbl_setting",
                column: "Key",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_setting");
        }
    }
}
