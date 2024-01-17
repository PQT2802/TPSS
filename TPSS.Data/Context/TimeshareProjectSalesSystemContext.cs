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

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<Property> Properties { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<TransactionProcessing> TransactionProcessings { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost,1433;Initial Catalog=Timeshare_Project_Sales_System;User ID=sa;Password=thang12345;Trusted_Connection=SSPI;Encrypt=false;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contract>(entity =>
        {
            entity.HasKey(e => e.ContractId).HasName("PK__Contract__C90D34099AA800A1");

            entity.ToTable("Contract");

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

            entity.HasIndex(e => new { e.UserId, e.PropertyId }, "UQ_UserProperty").IsUnique();

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
                .HasConstraintName("FK__LikeList__UserID__3A81B327");
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

        modelBuilder.Entity<Property>(entity =>
        {
            entity.HasKey(e => e.PropertyId).HasName("PK__Property__70C9A755F657A2CD");

            entity.ToTable("Property");

            entity.Property(e => e.PropertyId)
                .HasMaxLength(15)
                .HasColumnName("PropertyID");
            entity.Property(e => e.Owner).HasMaxLength(15);
            entity.Property(e => e.ProjectId)
                .HasMaxLength(15)
                .HasColumnName("ProjectID");
            entity.Property(e => e.PropertyName).HasMaxLength(200);

            entity.HasOne(d => d.OwnerNavigation).WithMany(p => p.Properties)
                .HasForeignKey(d => d.Owner)
                .HasConstraintName("FK__Property__Owner__2C3393D0");

            entity.HasOne(d => d.Project).WithMany(p => p.Properties)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK__Property__Projec__2B3F6F97");
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => e.ReservationId).HasName("PK__Reservat__B7EE5F04AD628F07");

            entity.ToTable("Reservation");

            entity.Property(e => e.ReservationId)
                .HasMaxLength(15)
                .HasColumnName("ReservationID");
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

            entity.HasOne(d => d.Buyer).WithMany(p => p.ReservationBuyers)
                .HasForeignKey(d => d.BuyerId)
                .HasConstraintName("FK__Reservati__Buyer__30F848ED");

            entity.HasOne(d => d.Property).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.PropertyId)
                .HasConstraintName("FK__Reservati__Prope__2F10007B");

            entity.HasOne(d => d.Seller).WithMany(p => p.ReservationSellers)
                .HasForeignKey(d => d.SellerId)
                .HasConstraintName("FK__Reservati__Selle__300424B4");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.Role1 }).HasName("PK__Role__FA2998BF699973A0");

            entity.ToTable("Role");

            entity.Property(e => e.UserId)
                .HasMaxLength(15)
                .HasColumnName("UserID");
            entity.Property(e => e.Role1)
                .HasMaxLength(15)
                .HasColumnName("Role");

            entity.HasOne(d => d.User).WithMany(p => p.Roles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Role__UserID__267ABA7A");
        });

        modelBuilder.Entity<TransactionProcessing>(entity =>
        {
            entity.HasKey(e => e.ProcessId).HasName("PK__Transact__1B39A9762989D08C");

            entity.ToTable("Transaction_Processing");

            entity.Property(e => e.ProcessId)
                .HasMaxLength(15)
                .HasColumnName("ProcessID");
            entity.Property(e => e.CommissionCalculation).HasColumnName("Commission_Calculation");
            entity.Property(e => e.ContractId)
                .HasMaxLength(15)
                .HasColumnName("ContractID");
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Contract).WithMany(p => p.TransactionProcessings)
                .HasForeignKey(d => d.ContractId)
                .HasConstraintName("FK__Transacti__Contr__36B12243");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__1788CCAC2A7756CD");

            entity.ToTable("User");

            entity.Property(e => e.UserId)
                .HasMaxLength(15)
                .HasColumnName("UserID");
            entity.Property(e => e.DigitalSignature)
                .HasMaxLength(30)
                .HasColumnName("Digital_signature");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(30);
            entity.Property(e => e.LastName).HasMaxLength(30);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
