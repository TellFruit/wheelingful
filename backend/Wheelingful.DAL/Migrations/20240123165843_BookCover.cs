using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wheelingful.DAL.Migrations
{
    /// <inheritdoc />
    public partial class BookCover : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CoverId",
                table: "Books",
                type: "varchar(24)",
                maxLength: 24,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverId",
                table: "Books");
        }
    }
}
