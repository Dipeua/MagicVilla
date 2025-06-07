using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MagicVilla_VillaAPI.Migrations
{
    /// <inheritdoc />
    public partial class Initdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Villas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Occupancy = table.Column<int>(type: "int", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sqft = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amenity = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Villas", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenity", "Details", "ImageUrl", "Name", "Occupancy", "Rate", "Sqft" },
                values: new object[,]
                {
                    { 1, "Pool, WiFi, Breakfast", "A luxurious villa with stunning views.", "https://example.com/royal-villa.jpg", "Royal Villa", 4, "200", "1500" },
                    { 2, "Pool, WiFi, Breakfast, Spa", "A beautiful villa right on the beach.", "https://example.com/beachfront-villa.jpg", "Beachfront Villa", 6, "300", "2000" },
                    { 3, "WiFi, Breakfast", "A cozy villa in the mountains.", "https://example.com/mountain-retreat.jpg", "Mountain Retreat", 2, "150", "800" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Villas");
        }
    }
}
