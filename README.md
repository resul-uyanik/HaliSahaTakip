# ⚽ Halı Saha Rezervasyon ve Takip Sistemi (Astroturf Tracking System)

Halı Saha Takip Sistemi, halı saha işletmelerinin rezervasyon süreçlerini, abone kayıtlarını, saha doluluk oranlarını ve finansal hareketlerini tek bir merkezden kolayca yönetebilmesi için C# (.NET) ile geliştirilmiş kurumsal bir otomasyon projesidir.

Proje; ilişkisel veritabanı yönetimi, katmanlı mimari prensipleri, nesne yönelimli programlama (OOP) ve dinamik raporlama tekniklerini pratik etmek amacıyla kurgulanmıştır.

---

## 🚀 Öne Çıkan Özellikler

* **📅 Dinamik Rezervasyon Modülü:** Sahaların gün ve saat bazında doluluk durumlarının takibi, çakışmaları önleyen akıllı takvim sistemi ve anlık randevu oluşturma.
* **👥 Abone ve Müşteri Yönetimi:** Düzenli olarak maç yapan abone takımların kayıtları, iletişim bilgileri ve geçmiş maç istatistiklerinin tutulması.
* **🏟️ Çoklu Saha Desteği:** İşletmeye ait birden fazla sahanın (Örn: Saha-1 Açık, Saha-2 Kapalı) özelliklerine, fiyat tarifelerine ve çalışma saatlerine göre ayrı ayrı yönetilmesi.
* **💰 Kasa ve Ödeme Takibi:** Alınan kaporalar, kalan ödemeler, abonelik ücretleri ve işletme giderlerinin kayıt altına alınarak günlük/aylık gelir-gider raporlarının çıkarılması.
* **🔔 Maç Hatırlatma Sistemi (Opsiyonel):** Yaklaşan rezervasyonlar için müşterilere ve saha görevlilerine yönelik durum bilgilendirmeleri.

---

## 🛠 Kullanılan Teknolojiler ve Mimari

* **Programlama Dili:** C# (.NET)
* **Geliştirme Ortamı:** Microsoft Visual Studio / JetBrains Rider
* **Veritabanı Yapısı:** MS SQL Server / SQLite (İlişkisel Veritabanı Yönetimi)
* **Tasarım Deseni:** Nesne Yönelimli Tasarım (OOP), Encapsulation, Inheritance ve Abstraction prensipleri.

---

## 📂 Proje Yapısı

```text
├── HalıSahaTakip.sln         # Ana Visual Studio Çözüm (Solution) dosyası
└── HalıSahaTakip/
    ├── HalıSahaTakip.csproj   # C# Proje konfigürasyon dosyası
    ├── Models/                # Saha, Musteri, Rezervasyon, Odeme sınıfları
    ├── Data/                  # Veritabanı bağlantı ve sorgu katmanı (Context / Repository)
    └── Program.cs             # Uygulamanın ana giriş noktası ve yönetim döngüsü
