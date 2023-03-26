using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrganizedMorning.Migrations
{
    /// <inheritdoc />
    public partial class encodedtitle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EncodedTitle",
                table: "Times",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EncodedTitle",
                table: "MorningPlans",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EncodedTitle",
                table: "Times");

            migrationBuilder.DropColumn(
                name: "EncodedTitle",
                table: "MorningPlans");
        }
    }
}
