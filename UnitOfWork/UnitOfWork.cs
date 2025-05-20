using ZamanKapsulu;
using ZamanKapsulu.Models;
using ZamanKapsulu.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    // Repository'ler
    public IRepository<Content> ContentRepository { get; private set; }
    public IRepository<TimeCapsule> TimeCapsuleRepository { get; private set; }

    
    public UnitOfWork(AppDbContext context)
    {
        _context = context;

        // Repository'ler oluşturulması icin
        ContentRepository = new Repository<Content>(_context);
        TimeCapsuleRepository = new Repository<TimeCapsule>(_context);
    }

    // SaveChanges ile değişiklikleri kaydederiz
    public void SaveChanges()
    {
        _context.SaveChanges();
    }

    // Dispose pattern - Bellek temizliği için kullanılır.
    public void Dispose()
    {
        _context.Dispose();
    }
}
