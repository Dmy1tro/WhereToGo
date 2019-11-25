using Microsoft.EntityFrameworkCore.Migrations;

namespace WhereToGoWebApi.Migrations
{
    public partial class organaizerChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Organizers",
                newName: "Position");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Organizers",
                newName: "PlaceName");

            migrationBuilder.AddColumn<string>(
                name: "InstType",
                table: "Organizers",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TelNumber",
                table: "Organizers",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InstType",
                table: "Organizers");

            migrationBuilder.DropColumn(
                name: "TelNumber",
                table: "Organizers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Position",
                table: "Organizers",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "PlaceName",
                table: "Organizers",
                newName: "Description");
        }
    }
}
