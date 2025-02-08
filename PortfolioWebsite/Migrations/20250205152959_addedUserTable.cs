using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortfolioWebsite.Migrations
{
    /// <inheritdoc />
    public partial class addedUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LinkedIn",
                table: "Tbl_PersonalInfo",
                newName: "LinkedInURL");

            migrationBuilder.RenameColumn(
                name: "Instagram",
                table: "Tbl_PersonalInfo",
                newName: "LinkedInTitle");

            migrationBuilder.RenameColumn(
                name: "GitHub",
                table: "Tbl_PersonalInfo",
                newName: "InstagramURL");

            migrationBuilder.AddColumn<string>(
                name: "GitHubTitle",
                table: "Tbl_PersonalInfo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GitHubURL",
                table: "Tbl_PersonalInfo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "InstagramTitle",
                table: "Tbl_PersonalInfo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GitHubTitle",
                table: "Tbl_PersonalInfo");

            migrationBuilder.DropColumn(
                name: "GitHubURL",
                table: "Tbl_PersonalInfo");

            migrationBuilder.DropColumn(
                name: "InstagramTitle",
                table: "Tbl_PersonalInfo");

            migrationBuilder.RenameColumn(
                name: "LinkedInURL",
                table: "Tbl_PersonalInfo",
                newName: "LinkedIn");

            migrationBuilder.RenameColumn(
                name: "LinkedInTitle",
                table: "Tbl_PersonalInfo",
                newName: "Instagram");

            migrationBuilder.RenameColumn(
                name: "InstagramURL",
                table: "Tbl_PersonalInfo",
                newName: "GitHub");
        }
    }
}
