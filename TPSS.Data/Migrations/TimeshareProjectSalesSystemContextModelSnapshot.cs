﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TPSS.Data.Context;

#nullable disable

namespace TPSS.Data.Migrations
{
    [DbContext(typeof(TimeshareProjectSalesSystemContext))]
    partial class TimeshareProjectSalesSystemContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TPSS.Data.Models.Entities.Contract", b =>
                {
                    b.Property<string>("ContractId")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasColumnName("ContractID");

                    b.Property<DateOnly?>("ContractDate")
                        .HasColumnType("date");

                    b.Property<string>("ContractStatus")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ContractTerms")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<double?>("Deposit")
                        .HasColumnType("float");

                    b.Property<bool?>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<string>("ReservationId")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasColumnName("ReservationID");

                    b.HasKey("ContractId")
                        .HasName("PK__Contract__C90D34099AA800A1");

                    b.HasIndex(new[] { "ReservationId" }, "IX_Contract_ReservationID");

                    b.ToTable("Contract", (string)null);
                });

            modelBuilder.Entity("TPSS.Data.Models.Entities.LikeList", b =>
                {
                    b.Property<int>("LikeId")
                        .HasColumnType("int")
                        .HasColumnName("LikeID");

                    b.Property<string>("PropertyId")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasColumnName("PropertyID");

                    b.Property<string>("UserId")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasColumnName("UserID");

                    b.HasKey("LikeId")
                        .HasName("PK__LikeList__A2922CF437D78B83");

                    b.HasIndex(new[] { "PropertyId" }, "IX_LikeList_PropertyID");

                    b.HasIndex(new[] { "UserId", "PropertyId" }, "UQ_UserProperty")
                        .IsUnique()
                        .HasFilter("([UserID] IS NOT NULL AND [PropertyID] IS NOT NULL)");

                    b.ToTable("LikeList", (string)null);
                });

            modelBuilder.Entity("TPSS.Data.Models.Entities.Payment", b =>
                {
                    b.Property<double?>("Amount")
                        .HasColumnType("float");

                    b.Property<bool?>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<string>("PaymentId")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasColumnName("PaymentID");

                    b.Property<bool?>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("TransactionId")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasColumnName("TransactionID");

                    b.HasIndex("TransactionId");

                    b.ToTable("Payment", (string)null);
                });

            modelBuilder.Entity("TPSS.Data.Models.Entities.Project", b =>
                {
                    b.Property<string>("ProjectId")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasColumnName("ProjectID");

                    b.Property<bool?>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<string>("ProjectName")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Status")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ProjectId")
                        .HasName("PK__Project__761ABED0232A2A0E");

                    b.ToTable("Project", (string)null);
                });

            modelBuilder.Entity("TPSS.Data.Models.Entities.ProjectDetail", b =>
                {
                    b.Property<string>("ProjectDetailId")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasColumnName("ProjectDetailID");

                    b.Property<DateOnly?>("CreateBy")
                        .HasColumnType("date");

                    b.Property<DateOnly?>("CreateDate")
                        .HasColumnType("date");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProjectDescription")
                        .HasColumnType("text");

                    b.Property<string>("ProjectId")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasColumnName("ProjectID");

                    b.Property<string>("ProjectName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly?>("UpdateBy")
                        .HasColumnType("date");

                    b.Property<DateOnly?>("UpdateDate")
                        .HasColumnType("date");

                    b.Property<bool?>("Verify")
                        .HasColumnType("bit");

                    b.HasKey("ProjectDetailId");

                    b.HasIndex("ProjectId");

                    b.ToTable("ProjectDetail", (string)null);
                });

            modelBuilder.Entity("TPSS.Data.Models.Entities.Property", b =>
                {
                    b.Property<string>("PropertyId")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasColumnName("PropertyID");

                    b.Property<double?>("Area")
                        .HasColumnType("float");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("District")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<double?>("Price")
                        .HasColumnType("float");

                    b.Property<string>("ProjectId")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasColumnName("ProjectID");

                    b.Property<string>("PropertyTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Province")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ward")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PropertyId")
                        .HasName("PK__Property__70C9A755F657A2CD");

                    b.HasIndex(new[] { "ProjectId" }, "IX_Property_ProjectID");

                    b.ToTable("Property", (string)null);
                });

            modelBuilder.Entity("TPSS.Data.Models.Entities.PropertyDetail", b =>
                {
                    b.Property<string>("PropertyDetailId")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasColumnName("PropertyDetailID");

                    b.Property<string>("CreateBy")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<DateOnly?>("CreateDate")
                        .HasColumnType("date");

                    b.Property<string>("Description")
                        .HasColumnType("ntext");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OwnerId")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasColumnName("OwnerID");

                    b.Property<string>("PropertyId")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasColumnName("PropertyID");

                    b.Property<string>("PropertyTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Service")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UpdateBy")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<DateOnly?>("UpdateDate")
                        .HasColumnType("date");

                    b.Property<string>("VerifyBy")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<DateOnly?>("VerifyDate")
                        .HasColumnType("date");

                    b.HasKey("PropertyDetailId");

                    b.HasIndex("PropertyId");

                    b.ToTable("PropertyDetail", (string)null);
                });

            modelBuilder.Entity("TPSS.Data.Models.Entities.Reservation", b =>
                {
                    b.Property<string>("ReservationId")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasColumnName("ReservationID");

                    b.Property<DateOnly?>("BookingDate")
                        .HasColumnType("date");

                    b.Property<string>("BuyerId")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasColumnName("BuyerID");

                    b.Property<bool?>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<int?>("Priority")
                        .HasColumnType("int");

                    b.Property<string>("PropertyId")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasColumnName("PropertyID");

                    b.Property<string>("SellerId")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasColumnName("SellerID");

                    b.Property<string>("Status")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ReservationId")
                        .HasName("PK__Reservat__B7EE5F04AD628F07");

                    b.HasIndex(new[] { "BuyerId" }, "IX_Reservation_BuyerID");

                    b.HasIndex(new[] { "PropertyId" }, "IX_Reservation_PropertyID");

                    b.HasIndex(new[] { "SellerId" }, "IX_Reservation_SellerID");

                    b.ToTable("Reservation", (string)null);
                });

            modelBuilder.Entity("TPSS.Data.Models.Entities.Role", b =>
                {
                    b.Property<string>("RoleId")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasColumnName("RoleID");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.ToTable("Role", (string)null);
                });

            modelBuilder.Entity("TPSS.Data.Models.Entities.Transaction", b =>
                {
                    b.Property<string>("TransactionId")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasColumnName("TransactionID");

                    b.Property<double?>("CommissionCalculation")
                        .HasColumnType("float")
                        .HasColumnName("Commission_Calculation");

                    b.Property<string>("ContractId")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasColumnName("ContractID");

                    b.Property<bool?>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<string>("Status")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("TransactionId")
                        .HasName("PK__Transact__1B39A9762989D08C");

                    b.HasIndex(new[] { "ContractId" }, "IX_Transaction_Processing_ContractID");

                    b.ToTable("Transaction", (string)null);
                });

            modelBuilder.Entity("TPSS.Data.Models.Entities.User", b =>
                {
                    b.Property<string>("UserId")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasColumnName("UserID");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit")
                        .HasColumnName("isActive");

                    b.Property<bool?>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasColumnName("RoleID");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId")
                        .HasName("PK__User__1788CCAC2A7756CD");

                    b.HasIndex("RoleId");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("TPSS.Data.Models.Entities.UserDetail", b =>
                {
                    b.Property<string>("UserDetailId")
                        .HasMaxLength(10)
                        .HasColumnType("nchar(10)")
                        .HasColumnName("UserDetailID")
                        .IsFixedLength();

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Avatar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreateBy")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateOnly?>("CreateDate")
                        .HasColumnType("date");

                    b.Property<DateOnly?>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PersonalId")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("PersonalID");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TaxIdentificationNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UpdateBy")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateOnly?>("UpdateDate")
                        .HasColumnType("date");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasColumnName("UserID");

                    b.HasKey("UserDetailId")
                        .HasName("PK__UserDeta__1788CCAC0F790C30");

                    b.HasIndex("UserId");

                    b.ToTable("UserDetail", (string)null);
                });

            modelBuilder.Entity("TPSS.Data.Models.Entities.Contract", b =>
                {
                    b.HasOne("TPSS.Data.Models.Entities.Reservation", "Reservation")
                        .WithMany("Contracts")
                        .HasForeignKey("ReservationId")
                        .HasConstraintName("FK__Contract__Reserv__33D4B598");

                    b.Navigation("Reservation");
                });

            modelBuilder.Entity("TPSS.Data.Models.Entities.LikeList", b =>
                {
                    b.HasOne("TPSS.Data.Models.Entities.Property", "Property")
                        .WithMany("LikeLists")
                        .HasForeignKey("PropertyId")
                        .HasConstraintName("FK__LikeList__Proper__3B75D760");

                    b.HasOne("TPSS.Data.Models.Entities.User", "User")
                        .WithMany("LikeLists")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_LikeList_User");

                    b.Navigation("Property");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TPSS.Data.Models.Entities.Payment", b =>
                {
                    b.HasOne("TPSS.Data.Models.Entities.Transaction", "Transaction")
                        .WithMany()
                        .HasForeignKey("TransactionId")
                        .IsRequired()
                        .HasConstraintName("FK_Payment_Transaction");

                    b.Navigation("Transaction");
                });

            modelBuilder.Entity("TPSS.Data.Models.Entities.ProjectDetail", b =>
                {
                    b.HasOne("TPSS.Data.Models.Entities.Project", "Project")
                        .WithMany("ProjectDetails")
                        .HasForeignKey("ProjectId")
                        .IsRequired()
                        .HasConstraintName("FK_ProjectDetail_Project");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("TPSS.Data.Models.Entities.Property", b =>
                {
                    b.HasOne("TPSS.Data.Models.Entities.Project", "Project")
                        .WithMany("Properties")
                        .HasForeignKey("ProjectId")
                        .IsRequired()
                        .HasConstraintName("FK__Property__Projec__2B3F6F97");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("TPSS.Data.Models.Entities.PropertyDetail", b =>
                {
                    b.HasOne("TPSS.Data.Models.Entities.Property", "Property")
                        .WithMany("PropertyDetails")
                        .HasForeignKey("PropertyId")
                        .HasConstraintName("FK_PropertyDetail_Property");

                    b.Navigation("Property");
                });

            modelBuilder.Entity("TPSS.Data.Models.Entities.Reservation", b =>
                {
                    b.HasOne("TPSS.Data.Models.Entities.Property", "Property")
                        .WithMany("Reservations")
                        .HasForeignKey("PropertyId")
                        .HasConstraintName("FK__Reservati__Prope__2F10007B");

                    b.Navigation("Property");
                });

            modelBuilder.Entity("TPSS.Data.Models.Entities.Transaction", b =>
                {
                    b.HasOne("TPSS.Data.Models.Entities.Contract", "Contract")
                        .WithMany("Transactions")
                        .HasForeignKey("ContractId")
                        .HasConstraintName("FK__Transacti__Contr__36B12243");

                    b.Navigation("Contract");
                });

            modelBuilder.Entity("TPSS.Data.Models.Entities.User", b =>
                {
                    b.HasOne("TPSS.Data.Models.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .IsRequired()
                        .HasConstraintName("FK_User_Role");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("TPSS.Data.Models.Entities.UserDetail", b =>
                {
                    b.HasOne("TPSS.Data.Models.Entities.User", "User")
                        .WithMany("UserDetails")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_UserDetail_User");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TPSS.Data.Models.Entities.Contract", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("TPSS.Data.Models.Entities.Project", b =>
                {
                    b.Navigation("ProjectDetails");

                    b.Navigation("Properties");
                });

            modelBuilder.Entity("TPSS.Data.Models.Entities.Property", b =>
                {
                    b.Navigation("LikeLists");

                    b.Navigation("PropertyDetails");

                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("TPSS.Data.Models.Entities.Reservation", b =>
                {
                    b.Navigation("Contracts");
                });

            modelBuilder.Entity("TPSS.Data.Models.Entities.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("TPSS.Data.Models.Entities.User", b =>
                {
                    b.Navigation("LikeLists");

                    b.Navigation("UserDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
