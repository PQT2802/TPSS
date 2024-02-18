using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TPSS.Data.Models.Entities;

namespace TPSS.Data.Context;

public partial class TimeshareProjectSalesSystemContext : DbContext
{
    public TimeshareProjectSalesSystemContext()
    {
    }

    public TimeshareProjectSalesSystemContext(DbContextOptions<TimeshareProjectSalesSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Contract> Contracts { get; set; }

    public virtual DbSet<LikeList> LikeLists { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<ProjectDetail> ProjectDetails { get; set; }

    public virtual DbSet<Property> Properties { get; set; }

    public virtual DbSet<PropertyDetail> PropertyDetails { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserDetail> UserDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost,1433;Initial Catalog=Timeshare_Project_Sales_System;User ID=sa;Password=thang12345;Trusted_Connection=SSPI;Encrypt=false;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contract>(entity =>
        {
            entity.HasKey(e => e.ContractId).HasName("PK__Contract__C90D34099AA800A1");

            entity.ToTable("Contract");

            entity.HasIndex(e => e.ReservationId, "IX_Contract_ReservationID");

            entity.Property(e => e.ContractId)
                .HasMaxLength(15)
                .HasColumnName("ContractID");
            entity.Property(e => e.ContractStatus).HasMaxLength(50);
            entity.Property(e => e.ContractTerms).HasMaxLength(200);
            entity.Property(e => e.ReservationId)
                .HasMaxLength(15)
                .HasColumnName("ReservationID");

            entity.HasOne(d => d.Reservation).WithMany(p => p.Contracts)
                .HasForeignKey(d => d.ReservationId)
                .HasConstraintName("FK__Contract__Reserv__33D4B598");
        });

        modelBuilder.Entity<LikeList>(entity =>
        {
            entity.HasKey(e => e.LikeId).HasName("PK__LikeList__A2922CF437D78B83");

            entity.ToTable("LikeList");

            entity.HasIndex(e => e.PropertyId, "IX_LikeList_PropertyID");

            entity.HasIndex(e => new { e.UserId, e.PropertyId }, "UQ_UserProperty")
                .IsUnique()
                .HasFilter("([UserID] IS NOT NULL AND [PropertyID] IS NOT NULL)");

            entity.Property(e => e.LikeId)
                .ValueGeneratedNever()
                .HasColumnName("LikeID");
            entity.Property(e => e.PropertyId)
                .HasMaxLength(15)
                .HasColumnName("PropertyID");
            entity.Property(e => e.UserId)
                .HasMaxLength(15)
                .HasColumnName("UserID");

            entity.HasOne(d => d.Property).WithMany(p => p.LikeLists)
                .HasForeignKey(d => d.PropertyId)
                .HasConstraintName("FK__LikeList__Proper__3B75D760");

            entity.HasOne(d => d.User).WithMany(p => p.LikeLists)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_LikeList_User");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Payment");

            entity.Property(e => e.PaymentId)
                .HasMaxLength(15)
                .HasColumnName("PaymentID");
            entity.Property(e => e.TransactionId)
                .HasMaxLength(15)
                .HasColumnName("TransactionID");

            entity.HasOne(d => d.Transaction).WithMany()
                .HasForeignKey(d => d.TransactionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Payment_Transaction");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.ProjectId).HasName("PK__Project__761ABED0232A2A0E");

            entity.ToTable("Project");

            entity.Property(e => e.ProjectId)
                .HasMaxLength(15)
                .HasColumnName("ProjectID");
            entity.Property(e => e.ProjectName).HasMaxLength(200);
            entity.Property(e => e.Status).HasMaxLength(50);
        });

        modelBuilder.Entity<ProjectDetail>(entity =>
        {
            entity.ToTable("ProjectDetail");

            entity.Property(e => e.ProjectDetailId)
                .HasMaxLength(15)
                .HasColumnName("ProjectDetailID");
            entity.Property(e => e.ProjectDescription).HasColumnType("text");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(15)
                .HasColumnName("ProjectID");

            entity.HasOne(d => d.Project).WithMany(p => p.ProjectDetails)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProjectDetail_Project");
        });

        modelBuilder.Entity<Property>(entity =>
        {
            entity.HasKey(e => e.PropertyId).HasName("PK__Property__70C9A755F657A2CD");

            entity.ToTable("Property");

            entity.HasIndex(e => e.ProjectId, "IX_Property_ProjectID");

            entity.Property(e => e.PropertyId)
                .HasMaxLength(15)
                .HasColumnName("PropertyID");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(15)
                .HasColumnName("ProjectID");

            entity.HasOne(d => d.Project).WithMany(p => p.Properties)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Property__Projec__2B3F6F97");
        });

        modelBuilder.Entity<PropertyDetail>(entity =>
        {
            entity.ToTable("PropertyDetail");

            entity.Property(e => e.PropertyDetailId)
                .HasMaxLength(15)
                .HasColumnName("PropertyDetailID");
            entity.Property(e => e.CreateBy).HasMaxLength(15);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Description).HasColumnType("ntext");
            entity.Property(e => e.OwnerId)
                .HasMaxLength(15)
                .HasColumnName("OwnerID");
            entity.Property(e => e.PropertyId)
                .HasMaxLength(15)
                .HasColumnName("PropertyID");
            entity.Property(e => e.UpdateBy).HasMaxLength(15);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.VerifyBy).HasMaxLength(15);
            entity.Property(e => e.VerifyDate).HasColumnType("datetime");

            entity.HasOne(d => d.Property).WithMany(p => p.PropertyDetails)
                .HasForeignKey(d => d.PropertyId)
                .HasConstraintName("FK_PropertyDetail_Property");
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => e.ReservationId).HasName("PK__Reservat__B7EE5F04AD628F07");

            entity.ToTable("Reservation");

            entity.HasIndex(e => e.BuyerId, "IX_Reservation_BuyerID");

            entity.HasIndex(e => e.PropertyId, "IX_Reservation_PropertyID");

            entity.HasIndex(e => e.SellerId, "IX_Reservation_SellerID");

            entity.Property(e => e.ReservationId)
                .HasMaxLength(15)
                .HasColumnName("ReservationID");
            entity.Property(e => e.BookingDate).HasColumnType("datetime");
            entity.Property(e => e.BuyerId)
                .HasMaxLength(15)
                .HasColumnName("BuyerID");
            entity.Property(e => e.PropertyId)
                .HasMaxLength(15)
                .HasColumnName("PropertyID");
            entity.Property(e => e.SellerId)
                .HasMaxLength(15)
                .HasColumnName("SellerID");
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Property).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.PropertyId)
                .HasConstraintName("FK__Reservati__Prope__2F10007B");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.Property(e => e.RoleId).HasMaxLength(15);
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__Transact__1B39A9762989D08C");

