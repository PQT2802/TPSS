using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TPSS.Data.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    ProjectID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    ProjectName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Project__761ABED0232A2A0E", x => x.ProjectID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Phone = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    Digital_signature = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Verify = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__User__1788CCAC2A7756CD", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Property",
                columns: table => new
                {
                    PropertyID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    ProjectID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    PropertyName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Owner = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Price = table.Column<double>(type: "float", nullable: true),
                    Verify = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Property__70C9A755F657A2CD", x => x.PropertyID);
                    table.ForeignKey(
                        name: "FK__Property__Owner__2C3393D0",
                        column: x => x.Owner,
                        principalTable: "User",
                        principalColumn: "UserID");
                    table.ForeignKey(
                        name: "FK__Property__Projec__2B3F6F97",
                        column: x => x.ProjectID,
                        principalTable: "Project",
                        principalColumn: "ProjectID");
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    UserID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Role__FA2998BF699973A0", x => new { x.UserID, x.Role });
                    table.ForeignKey(
                        name: "FK__Role__UserID__267ABA7A",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "LikeList",
                columns: table => new
                {
                    LikeID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    PropertyID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__LikeList__A2922CF437D78B83", x => x.LikeID);
                    table.ForeignKey(
                        name: "FK__LikeList__Proper__3B75D760",
                        column: x => x.PropertyID,
                        principalTable: "Property",
                        principalColumn: "PropertyID");
                    table.ForeignKey(
                        name: "FK__LikeList__UserID__3A81B327",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Reservation",
                columns: table => new
                {
                    ReservationID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    SellerID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    BuyerID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    PropertyID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    BookingDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Priority = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Reservat__B7EE5F04AD628F07", x => x.ReservationID);
                    table.ForeignKey(
                        name: "FK__Reservati__Buyer__30F848ED",
                        column: x => x.BuyerID,
                        principalTable: "User",
                        principalColumn: "UserID");
                    table.ForeignKey(
                        name: "FK__Reservati__Prope__2F10007B",
                        column: x => x.PropertyID,
                        principalTable: "Property",
                        principalColumn: "PropertyID");
                    table.ForeignKey(
                        name: "FK__Reservati__Selle__300424B4",
                        column: x => x.SellerID,
                        principalTable: "User",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Contract",
                columns: table => new
                {
                    ContractID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    ReservationID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    ContractDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ContractTerms = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Deposit = table.Column<double>(type: "float", nullable: true),
                    ContractStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Contract__C90D34099AA800A1", x => x.ContractID);
                    table.ForeignKey(
                        name: "FK__Contract__Reserv__33D4B598",
                        column: x => x.ReservationID,
                        principalTable: "Reservation",
                        principalColumn: "ReservationID");
                });

            migrationBuilder.CreateTable(
                name: "Transaction_Processing",
                columns: table => new
                {
                    ProcessID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    ContractID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Commission_Calculation = table.Column<double>(type: "float", nullable: true),
                    Payment = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Transact__1B39A9762989D08C", x => x.ProcessID);
                    table.ForeignKey(
                        name: "FK__Transacti__Contr__36B12243",
                        column: x => x.ContractID,
                        principalTable: "Contract",
                        principalColumn: "ContractID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contract_ReservationID",
                table: "Contract",
                column: "ReservationID");

            migrationBuilder.CreateIndex(
                name: "IX_LikeList_PropertyID",
                table: "LikeList",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "UQ_UserProperty",
                table: "LikeList",
                columns: new[] { "UserID", "PropertyID" },
                unique: true,
                filter: "[UserID] IS NOT NULL AND [PropertyID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Property_Owner",
                table: "Property",
                column: "Owner");

            migrationBuilder.CreateIndex(
                name: "IX_Property_ProjectID",
                table: "Property",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_BuyerID",
                table: "Reservation",
                column: "BuyerID");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_PropertyID",
                table: "Reservation",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_SellerID",
                table: "Reservation",
                column: "SellerID");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_Processing_ContractID",
                table: "Transaction_Processing",
                column: "ContractID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LikeList");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Transaction_Processing");

            migrationBuilder.DropTable(
                name: "Contract");

            migrationBuilder.DropTable(
                name: "Reservation");

            migrationBuilder.DropTable(
                name: "Property");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Project");
        }
    }
}
