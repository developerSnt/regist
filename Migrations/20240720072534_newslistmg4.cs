using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace regist.Migrations
{
    /// <inheritdoc />
    public partial class newslistmg4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Domion",
                table: "DataInfos",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Domion",
                table: "DataInfos");
        }
    }
}