            entity.ToTable("Transaction");

            entity.HasIndex(e => e.ContractId, "IX_Transaction_Processing_ContractID");

            entity.Property(e => e.TransactionId)
                .HasMaxLength(15)
                .HasColumnName("TransactionID");
            entity.Property(e => e.CommissionCalculation).HasColumnName("Commission_Calculation");
            entity.Property(e => e.ContractId)
                .HasMaxLength(15)
                .HasColumnName("ContractID");
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Contract).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.ContractId)
                .HasConstraintName("FK__Transacti__Contr__36B12243");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__1788CCAC2A7756CD");

            entity.ToTable("User");

            entity.Property(e => e.UserId).HasMaxLength(15);
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.RoleId).HasMaxLength(15);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Role");
        });

        modelBuilder.Entity<UserDetail>(entity =>
        {
            entity.HasKey(e => e.UserDetailId).HasName("PK__UserDeta__1788CCAC0F790C30");

            entity.ToTable("UserDetail");

            entity.Property(e => e.UserDetailId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("UserDetailID");
            entity.Property(e => e.CreateBy).HasMaxLength(20);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.PersonalId).HasColumnName("PersonalID");
            entity.Property(e => e.UpdateBy).HasMaxLength(20);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.UserId)
                .HasMaxLength(15)
                .HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.UserDetails)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserDetail_User");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
