using ZamanKapsulu.Enum;

namespace ZamanKapsulu.Models
{
    // dijital kapsül bilgilerini tutar
    public class TimeCapsule : BaseEntity
    {
       
        public string Title { get; set; }

       
        public string Description { get; set; } // Kapsül açıklaması

        // Kapsülün açılacağı tarih (gelecek bir tarih olmalı)
        private DateTime _openDate;
        public DateTime OpenDate
        {
            get { return _openDate; }
            set
            {
                if (value <= DateTime.Now)
                    throw new ArgumentException("Açılma tarihi geçmiş bir tarih olamaz.");
                _openDate = value;
            }
        }

        // Bir kapsüle ait birden fazla içerik olabilir
        // Navigation property tanımı (1 kapsül - n içerik)
        public virtual List<Content> Contents { get; set; } = new List<Content>();

        // ToString metodu override edilerek kapsül bilgisi okunabilir şekilde döndürülür
        public override string ToString()
        {
            return $"Title: {Title} | OpenDate: {OpenDate.ToShortDateString()} | Status: {Status}";
        }
    }
}
