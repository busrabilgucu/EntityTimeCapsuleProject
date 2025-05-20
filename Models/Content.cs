using System.Net.Mime;
using ZamanKapsulu.Models; 

// bu class zaman kapsülüne eklenen içerikleri temsil eder.
public class Content : BaseEntity
{
    // İçeriğin türünü belirtir (örneğin: Resim, Yazı, Düşünce vb.)
    public ContentType ContentType { get; set; }

    // İçeriğin metnini saklar (örneğin: yazı içeriği, düşünce vs.)
    public string Text { get; private set; }

    // Hangi zaman kapsülüne ait olduğunu belirtir.
    public int TimeCapsuleId { get; set; }

    // Zaman kapsülü ile olan ilişkiyi temsil eder. 'virtual' anahtar kelimesi, Entity Framework'ün Lazy Loading (tembel yükleme) özelliğini kullanabilmesi için gereklidir.
    public virtual TimeCapsule TimeCapsule { get; set; }

    // Content sınıfının kurucusu. İçerik metni verildiğinde bir Content nesnesi oluşturulur.
    public Content(string text)
    {
        Text = text; 
    }

    // İçerik türünü belirlemek için kullanılan metot
    public void SetContentType(ContentType contentType)
    {
        ContentType = contentType; // İçeriğin türü atanır.
    }

    // Content nesnesini string olarak döndürür, bu sayede konsola yazdırıldığında ya da loglama yapıldığında kolayca anlaşılabilir.
    public override string ToString()
    {
        // İçeriğin türünü ve metnini gösteren formatlı bir string döndürür.
        return $"Type: {ContentType} | Text: {Text}";
    }
}
