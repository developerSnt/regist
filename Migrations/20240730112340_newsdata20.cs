using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace regist.Migrations
{
    /// <inheritdoc />
    public partial class newsdata20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NewsDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    tital = table.Column<string>(type: "TEXT", nullable: false),
                    date = table.Column<string>(type: "TEXT", nullable: false),
                    imageurl = table.Column<string>(type: "TEXT", nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsDatas", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NewsDatas");
        }
    }
}
