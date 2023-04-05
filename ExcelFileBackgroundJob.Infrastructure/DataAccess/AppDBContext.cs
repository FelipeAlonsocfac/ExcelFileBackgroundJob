using ExcelFileBackgroundJob.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExcelFileBackgroundJob.Infrastructure.DataAccess;

public class AppDBContext : DbContext
{
    public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public virtual DbSet<tblCustomer> Customer { get; set; }
    public virtual DbSet<tblLog> ExcelLogs { get; set; }
}
