using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace APISYMBOL.Migrations
{
    /// <inheritdoc />
    public partial class Portfilos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "20f73872-d8be-4d4d-9532-fa10b9092816");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9ee62c50-e172-4230-bc96-c2764c912773");

            migrationBuilder.CreateTable(
                name: "Portfollos",
                columns: table => new
                {
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StockId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Portfollos", x => new { x.AppUserId, x.StockId });
                    table.ForeignKey(
                        name: "FK_Portfollos_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Portfollos_Stocks_StockId",
                        column: x => x.StockId,
                        principalTable: "Stocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1de0accc-fd4a-45f2-bd28-7ec148d1b478", null, "User", " USER" },
                    { "acbefc68-2331-4498-a240-cf3569d06d74", null, "Admin", " ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Portfollos_StockId",
                table: "Portfollos",
                column: "StockId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Portfollos");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1de0accc-fd4a-45f2-bd28-7ec148d1b478");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "acbefc68-2331-4498-a240-cf3569d06d74");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "20f73872-d8be-4d4d-9532-fa10b9092816", null, "User", " USER" },
                    { "9ee62c50-e172-4230-bc96-c2764c912773", null, "Admin", " ADMIN" }
                });
        }
    }
}
