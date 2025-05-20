using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ZamanKapsulu;
using ZamanKapsulu.Models;
using ZamanKapsulu.Repositories;
using ZamanKapsulu.Services;
using System;
using System.Linq;
using System.Net.Mime;

class Program
{
    static void Main(string[] args)
    {
        // SQL adresi için
        var options = new DbContextOptionsBuilder<AppDbContext>()
                        .UseSqlServer("Server=DESKTOP-IUQ7IVV\\MSSQLSERVER2022;Database=ZamanKapsuluDb;Trusted_Connection=True;")
                        .Options;

        // AppDbContext nesnesi oluşturuluyor
        using var context = new AppDbContext(options);

        // UnitOfWork nesnesi oluşturuluyor
        var unitOfWork = new UnitOfWork(context);

        // Kullanıcıya hoş geldiniz mesajı
        Console.WriteLine("Zaman Kapsülü Uygulamasına Hoş Geldiniz!");

        // Ana menüyü başlatıyoruz
        MainMenu(unitOfWork);
    }

    // Ana Menü metodu: Kullanıcıya menü seçenekleri sunuyor
    static void MainMenu(IUnitOfWork unitOfWork)
    {
        while (true)
        {
            // Menü seçenekleri ekrana yazdırılıyor
            Console.WriteLine("\nMenü:");
            Console.WriteLine("1. Yeni kapsül oluştur");
            Console.WriteLine("2. Kapsüle içerik ekle");
            Console.WriteLine("3. Tüm kapsülleri listele");
            Console.WriteLine("4. Açılma tarihi geçmiş kapsülleri görüntüle");
            Console.WriteLine("5. Kapsül Detayları (İçeriğiyle birlikte) listele");
            Console.WriteLine("6. Rapor: En fazla içeriğe sahip kapsüller");
            Console.WriteLine("7. Rapor: En çok kullanılan içerik türü");
            Console.WriteLine("8. Çıkış");

            // Kullanıcının seçim yapması bekleniyor
            var secim = Console.ReadLine();

            // Seçime göre işlemler yapılır
            switch (secim)
            {
                case "1":
                    CreateNewTimeCapsule(unitOfWork); // Yeni zaman kapsülü oluştur
                    break;
                case "2":
                    AddContentToCapsule(unitOfWork); // Kapsüle içerik ekle
                    break;
                case "3":
                    ListAllTimeCapsules(unitOfWork); // Tüm kapsülleri listele
                    break;
                case "4":
                    ListExpiredTimeCapsules(unitOfWork); // Açılma tarihi geçmiş kapsülleri listele
                    break;
                case "5":
                    ShowTimeCapsuleDetails(unitOfWork); // Kapsül detaylarını göster
                    break;
                case "6":
                    ReportMostContentTimeCapsules(unitOfWork); // En fazla içeriğe sahip kapsülleri raporla
                    break;
                case "7":
                    ReportMostUsedContentTypes(unitOfWork); // En çok kullanılan içerik türlerini raporla
                    break;
                case "8":
                    Console.WriteLine("Çıkılıyor..."); // Çıkış işlemi
                    return;
                default:
                    Console.WriteLine("Geçersiz seçenek, tekrar deneyin.");
                    break;
            }
        }
    }

    // Yeni bir zaman kapsülü oluşturma metodu
    static void CreateNewTimeCapsule(IUnitOfWork unitOfWork)
    {
        Console.WriteLine("\nYeni Zaman Kapsülü Oluşturuluyor...");

        // Kullanıcıdan başlık bilgisi alınır
        Console.Write("Başlık girin: ");
        var title = Console.ReadLine();

        // Kullanıcıdan açılma tarihi alınır
        Console.Write("Açılma tarihini (yyyy-MM-dd) formatında girin: ");
        var openDate = DateTime.Parse(Console.ReadLine());

        // Yeni bir TimeCapsule nesnesi oluşturuluyor
        var timeCapsule = new TimeCapsule
        {
            Title = title,
            OpenDate = openDate
        };

        // Zaman kapsülünü veritabanına ekliyoruz
        unitOfWork.TimeCapsuleRepository.Add(timeCapsule);
        unitOfWork.SaveChanges();
        Console.WriteLine("Zaman kapsülü oluşturuldu.");
    }

    // Zaman kapsülüne içerik ekleme metodu
    static void AddContentToCapsule(IUnitOfWork unitOfWork)
    {
        Console.WriteLine("\nKapsüle içerik ekleniyor...");

        // Kullanıcıdan kapsül ID'si alınır
        Console.Write("Kapsül ID'sini girin: ");
        var capsuleId = int.Parse(Console.ReadLine());

        // Kapsül veritabanından alınır
        var timeCapsule = unitOfWork.TimeCapsuleRepository.GetById(capsuleId);

        // Kapsül bulunamazsa hata mesajı gösterilir
        if (timeCapsule == null)
        {
            Console.WriteLine("Kapsül bulunamadı.");
            return;
        }

        // İçerik türü kullanıcıdan alınır
        Console.Write("İçerik türünü seçin (1-Resim, 2-Yazı, 3-Düşünce): ");
        var contentType = (ContentType)Enum.Parse(typeof(ContentType), Console.ReadLine(), true);

        // İçerik metni kullanıcıdan alınır
        Console.Write("İçerik metnini girin: ");
        var contentText = Console.ReadLine();

        // Yeni bir içerik nesnesi oluşturuluyor
        var content = new Content(contentText);
        content.SetContentType(contentType);  // İçerik türü set edilir
        content.TimeCapsuleId = timeCapsule.Id; // İçerik, ilgili kapsülle ilişkilendirilir

        // İçerik veritabanına eklenir ve değişiklikler kaydedilir
        unitOfWork.ContentRepository.Add(content);
        unitOfWork.SaveChanges();
        Console.WriteLine("İçerik eklendi.");
    }

