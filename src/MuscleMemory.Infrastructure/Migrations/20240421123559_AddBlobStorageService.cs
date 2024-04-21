using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MuscleMemory.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBlobStorageService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PictureUrl",
                table: "Exercises",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PictureUrl",
                table: "Exercises");
        }
    }
}
