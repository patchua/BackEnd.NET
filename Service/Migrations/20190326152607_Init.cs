using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InstrumentPrices",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Portfolio = table.Column<string>(nullable: false),
                    InstrumentOwner = table.Column<string>(nullable: false),
                    Instrument = table.Column<string>(nullable: false),
                    TimeSlot = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstrumentPrices", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InstrumentPrices_TimeSlot",
                table: "InstrumentPrices",
                column: "TimeSlot");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InstrumentPrices");
        }
    }
}
