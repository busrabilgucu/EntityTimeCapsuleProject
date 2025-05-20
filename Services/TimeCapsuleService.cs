using System;
using System.Collections.Generic;
using ZamanKapsulu.Models;
using ZamanKapsulu.Repositories;

namespace ZamanKapsulu.Services
{
   
    public class TimeCapsuleService : BaseService<TimeCapsule>
    {
        private readonly IRepository<TimeCapsule> _timeCapsuleRepository;

        
        public TimeCapsuleService(IRepository<TimeCapsule> timeCapsuleRepository) : base(timeCapsuleRepository)
        {
            _timeCapsuleRepository = timeCapsuleRepository;
        }

        // Açılma tarihi geçmiş kapsülleri listeleme
        public List<TimeCapsule> GetExpiredTimeCapsules()
        {
            var currentDate = DateTime.Now;
            return _timeCapsuleRepository.GetAll().FindAll(t => t.OpenDate < currentDate);
        }

        // Kapsül oluşturulma tarihi bugüne eşit olanları listeleme
        public List<TimeCapsule> GetTodayOpenedTimeCapsules()
        {
            var currentDate = DateTime.Now.Date;
            return _timeCapsuleRepository.GetAll().FindAll(t => t.OpenDate.Date == currentDate);
        }
    }
}
