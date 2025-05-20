using System.Collections.Generic;

namespace ZamanKapsulu.Repositories
{
    // IRepository arayüzü, tüm repository'ler için temel CRUD işlemleri sağlar
    public interface IRepository<T> where T : BaseEntity
    {
        // Veritabanına yeni bir nesne ekler
        void Add(T entity);

        // Veritabanındaki mevcut nesneyi günceller
        void Update(T entity);

        // Veritabanından nesne siler
        void Delete(int id);

        // Tüm nesneleri listeler
        List<T> GetAll();

        // ID'ye göre bir nesne getirir
        T GetById(int id);
    }
}
