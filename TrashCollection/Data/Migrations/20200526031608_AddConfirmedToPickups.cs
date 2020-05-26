using Microsoft.EntityFrameworkCore.Migrations;

namespace TrashCollection.Data.Migrations
{
    public partial class AddConfirmedToPickups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          
            migrationBuilder.RenameColumn(
                name: "price",
                table: "Pickups",
                newName: "Price");

            migrationBuilder.AddColumn<bool>(
                name: "Confirmed",
                table: "Pickups",
                nullable: false,
                defaultValue: false);

           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.DropColumn(
                name: "Confirmed",
                table: "Pickups");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Pickups",
                newName: "price");

        }
    }
}
