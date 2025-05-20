using System.Collections.Generic;
using System.Net.Mime;
using ZamanKapsulu.Models;
using ZamanKapsulu.Repositories;

namespace ZamanKapsulu.Services
{
    
    public class ContentService : BaseService<Content>
    {
        private readonly IRepository<Content> _contentRepository;

       
        public ContentService(IRepository<Content> contentRepository) : base(contentRepository)
        {
            _contentRepository = contentRepository;
        }

        // Icerik turune göre içerikleri listeleme
        public List<Content> GetContentsByType(ContentType contentType)
        {
            return _contentRepository.GetAll()
                                     .Where(c => c.ContentType == contentType)
                                     .ToList();
        }

    }
}
