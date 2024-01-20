using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TPSS.Data.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "User",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Transaction",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Reservation",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Property",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Project",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Payment",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Contract",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "User");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Contract");
        }
    }
}
