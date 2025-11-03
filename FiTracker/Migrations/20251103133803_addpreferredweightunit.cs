using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FiTracker.Migrations
{
    /// <inheritdoc />
    public partial class addpreferredweightunit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PreferredWeightUnit",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "kg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreferredWeightUnit",
                table: "AspNetUsers");
        }
    }
}
