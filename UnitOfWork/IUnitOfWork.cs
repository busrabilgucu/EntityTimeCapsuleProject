using ZamanKapsulu.Models;
using ZamanKapsulu.Repositories;

public interface IUnitOfWork : IDisposable
{
    // Content repository
    IRepository<Content> ContentRepository { get; }

    // TimeCapsule repository
    IRepository<TimeCapsule> TimeCapsuleRepository { get; }

    // Tüm değişiklikleri kaydet
    void SaveChanges();
}
