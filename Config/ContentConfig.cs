using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZamanKapsulu.Enum;
using ZamanKapsulu.Models;

namespace LibraryProject.Config
{
    public class ContentConfig : IEntityTypeConfiguration<Content>
    {
        public void Configure(EntityTypeBuilder<Content> builder)
        {
            builder.ToTable("Contents");

            // ContentType zorunlu ve enum 
            builder.Property(c => c.ContentType)
                   .IsRequired(); 

            // Text zorunlu ve 1000 karakter
            builder.Property(c => c.Text)
                   .IsRequired()              // Bos olamaz
                   .HasMaxLength(1000);       // Maksimum 1000 karakter

            // ForeignKey ilişkisi: Content her TimeCapsule'a ait olmalı
            builder.HasOne(c => c.TimeCapsule)                // Her içerik bir kapsüle ait
                   .WithMany(tc => tc.Contents)               // Bir kapsül birden fazla içeriğe sahip olabilir
                   .HasForeignKey(c => c.TimeCapsuleId)      // Foreign key ile bağlantı
                   .OnDelete(DeleteBehavior.Cascade);         // Kapsül silindiğinde içerikler de silinsin

            // Varsayılan durum: Status -> Created
            builder.Property(c => c.Status)
                   .HasDefaultValue(Status.Created);  // Başlangıçta status "Created" olacak

            // Varsayılan CreatedTime -> DateTime.Now (yeni bir içerik eklendiğinde otomatik atanır)
            builder.Property(c => c.CreatedTime)
                   .HasDefaultValueSql("GETDATE()");
        }
    }
}
