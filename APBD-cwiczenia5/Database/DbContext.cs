using Microsoft.EntityFrameworkCore;

namespace Cwiczenia5.Database;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }
}