    // Tüm kapsülleri listeleme metodu
    static void ListAllTimeCapsules(IUnitOfWork unitOfWork)
    {
        Console.WriteLine("\nTüm Zaman Kapsülleri:");

        // Veritabanındaki tüm zaman kapsülleri çekilir
        var timeCapsules = unitOfWork.TimeCapsuleRepository.GetAll();

        // Zaman kapsülleri ekrana yazdırılır
        foreach (var capsule in timeCapsules)
        {
            Console.WriteLine($"ID: {capsule.Id}, Başlık: {capsule.Title}, Açılma Tarihi: {capsule.OpenDate}");
        }
    }

    // Açılma tarihi geçmiş kapsülleri listeleme metodu
    static void ListExpiredTimeCapsules(IUnitOfWork unitOfWork)
    {
        Console.Write("Açılma tarihini (yyyy-MM-dd) formatında girin: ");
        DateTime openDate;
        bool isValidDate = DateTime.TryParse(Console.ReadLine(), out openDate);

        // Eğer tarih geçerli değilse, kullanıcıya hata mesajı gösterilir
        if (!isValidDate)
        {
            Console.WriteLine("Geçersiz tarih formatı");
            return;
        }

        // Açılma tarihi geçmiş kapsüller burada listeleme işlemi yapılacak (eksik kod)
    }

    // Kapsül Detayları listeleme metodu
    static void ShowTimeCapsuleDetails(IUnitOfWork unitOfWork)
    {
        Console.WriteLine("\nKapsül Detayları:");

        // Kullanıcıdan kapsül ID'si alınır
        Console.Write("Kapsül ID'sini girin: ");
        var capsuleId = int.Parse(Console.ReadLine());

        // Kapsül veritabanından alınır
        var timeCapsule = unitOfWork.TimeCapsuleRepository.GetById(capsuleId);

        // Kapsül bulunamazsa hata mesajı gösterilir
        if (timeCapsule == null)
        {
            Console.WriteLine("Kapsül bulunamadı.");
            return;
        }

        // Kapsülün başlık ve açılma tarihi ekrana yazdırılır
        Console.WriteLine($"Başlık: {timeCapsule.Title}, Açılma Tarihi: {timeCapsule.OpenDate}");

        // Kapsüle ait içerikler veritabanından alınır
        var contents = unitOfWork.ContentRepository.GetAll().Where(c => c.TimeCapsuleId == timeCapsule.Id).ToList();

        // İçerik varsa ekrana yazdırılır
        if (contents.Any())
        {
            Console.WriteLine("\nİçerikler:");
            foreach (var content in contents)
            {
                Console.WriteLine($"Tür: {content.ContentType}, İçerik: {content.Text}");
            }
        }
        else
        {
            Console.WriteLine("Kapsülde içerik yoktur.");
        }
    }

    // En fazla içeriğe sahip kapsülleri raporlama metodu
    static void ReportMostContentTimeCapsules(IUnitOfWork unitOfWork)
    {
        Console.WriteLine("\nEn Fazla İçeriğe Sahip Kapsüller:");

        // İçeriğe sahip kapsüller, içerik sayısına göre sıralanır ve ilk 5 kapsül seçilir
        var mostContentCapsules = unitOfWork.TimeCapsuleRepository.GetAll()
            .OrderByDescending(c => unitOfWork.ContentRepository.GetAll().Count(x => x.TimeCapsuleId == c.Id))
            .Take(5).ToList();

        // En fazla içeriğe sahip kapsüller ekrana yazdırılır
        foreach (var capsule in mostContentCapsules)
        {
            var contentCount = unitOfWork.ContentRepository.GetAll().Count(c => c.TimeCapsuleId == capsule.Id);
            Console.WriteLine($"Başlık: {capsule.Title}, İçerik Sayısı: {contentCount}");
        }
    }

    // En çok kullanılan içerik türünü raporlama metodu
    static void ReportMostUsedContentTypes(IUnitOfWork unitOfWork)
    {
        Console.WriteLine("\nEn Çok Kullanılan İçerik Türü:");

        // İçerik türlerinin sayıları gruplandırılır ve sıralanır
        var mostUsedContentTypes = unitOfWork.ContentRepository.GetAll()
            .GroupBy(c => c.ContentType)
            .OrderByDescending(g => g.Count())
            .FirstOrDefault();

        // En çok kullanılan içerik türü ekrana yazdırılır
        if (mostUsedContentTypes != null)
        {
            Console.WriteLine($"İçerik Türü: {mostUsedContentTypes.Key}, Kullanım Sayısı: {mostUsedContentTypes.Count()}");
        }
        else
        {
            Console.WriteLine("İçerik türü bulunamadı.");
        }
    }
}