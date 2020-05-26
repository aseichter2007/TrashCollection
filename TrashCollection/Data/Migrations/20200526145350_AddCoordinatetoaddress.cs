using Microsoft.EntityFrameworkCore.Migrations;

namespace TrashCollection.Data.Migrations
{
    public partial class AddCoordinatetoaddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.AddColumn<string>(
                name: "Coordinate",
                table: "Addresses",
                nullable: true);

          
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
         

            migrationBuilder.DropColumn(
                name: "Coordinate",
                table: "Addresses");

        }
    }
}
