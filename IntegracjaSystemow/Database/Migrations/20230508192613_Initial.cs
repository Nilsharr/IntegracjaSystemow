using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace IntegracjaSystemow.Database.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Laptops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Producer = table.Column<string>(type: "text", nullable: false),
                    ScreenDiagonal = table.Column<string>(type: "text", nullable: true),
                    ScreenResolution = table.Column<string>(type: "text", nullable: true),
                    ScreenSurface = table.Column<string>(type: "text", nullable: true),
                    IsTouchScreen = table.Column<string>(type: "text", nullable: false),
                    Processor = table.Column<string>(type: "text", nullable: true),
                    PhysicalCores = table.Column<int>(type: "integer", nullable: true),
                    ClockSpeed = table.Column<int>(type: "integer", nullable: true),
                    MemorySize = table.Column<string>(type: "text", nullable: true),
                    DiskCapacity = table.Column<string>(type: "text", nullable: true),
                    DiskType = table.Column<string>(type: "text", nullable: true),
                    GraphicCard = table.Column<string>(type: "text", nullable: true),
                    GraphicCardMemory = table.Column<string>(type: "text", nullable: true),
                    OperatingSystem = table.Column<string>(type: "text", nullable: true),
                    PhysicalDriveType = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Laptops", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Laptops");
        }
    }
}
