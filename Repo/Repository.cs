using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ZamanKapsulu.Repositories
{
    // IRepository'yi implement eder ve BaseEntity tipindeki nesneler için CRUD işlemlerini yönetir
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly DbContext _context;
        protected readonly DbSet<T> _dbSet;

        // DbSet<T>
        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        // nesne ekler
        public void Add(T entity)
        {
            try
            {
                _dbSet.Add(entity); // Entity'yi ekle
                _context.SaveChanges(); // Değişiklikleri kaydet
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error while adding entity: {ex.Message}");
                throw; 
            }
        }


        // mevcut nesneyi günceller
        public void Update(T entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges(); // Değişiklikleri kaydet
        }

        //  nesne siler
        public void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                _context.SaveChanges(); // Değişiklikleri kaydet
            }
        }

        // Tüm nesneleri listeler
        public List<T> GetAll()
        {
            try
            {
                return _dbSet.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetAll: {ex.Message}");
                // Hata durumunda null döndürebilir
                return null;
            }
        }


        // ID'ye göre bir nesne getirir
        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }
    }
}
