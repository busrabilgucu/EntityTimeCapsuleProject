

using ZamanKapsulu.Enum;

namespace EntityQuizProje.Interface
{
    // IBaseEntity, BaseEntity'nin sahip olmasi gereken propları belirler.
    // classlar bunu implement eder
    public interface IBaseEntity
    {
        int Id { get; set; }

        // Kapsülün oluşturulma zamanı
        DateTime CreatedTime { get; set; }

        // Kapsülün güncellenme zamanı
        DateTime? UpdatedTime { get; set; }

        // Kapsülün durumu
        Status Status { get; set; }

        // Kapsülün silinme zamanı
        DateTime? DeletedTime { get; set; }
    }
}
