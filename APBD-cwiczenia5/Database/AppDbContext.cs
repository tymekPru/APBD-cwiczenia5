using Microsoft.EntityFrameworkCore;

namespace APBD_Cwiczenia5.Database;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }
}
