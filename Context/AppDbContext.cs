using System.Net.Mime;
using Microsoft.EntityFrameworkCore;
using ZamanKapsulu.Models;

public class AppDbContext : DbContext
{
    public DbSet<Content> Contents { get; set; }
    public DbSet<TimeCapsule> TimeCapsules { get; set; }

    // Constructor: Bağlantı dizesi, dışarıdan sağlanacak
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    // OnModelCreating: Veritabanı modelini oluştururken yapılacak konfigürasyonlar
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Content>()
            .Property(c => c.ContentType)
            .HasConversion(
                v => v.ToString(), // Enum'u string'e çeviriyoruz
                v => (ContentType)Enum.Parse(typeof(ContentType), v) // String'i enum'a çeviriyoruz
            );

       
        modelBuilder.Entity<Content>()
            .Property(c => c.Text)
            .IsRequired();  // Text alanı zorunlu

        modelBuilder.Entity<TimeCapsule>()
            .Property(t => t.Title)
            .IsRequired();  // Title alanı zorunlu
    }
    //adres
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-IUQ7IVV\\MSSQLSERVER2022;Database=ZamanKapsuluDb;Trusted_Connection=True;");
        }
    }
}
