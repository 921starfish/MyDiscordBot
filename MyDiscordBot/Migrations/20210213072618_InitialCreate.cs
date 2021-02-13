using Microsoft.EntityFrameworkCore.Migrations;

namespace MyDiscordBot.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WORDWOLF_THEMES",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    A = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    B = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WORDWOLF_THEMES", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "WORDWOLF_THEMES",
                columns: new[] { "Id", "A", "B" },
                values: new object[] { 1, "りんご", "みかん" });

            migrationBuilder.InsertData(
                table: "WORDWOLF_THEMES",
                columns: new[] { "Id", "A", "B" },
                values: new object[] { 2, "たぬき", "きつね" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WORDWOLF_THEMES");
        }
    }
}
