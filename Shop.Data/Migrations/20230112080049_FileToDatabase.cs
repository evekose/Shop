using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Data.Migrations
{
    public partial class FileToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SpaceShips",
                table: "SpaceShips");

            migrationBuilder.RenameTable(
                name: "SpaceShips",
                newName: "Spaceships");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Spaceships",
                table: "Spaceships",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "FileToDatabase",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageData = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    SpaceshipId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileToDatabase", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileToDatabase");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Spaceships",
                table: "Spaceships");

            migrationBuilder.RenameTable(
                name: "Spaceships",
                newName: "SpaceShips");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SpaceShips",
                table: "SpaceShips",
                column: "Id");
        }
    }
}
