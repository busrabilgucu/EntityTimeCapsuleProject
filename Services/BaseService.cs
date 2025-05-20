using System.Collections.Generic;
using ZamanKapsulu.Models;
using ZamanKapsulu.Repositories;

namespace ZamanKapsulu.Services
{
   
    public class BaseService<T> : IBaseService<T> where T : BaseEntity
    {
        private readonly IRepository<T> _repository;

      
        public BaseService(IRepository<T> repository)
        {
            _repository = repository;
        }

        // Yeni bir entity ekler
        public void Add(T entity)
        {
            _repository.Add(entity);
        }

        // Entity'yi günceller
        public void Update(T entity)
        {
            _repository.Update(entity);
        }

        // Entity'yi siler
        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        // Tüm entity'leri döner
        public List<T> GetAll()
        {
            return _repository.GetAll();
        }

        // Id'ye göre entity döner
        public T GetById(int id)
        {
            return _repository.GetById(id);
        }
    }
}
