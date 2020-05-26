using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TrashCollection.Data.Migrations
{
    public partial class PickupDatetoDatetimeop2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8a040f54-b930-4d04-8a01-43fda41079d1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c9ffb5b8-22b4-4a38-9b2c-3b87b9fc83a8");

            migrationBuilder.CreateTable(
                name: "Pickups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(nullable: false),
                    AddressID = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Regular = table.Column<bool>(nullable: false),
                    price = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pickups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pickups_Addresses_AddressID",
                        column: x => x.AddressID,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pickups_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1d235a9d-1cf8-4df5-9898-bdffd7579082", "4ef808bd-6a61-4118-8f1e-9cb2c73f31c5", "Employee", "EMPLOYEE" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "303dc517-ee37-44ed-875a-bb223ffde898", "e9aa4aaa-f0a8-42ef-8809-8519e4478308", "Customer", "CUSTOMER" });

            migrationBuilder.CreateIndex(
                name: "IX_Pickups_AddressID",
                table: "Pickups",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_Pickups_CustomerId",
                table: "Pickups",
                column: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pickups");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1d235a9d-1cf8-4df5-9898-bdffd7579082");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "303dc517-ee37-44ed-875a-bb223ffde898");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c9ffb5b8-22b4-4a38-9b2c-3b87b9fc83a8", "8fef49e1-fdeb-47f3-a2fb-0e34bd743161", "Employee", "EMPLOYEE" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8a040f54-b930-4d04-8a01-43fda41079d1", "ba099886-abe2-408a-b4a2-31c280cda403", "Customer", "CUSTOMER" });
        }
    }
}
