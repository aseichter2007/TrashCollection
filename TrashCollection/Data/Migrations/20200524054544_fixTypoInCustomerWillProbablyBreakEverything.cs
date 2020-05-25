using Microsoft.EntityFrameworkCore.Migrations;

namespace TrashCollection.Data.Migrations
{
    public partial class fixTypoInCustomerWillProbablyBreakEverything : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "SuspendStert",
                table: "Customers");

            migrationBuilder.AddColumn<string>(
                name: "SuspendStart",
                table: "Customers",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4a8fdc5b-33a4-4b05-a171-9db29c599ab5", "4fa322c4-cff1-4288-b937-7a3e8042caf4", "Employee", "EMPLOYEE" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "37b6986b-5fcb-49eb-ba2e-9e5da02d9b2b", "1bd7a6ec-9cbe-4e15-9365-ec7d0ddcc6fe", "Customer", "CUSTOMER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "37b6986b-5fcb-49eb-ba2e-9e5da02d9b2b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4a8fdc5b-33a4-4b05-a171-9db29c599ab5");

            migrationBuilder.DropColumn(
                name: "SuspendStart",
                table: "Customers");

            migrationBuilder.AddColumn<string>(
                name: "SuspendStert",
                table: "Customers",
                type: "nvarchar(max)",
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
    }
}
