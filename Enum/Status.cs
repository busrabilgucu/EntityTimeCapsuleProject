namespace ZamanKapsulu.Enum
{
    // Status enum'u bir entity'nin hangi durumda oldugunu belirtmek icin kullanilir
    public enum Status
    {
        // Yeni olusturuldu
        Created = 1,

        // Guncellendi
        Updated = 2,

        // Silindi (Soft delete icin)
        Deleted = 3
    }
}
