using ExcelFileBackgroundJob.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ExcelFileBackgroundJob.Infrastructure.DataAccess;

public class AppDBContext : DbContext
{
    private readonly IConfiguration _configuration;
    public AppDBContext(DbContextOptions<AppDBContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblCustomer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__tblCusto__A4AE64D8A9D3446A");

            entity.ToTable("tblCustomer");

            entity.Property(e => e.Country)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(12)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__tblLog__5E548648417EB3E5");

            entity.ToTable("tblLog");

            entity.Property(e => e.Application)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Description)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.ErrorDate).HasColumnType("date");
            entity.Property(e => e.Method)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Module)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var a = _configuration.GetConnectionString("DefaultConnection");
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(a);
        }
    }

    public virtual DbSet<TblCustomer> Customer { get; set; }
    public virtual DbSet<TblLog> ExcelLogs { get; set; }
}
