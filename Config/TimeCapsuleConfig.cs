using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZamanKapsulu.Models;

namespace ZamanKapsulu.Config
{
    // TimeCapsule yapılandırması
    public class TimeCapsuleConfig : IEntityTypeConfiguration<TimeCapsule>
    {
        public void Configure(EntityTypeBuilder<TimeCapsule> builder)
        {
            //isim
            builder.ToTable("TimeCapsules");

            // Kapsül başlığının zorunlu olduğu ve en fazla 255 karakter alabilir.
            builder.Property(tc => tc.Title)
                   .IsRequired()                // Başlık zorunlu
                   .HasMaxLength(255);          // Max 255 karakter

            // Açıklama isteğe bağlı 
            builder.Property(tc => tc.Description)
                   .HasMaxLength(500);         // Maksimum 500 karakter

            // Açılma tarihi zorunludur
            builder.Property(tc => tc.OpenDate)
                   .IsRequired();             
            builder.Property(tc => tc.Status)
                   .IsRequired();              // Durum zorunlu

            // CreatedTime ve UpdatedTime için varsayılan değerler belirtiliyor
            builder.Property(tc => tc.CreatedTime)
                   .HasDefaultValueSql("GETDATE()");

            builder.Property(tc => tc.UpdatedTime)
                   .HasDefaultValueSql("GETDATE()");

            // TimeCapsule ile Content arasındaki ilişki
            // Bir TimeCapsule birden fazla Content içerebilir
            builder.HasMany(tc => tc.Contents)
                   .WithOne(c => c.TimeCapsule)  // Bir Content bir TimeCapsule'a ait olacak
                   .HasForeignKey(c => c.TimeCapsuleId)
                   .OnDelete(DeleteBehavior.Cascade); // TimeCapsule silindiğinde içerikler de silinsin

            // ToString metodu için yapılan yapılandırma
            builder.Ignore(tc => tc.ToString());
        }
    }
}
