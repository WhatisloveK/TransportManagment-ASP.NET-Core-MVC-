using Microsoft.EntityFrameworkCore.Migrations;

namespace TransportManagment.Migrations
{
    public partial class editedtransport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Transport");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Transport",
                nullable: false,
                defaultValue: 0);
        }
    }
}
