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

    //public DbSet<Customer> Customer { get; set; }
    //public DbSet<ExcelLogs> ExcelLogs { get; set; }
}
