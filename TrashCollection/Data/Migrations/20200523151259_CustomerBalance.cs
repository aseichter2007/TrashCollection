using Microsoft.EntityFrameworkCore.Migrations;

namespace TrashCollection.Data.Migrations
{
    public partial class CustomerBalance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2e9ec04a-f002-4af9-8905-35dcd01164ca");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f95031ac-2068-42f3-b127-6bf4c2425ec5");

            migrationBuilder.AddColumn<string>(
                name: "Balance",
                table: "Customers",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0b0f7734-3dc2-416b-b585-9244773e5b88", "8e37f37b-0941-4d8f-94c6-2549c3ff23ac", "Employee", "EMPLOYEE" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a805249e-060e-4fad-9df6-490e79bdce32", "567cfeaf-17a3-4b0a-877a-214c330b3306", "Customer", "CUSTOMER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0b0f7734-3dc2-416b-b585-9244773e5b88");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a805249e-060e-4fad-9df6-490e79bdce32");

            migrationBuilder.DropColumn(
                name: "Balance",
                table: "Customers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f95031ac-2068-42f3-b127-6bf4c2425ec5", "8ab80ca4-69af-4a7f-a0cb-4b22e89cd28c", "Employee", "EMPLOYEE" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2e9ec04a-f002-4af9-8905-35dcd01164ca", "3c524c1b-23e4-4753-811d-55dc0a7e18ae", "Customer", "CUSTOMER" });
        }
    }
}
