using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FourthMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "File",
                table: "Images",
                newName: "FilePath");

            migrationBuilder.RenameIndex(
                name: "IX_Images_File",
                table: "Images",
                newName: "IX_Images_FilePath");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Images",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Images");

            migrationBuilder.RenameColumn(
                name: "FilePath",
                table: "Images",
                newName: "File");

            migrationBuilder.RenameIndex(
                name: "IX_Images_FilePath",
                table: "Images",
                newName: "IX_Images_File");
        }
    }
}
