using Microsoft.EntityFrameworkCore;
using APBD_Cwiczenia5.Models;

namespace APBD_Cwiczenia5.Database;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<PC> PCs => Set<PC>();
    public DbSet<Component> Components => Set<Component>();
    public DbSet<ComponentManufacturer> ComponentManufacturers => Set<ComponentManufacturer>();
    public DbSet<ComponentType> ComponentTypes => Set<ComponentType>();
    public DbSet<PCComponent> PCComponents => Set<PCComponent>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigurePc(modelBuilder);
        ConfigureComponentManufacturer(modelBuilder);
        ConfigureComponentType(modelBuilder);
        ConfigureComponent(modelBuilder);
        ConfigurePcComponent(modelBuilder);

        SeedData(modelBuilder);
    }

    private static void ConfigurePc(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PC>(entity =>
        {
            entity.ToTable("PCs");

            entity.HasKey(p => p.Id);

            entity.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(p => p.Weight)
                .IsRequired()
                .HasColumnType("float");

            entity.Property(p => p.Warranty)
                .IsRequired();

            entity.Property(p => p.CreatedAt)
                .IsRequired()
                .HasColumnType("datetime");

            entity.Property(p => p.Stock)
                .IsRequired();
        });
    }

    private static void ConfigureComponentManufacturer(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ComponentManufacturer>(entity =>
        {
            entity.ToTable("ComponentManufacturers");

            entity.HasKey(m => m.Id);

            entity.Property(m => m.Abbreviation)
                .IsRequired()
                .HasMaxLength(30);

            entity.Property(m => m.FullName)
                .IsRequired()
                .HasMaxLength(300);

            entity.Property(m => m.FoundationDate)
                .IsRequired()
                .HasColumnType("date");
        });
    }

    private static void ConfigureComponentType(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ComponentType>(entity =>
        {
            entity.ToTable("ComponentTypes");

            entity.HasKey(t => t.Id);

            entity.Property(t => t.Abbreviation)
                .IsRequired()
                .HasMaxLength(30);

            entity.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(150);
        });
    }

    private static void ConfigureComponent(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Component>(entity =>
        {
            entity.ToTable("Components");

            entity.HasKey(c => c.Code);

            entity.Property(c => c.Code)
                .HasColumnType("char(10)")
                .HasMaxLength(10)
                .ValueGeneratedNever();

            entity.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(300);

            entity.Property(c => c.Description)
                .IsRequired();

            entity.Property(c => c.ComponentManufacturersId)
                .IsRequired();

            entity.Property(c => c.ComponentTypesId)
                .IsRequired();

            entity.HasOne(c => c.Manufacturer)
                .WithMany(m => m.Components)
                .HasForeignKey(c => c.ComponentManufacturersId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(c => c.Type)
                .WithMany(t => t.Components)
                .HasForeignKey(c => c.ComponentTypesId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }

    private static void ConfigurePcComponent(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PCComponent>(entity =>
        {
            entity.ToTable("PCComponents");

            entity.HasKey(pc => new { pc.PCId, pc.ComponentCode });

            entity.Property(pc => pc.ComponentCode)
                .HasColumnType("char(10)")
                .HasMaxLength(10);

            entity.Property(pc => pc.Amount)
                .IsRequired();

            entity.HasOne(pc => pc.PC)
                .WithMany(p => p.PCComponents)
                .HasForeignKey(pc => pc.PCId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(pc => pc.Component)
                .WithMany(c => c.PCComponents)
                .HasForeignKey(pc => pc.ComponentCode)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }

    private static void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ComponentManufacturer>().HasData(
            new ComponentManufacturer
            {
                Id = 1,
                Abbreviation = "AMD",
                FullName = "Advanced Micro Devices",
                FoundationDate = new DateOnly(1969, 5, 1)
            },
            new ComponentManufacturer
            {
                Id = 2,
                Abbreviation = "NV",
                FullName = "NVIDIA Corporation",
                FoundationDate = new DateOnly(1993, 4, 5)
            },
            new ComponentManufacturer
            {
                Id = 3,
                Abbreviation = "COR",
                FullName = "Corsair Gaming Inc.",
                FoundationDate = new DateOnly(1994, 1, 1)
            });

        modelBuilder.Entity<ComponentType>().HasData(
            new ComponentType { Id = 1, Abbreviation = "CPU", Name = "Processor" },
            new ComponentType { Id = 2, Abbreviation = "GPU", Name = "Graphics Card" },
            new ComponentType { Id = 3, Abbreviation = "RAM", Name = "Memory" });

        modelBuilder.Entity<Component>().HasData(
            new Component
            {
                Code = "CPU0000001",
                Name = "Ryzen 7 7800X3D",
                Description = "8-core gaming processor",
                ComponentManufacturersId = 1,
                ComponentTypesId = 1
            },
            new Component
            {
                Code = "GPU0000001",
                Name = "RTX 4080 Super",
                Description = "High-end gaming graphics card",
                ComponentManufacturersId = 2,
                ComponentTypesId = 2
            },
            new Component
            {
                Code = "RAM0000001",
                Name = "Corsair Vengeance DDR5 16GB",
                Description = "DDR5 RAM module 16GB",
                ComponentManufacturersId = 3,
                ComponentTypesId = 3
            });

        modelBuilder.Entity<PC>().HasData(
            new PC
            {
                Id = 1,
                Name = "Gaming Beast X",
                Weight = 12.5,
                Warranty = 36,
                CreatedAt = new DateTime(2026, 5, 8, 9, 0, 0, DateTimeKind.Unspecified),
                Stock = 5
            },
            new PC
            {
                Id = 2,
                Name = "Office Mini Pro",
                Weight = 4.2,
                Warranty = 24,
                CreatedAt = new DateTime(2026, 4, 15, 13, 30, 0, DateTimeKind.Unspecified),
                Stock = 12
            },
            new PC
            {
                Id = 3,
                Name = "Workstation Pro",
                Weight = 9.8,
                Warranty = 48,
                CreatedAt = new DateTime(2026, 3, 20, 10, 15, 0, DateTimeKind.Unspecified),
                Stock = 3
            });

        modelBuilder.Entity<PCComponent>().HasData(
            // Gaming Beast X
            new PCComponent { PCId = 1, ComponentCode = "CPU0000001", Amount = 1 },
            new PCComponent { PCId = 1, ComponentCode = "GPU0000001", Amount = 1 },
            new PCComponent { PCId = 1, ComponentCode = "RAM0000001", Amount = 2 },
            // Office Mini Pro
            new PCComponent { PCId = 2, ComponentCode = "CPU0000001", Amount = 1 },
            new PCComponent { PCId = 2, ComponentCode = "RAM0000001", Amount = 1 },
            // Workstation Pro
            new PCComponent { PCId = 3, ComponentCode = "CPU0000001", Amount = 1 },
            new PCComponent { PCId = 3, ComponentCode = "GPU0000001", Amount = 1 },
            new PCComponent { PCId = 3, ComponentCode = "RAM0000001", Amount = 4 });
    }
}
