﻿using System;
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
                name: "Role",
                columns: table => new
                {
                    RoleID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__User__1788CCAC2A7756CD", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "ProjectDetail",
                columns: table => new
                {
                    ProjectDetailID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    ProjectID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateOnly>(type: "date", nullable: true),
                    UpdateDate = table.Column<DateOnly>(type: "date", nullable: true),
                    CreateBy = table.Column<DateOnly>(type: "date", nullable: true),
                    UpdateBy = table.Column<DateOnly>(type: "date", nullable: true),
                    ProjectDescription = table.Column<string>(type: "text", nullable: true),
                    Verify = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectDetail", x => x.ProjectDetailID);
                    table.ForeignKey(
                        name: "FK_ProjectDetail_Project",
                        column: x => x.ProjectID,
                        principalTable: "Project",
                        principalColumn: "ProjectID");
                });

            migrationBuilder.CreateTable(
                name: "Property",
                columns: table => new
                {
                    PropertyID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    ProjectID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    PropertyTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Area = table.Column<double>(type: "float", nullable: true),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ward = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Property__70C9A755F657A2CD", x => x.PropertyID);
                    table.ForeignKey(
                        name: "FK__Property__Projec__2B3F6F97",
                        column: x => x.ProjectID,
                        principalTable: "Project",
                        principalColumn: "ProjectID");
                });

            migrationBuilder.CreateTable(
                name: "UserDetail",
                columns: table => new
                {
                    UserDetailID = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PersonalID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateOnly>(type: "date", nullable: true),
                    UpdateDate = table.Column<DateOnly>(type: "date", nullable: true),
                    CreateBy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    TaxIdentificationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UserDeta__1788CCAC0F790C30", x => x.UserDetailID);
                    table.ForeignKey(
                        name: "FK_UserDetail_Role",
                        column: x => x.RoleID,
                        principalTable: "Role",
                        principalColumn: "RoleID");
                    table.ForeignKey(
                        name: "FK_UserDetail_User",
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
                        name: "FK_LikeList_User",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID");
                    table.ForeignKey(
                        name: "FK__LikeList__Proper__3B75D760",
                        column: x => x.PropertyID,
                        principalTable: "Property",
                        principalColumn: "PropertyID");
                });

            migrationBuilder.CreateTable(
                name: "PropertyDetail",
                columns: table => new
                {
                    PropertyDetailID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    PropertyID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    OwnerID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    PropertyTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "ntext", nullable: true),
                    CreateDate = table.Column<DateOnly>(type: "date", nullable: true),
                    UpdateDate = table.Column<DateOnly>(type: "date", nullable: true),
                    CreateBy = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Service = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VerifyBy = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    VerifyDate = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyDetail", x => x.PropertyDetailID);
                    table.ForeignKey(
                        name: "FK_PropertyDetail_Property",
                        column: x => x.PropertyID,
                        principalTable: "Property",
                        principalColumn: "PropertyID");
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
                        name: "FK__Reservati__Prope__2F10007B",
                        column: x => x.PropertyID,
                        principalTable: "Property",
                        principalColumn: "PropertyID");
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
                name: "Transaction",
                columns: table => new
                {
                    TransactionID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    ContractID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Commission_Calculation = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Transact__1B39A9762989D08C", x => x.TransactionID);
                    table.ForeignKey(
                        name: "FK__Transacti__Contr__36B12243",
                        column: x => x.ContractID,
                        principalTable: "Contract",
                        principalColumn: "ContractID");
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    PaymentID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    TransactionID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_Payment_Transaction",
                        column: x => x.TransactionID,
                        principalTable: "Transaction",
                        principalColumn: "TransactionID");
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
                filter: "([UserID] IS NOT NULL AND [PropertyID] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_TransactionID",
                table: "Payment",
                column: "TransactionID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDetail_ProjectID",
                table: "ProjectDetail",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Property_ProjectID",
                table: "Property",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyDetail_PropertyID",
                table: "PropertyDetail",
                column: "PropertyID");

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
                table: "Transaction",
                column: "ContractID");

            migrationBuilder.CreateIndex(
                name: "IX_UserDetail_RoleID",
                table: "UserDetail",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_UserDetail_UserID",
                table: "UserDetail",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LikeList");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "ProjectDetail");

            migrationBuilder.DropTable(
                name: "PropertyDetail");

            migrationBuilder.DropTable(
                name: "UserDetail");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Contract");

            migrationBuilder.DropTable(
                name: "Reservation");

            migrationBuilder.DropTable(
                name: "Property");

            migrationBuilder.DropTable(
                name: "Project");
        }
    }
}