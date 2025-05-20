# Dijital Zaman Kapsülü Console Uygulaması

Kullanıcıların gelecekte açılmak üzere dijital zaman kapsülleri oluşturabildiği, içerik ekleyebildiği ve zamanı geldiğinde bunları görüntüleyebildiği bir .NET Console uygulamasıdır.

# Özellikler

-  Kapsül oluşturma (açılma tarihi belirterek)
-  Kapsüllere farklı türde içerikler ekleme
-  Tüm kapsülleri listeleme (gelecekteki kapsüller şifreli görünür)
-  Açılma tarihi geçmiş kapsülleri tüm içeriğiyle görüntüleme
-  Kapsül detaylarını inceleme
-  Raporlar:
  - En fazla içeriğe sahip kapsüller
  - En çok kullanılan içerik türü

# Teknolojiler

- .NET 6/7 Console Application
- Entity Framework Core
- SQL Server (Code First)
- Repository Pattern
- Unit of Work Pattern
- Enum kullanımı (ContentType için)

  # Tasarım Yapısı
Repository Pattern: Her tablo için CRUD işlemleri soyutlandı.

Unit of Work: Tüm işlemler merkezi olarak yönetildi.

EF Core: Code First yaklaşımı ile veritabanı otomatik oluşturulur.

# Konsol Menüleri

1. Yeni kapsül oluştur
2. Kapsüle içerik ekle
3. Tüm kapsülleri listele
4. Açılma tarihi geçmiş kapsülleri görüntüle
5. Kapsül detaylarını listele
6. Rapor: En fazla içeriğe sahip kapsüller
7. Rapor: En çok kullanılan içerik türü
0. Çıkış

Bu proje, eğitim amaçlı geliştirilmiş olup Entity Framework, Repository Pattern ve Unit of Work yapılarının console ortamında uygulanmasını göstermektedir.
