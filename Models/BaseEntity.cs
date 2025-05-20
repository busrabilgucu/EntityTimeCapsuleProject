using ZamanKapsulu.Enum;

public abstract class BaseEntity
{
   
    public int Id { get; set; }

    // kapsülün oluşturulma zamanı. Varsayılan olarak o anki zaman atanır.
    public DateTime CreatedTime { get; set; } = DateTime.Now;

    // Kapsülün güncellenme zamanı (null olabilir, çünkü güncellenmemiş olabilir)
    public DateTime? UpdatedTime { get; set; }

    // kapsülün durumunu belirtmek için
    public Status Status { get; set; }

    // Soft delete durumu
    public DateTime? DeletedTime { get; set; }
}
