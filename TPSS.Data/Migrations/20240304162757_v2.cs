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
                name: "FK_Payment_Transaction",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyDetail_Property",
                table: "PropertyDetail");

            migrationBuilder.DropForeignKey(
                name: "FK__Transacti__Contr__36B12243",
                table: "Transaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Transact__1B39A9762989D08C",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_Processing_ContractID",
                table: "Transaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PropertyDetail",
                table: "PropertyDetail");

            migrationBuilder.DropIndex(
                name: "IX_PropertyDetail_PropertyID",
                table: "PropertyDetail");

            migrationBuilder.DropIndex(
                name: "IX_Payment_TransactionID",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "ContractID",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "Images",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "Images",
                table: "ProjectDetail");

            migrationBuilder.DropColumn(
                name: "TransactionID",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "ContractDate",
                table: "Contract");

            migrationBuilder.RenameColumn(
                name: "Commission_Calculation",
                table: "Transaction",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "PaymentID",
                table: "Payment",
                newName: "PaymentId");

            migrationBuilder.RenameColumn(
                name: "ContractTerms",
                table: "Contract",
                newName: "Contract");

            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "Transaction",
                type: "bit",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentID",
                table: "Transaction",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "PropertyID",
                table: "PropertyDetail",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreateBy",
                table: "PropertyDetail",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Project",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Payment",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PaymentId",
                table: "Payment",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15,
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Commission_Calculation",
                table: "Payment",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContractID",
                table: "Payment",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SignDate",
                table: "Contract",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transaction",
                table: "Transaction",
                column: "TransactionID");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Property__70C9A755F657A2CD",
                table: "PropertyDetail",
                column: "PropertyID");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Transact__1B39A9762989D08C",
                table: "Payment",
                column: "PaymentId");

            migrationBuilder.CreateTable(
                name: "Album",
                columns: table => new
                {
                    ImageId = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    PropertyId = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Album", x => x.ImageId);
                    table.ForeignKey(
                        name: "FK_Album_Property",
                        column: x => x.PropertyId,
                        principalTable: "Property",
                        principalColumn: "PropertyID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_PaymentID",
                table: "Transaction",
                column: "PaymentID");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_Processing_ContractID",
                table: "Payment",
                column: "ContractID");

            migrationBuilder.CreateIndex(
                name: "IX_Album_PropertyId",
                table: "Album",
                column: "PropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK__Transacti__Contr__36B12243",
                table: "Payment",
                column: "ContractID",
                principalTable: "Contract",
                principalColumn: "ContractID");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyDetail_Property",
                table: "PropertyDetail",
                column: "PropertyID",
                principalTable: "Property",
                principalColumn: "PropertyID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Payment",
                table: "Transaction",
                column: "PaymentID",
                principalTable: "Payment",
                principalColumn: "PaymentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Transacti__Contr__36B12243",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyDetail_Property",
                table: "PropertyDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Payment",
                table: "Transaction");

            migrationBuilder.DropTable(
                name: "Album");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transaction",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_PaymentID",
                table: "Transaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Property__70C9A755F657A2CD",
                table: "PropertyDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Transact__1B39A9762989D08C",
                table: "Payment");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_Processing_ContractID",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "PaymentID",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "CreateBy",
                table: "PropertyDetail");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "Commission_Calculation",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "ContractID",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "SignDate",
                table: "Contract");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Transaction",
                newName: "Commission_Calculation");

            migrationBuilder.RenameColumn(
                name: "PaymentId",
                table: "Payment",
                newName: "PaymentID");

            migrationBuilder.RenameColumn(
                name: "Contract",
                table: "Contract",
                newName: "ContractTerms");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Transaction",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContractID",
                table: "Transaction",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PropertyID",
                table: "PropertyDetail",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AddColumn<string>(
                name: "Images",
                table: "Property",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Images",
                table: "ProjectDetail",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "Payment",
                type: "bit",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PaymentID",
                table: "Payment",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AddColumn<string>(
                name: "TransactionID",
                table: "Payment",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateOnly>(
                name: "ContractDate",
                table: "Contract",
                type: "date",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK__Transact__1B39A9762989D08C",
                table: "Transaction",
                column: "TransactionID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PropertyDetail",
                table: "PropertyDetail",
                column: "PropertyDetailID");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_Processing_ContractID",
                table: "Transaction",
                column: "ContractID");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyDetail_PropertyID",
                table: "PropertyDetail",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_TransactionID",
                table: "Payment",
                column: "TransactionID");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Transaction",
                table: "Payment",
                column: "TransactionID",
                principalTable: "Transaction",
                principalColumn: "TransactionID");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyDetail_Property",
                table: "PropertyDetail",
                column: "PropertyID",
                principalTable: "Property",
                principalColumn: "PropertyID");

            migrationBuilder.AddForeignKey(
                name: "FK__Transacti__Contr__36B12243",
                table: "Transaction",
                column: "ContractID",
                principalTable: "Contract",
                principalColumn: "ContractID");
        }
    }
}
