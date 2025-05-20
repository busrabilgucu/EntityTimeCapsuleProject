using System.Collections.Generic;
using ZamanKapsulu.Models;

namespace ZamanKapsulu.Services
{
    // Temel servis interface'i
    public interface IBaseService<T> where T : BaseEntity
    {
        // Yeni bir entity oluşturur
        void Add(T entity);

        // Entity'yi günceller
        void Update(T entity);

        // Bir entity'yi siler
        void Delete(int id);

        // Tüm entity'leri listeler
        List<T> GetAll();

        // Id ile bir entity'yi getirir
        T GetById(int id);
    }
}
