using System;
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
            migrationBuilder.DropForeignKey(
                name: "FK__Property__Owner__2C3393D0",
                table: "Property");

            migrationBuilder.DropForeignKey(
                name: "FK__Role__UserID__267ABA7A",
                table: "Role");

            migrationBuilder.DropTable(
                name: "Transaction_Processing");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Role__FA2998BF699973A0",
                table: "Role");

            migrationBuilder.DropIndex(
                name: "IX_Property_Owner",
                table: "Property");

            migrationBuilder.DropIndex(
                name: "UQ_UserProperty",
                table: "LikeList");

            migrationBuilder.DropColumn(
                name: "Digital_signature",
                table: "User");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "User");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "User");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "Owner",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "PropertyName",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "Verify",
                table: "Property");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "Role",
                newName: "RoleName");

            migrationBuilder.AlterColumn<string>(
                name: "Verify",
                table: "User",
                type: "nchar(10)",
                fixedLength: true,
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "User",
                type: "nchar(10)",
                fixedLength: true,
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "User",
                type: "nchar(10)",
                fixedLength: true,
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "User",
                type: "nchar(10)",
                fixedLength: true,
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoleID",
                table: "User",
                type: "nchar(10)",
                fixedLength: true,
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoleID",
                table: "Role",
                type: "nchar(10)",
                fixedLength: true,
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectID",
                table: "Property",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15,
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Area",
                table: "Property",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Property",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "District",
                table: "Property",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Property",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PropertyTitle",
                table: "Property",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Province",
                table: "Property",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "Property",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ward",
                table: "Property",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Role",
                table: "Role",
                column: "RoleID");

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
                name: "PropertyDetail",
                columns: table => new
                {
                    PropertyDetailID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
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
                    table.ForeignKey(
                        name: "FK_PropertyDetail_Property",
                        column: x => x.PropertyID,
                        principalTable: "Property",
                        principalColumn: "PropertyID");
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
                name: "UserDetail",
                columns: table => new
                {
                    UserID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PersonalID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateOnly>(type: "date", nullable: true),
                    UpdateDate = table.Column<DateOnly>(type: "date", nullable: true),
                    CreateBy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    TaxIdentificationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UserDeta__1788CCAC0F790C30", x => x.UserID);
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
                name: "IX_User_RoleID",
                table: "User",
                column: "RoleID");

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
                name: "IX_PropertyDetail_PropertyID",
                table: "PropertyDetail",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_Processing_ContractID",
                table: "Transaction",
                column: "ContractID");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Role",
                table: "User",
                column: "RoleID",
                principalTable: "Role",
                principalColumn: "RoleID");

            migrationBuilder.AddForeignKey(
                name: "FK_User_UserDetail",
                table: "User",
                column: "UserID",
                principalTable: "UserDetail",
                principalColumn: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Role",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_UserDetail",
                table: "User");

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

            migrationBuilder.DropIndex(
                name: "IX_User_RoleID",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Role",
                table: "Role");

            migrationBuilder.DropIndex(
                name: "UQ_UserProperty",
                table: "LikeList");

            migrationBuilder.DropColumn(
                name: "RoleID",
                table: "User");

            migrationBuilder.DropColumn(
                name: "RoleID",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "Area",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "District",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "PropertyTitle",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "Province",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "Ward",
                table: "Property");

            migrationBuilder.RenameColumn(
                name: "RoleName",
                table: "Role",
                newName: "Role");

            migrationBuilder.AlterColumn<bool>(
                name: "Verify",
                table: "User",
                type: "bit",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nchar(10)",
                oldFixedLength: true,
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "User",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nchar(10)",
                oldFixedLength: true,
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "User",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nchar(10)",
                oldFixedLength: true,
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "User",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nchar(10)",
                oldFixedLength: true,
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Digital_signature",
                table: "User",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "User",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "User",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "User",
                type: "varchar(15)",
                unicode: false,
                maxLength: 15,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "Role",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectID",
                table: "Property",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AddColumn<string>(
                name: "Owner",
                table: "Property",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PropertyName",
                table: "Property",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Verify",
                table: "Property",
                type: "bit",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK__Role__FA2998BF699973A0",
                table: "Role",
                columns: new[] { "UserID", "Role" });

            migrationBuilder.CreateTable(
                name: "Transaction_Processing",
                columns: table => new
                {
                    ProcessID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    ContractID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Commission_Calculation = table.Column<double>(type: "float", nullable: true),
                    Payment = table.Column<double>(type: "float", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
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
                name: "IX_Property_Owner",
                table: "Property",
                column: "Owner");

            migrationBuilder.CreateIndex(
                name: "UQ_UserProperty",
                table: "LikeList",
                columns: new[] { "UserID", "PropertyID" },
                unique: true,
                filter: "[UserID] IS NOT NULL AND [PropertyID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_Processing_ContractID",
                table: "Transaction_Processing",
                column: "ContractID");

            migrationBuilder.AddForeignKey(
                name: "FK__Property__Owner__2C3393D0",
                table: "Property",
                column: "Owner",
                principalTable: "User",
                principalColumn: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK__Role__UserID__267ABA7A",
                table: "Role",
                column: "UserID",
                principalTable: "User",
                principalColumn: "UserID");
        }
    }
}
