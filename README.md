[README.md](https://github.com/user-attachments/files/27520916/README.md)
# 🥖 DinamikFırınSitesi

> ASP.NET Core tabanlı, tam yönetim panelli dinamik fırın web sitesi.

---

## 📌 Proje Hakkında

DinamikFırınSitesi, bir fırın işletmesi için geliştirilmiş **tam stack** web uygulamasıdır. **REST API** ve **MVC UI** katmanından oluşan iki projeli bir yapıya sahiptir. Admin paneli aracılığıyla sitedeki tüm içerikler dinamik olarak yönetilebilir.

---

## 🏗️ Proje Yapısı

```
DinamikFırınSitesi/
├── DinamikFırınSitesi/         # ASP.NET Core Web API (Backend)
│   ├── Controllers/            # API endpoint'leri (16 controller)
│   ├── Dal/
│   │   ├── Context/            # Entity Framework DbContext
│   │   └── Entitys/            # Veritabanı modelleri
│   ├── Migrations/             # EF Core migration dosyaları
│   └── Program.cs
│
└── DinamikFırınSitesiUı/       # ASP.NET Core MVC (Frontend)
    ├── Controllers/            # MVC controller'lar (20 controller)
    ├── Dtos/                   # Veri transfer objeleri (17 DTO grubu)
    ├── ViewComponent/          # Razor ViewComponent'ler
    ├── Views/                  # Razor view'lar
    │   ├── Admin/              # Yönetim paneli sayfaları
    │   └── Shared/Components/  # Yeniden kullanılabilir bileşenler
    └── Program.cs
```

---

## ✨ Özellikler

### 🌐 Kullanıcı Arayüzü
- **Ana Sayfa** – Banner, hakkımızda, ürünler, ekip, hizmetler bölümleri
- **Ürünler** – Fırın ürünleri listesi
- **Hizmetler** – Sunulan hizmetler ve detayları
- **Ekip** – Çalışan tanıtım kartları
- **İletişim** – İletişim formu ve konum bilgisi
- **Bülten Aboneliği** – E-posta abonelik sistemi
- **Galeri** – Görsel galeri bölümü

### 🔐 Admin Paneli
- **Güvenli Giriş** – Cookie tabanlı kimlik doğrulama
- **IP Tabanlı Kilitleme** – Başarısız giriş denemelerinde 24 saat IP engelleme (MemoryCache)
- **Tam CRUD Yönetimi:**
  - 📦 Ürün yönetimi
  - 🛎️ Hizmet yönetimi
  - 👥 Ekip üyesi yönetimi
  - 🖼️ Banner yönetimi
  - 🖼️ Galeri yönetimi
  - 👤 Hakkımızda içerik yönetimi
  - 🔢 Sayaç (Counter) yönetimi
  - 📱 Sosyal medya bağlantıları
  - 📧 İletişim ayarları
  - 💌 Mesaj yönetimi (Okundu/Okunmadı filtreleme)
  - 📬 Bülten aboneleri listesi
  - 🤝 Müşteri (Client) yönetimi

---

## 🛠️ Kullanılan Teknolojiler

| Katman | Teknoloji |
|---|---|
| **Backend API** | ASP.NET Core Web API |
| **Frontend** | ASP.NET Core MVC + Razor Views |
| **ORM** | Entity Framework Core |
| **Kimlik Doğrulama** | Cookie Authentication |
| **Cache** | IMemoryCache |
| **HTTP İstemci** | IHttpClientFactory |
| **JSON** | Newtonsoft.Json |
| **Veritabanı** | MS SQL Server |
| **UI Bileşenleri** | ViewComponents, Partial Views |

---

## 🚀 Kurulum ve Çalıştırma

### Gereksinimler
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/tr-tr/sql-server/sql-server-downloads) (LocalDB veya tam sürüm)
- Visual Studio 2022 veya VS Code

### 1. Repoyu Klonla

```bash
git clone https://github.com/kullanici-adin/DinamikFırınSitesi.git
cd DinamikFırınSitesi
```

### 2. API Projesini Yapılandır

`DinamikFırınSitesi/` klasörüne `.env` dosyası oluştur (`.env.example`'a bakarak):

```env
CONNECTION_STRING=Server=.;Database=FirinDb;Trusted_Connection=True;TrustServerCertificate=True;
```

Veya `appsettings.json` içindeki `ConnectionStrings` bölümünü kendi bağlantı bilgilerinle güncelle.

### 3. Veritabanını Oluştur

```bash
cd DinamikFırınSitesi
dotnet ef database update
```

### 4. API'yi Başlat

```bash
cd DinamikFırınSitesi
dotnet run
# API: https://localhost:7061
```

### 5. UI'yi Başlat

```bash
cd DinamikFırınSitesiUı
dotnet run
# Site: https://localhost:7xxx
```

---

## ⚙️ Yapılandırma

### API URL'si

UI projesi, API'ye `https://localhost:7061` üzerinden erişir. Port değiştiyse ilgili ViewComponent dosyalarındaki URL'leri güncellemeyi unutma.

### Admin Girişi

Admin paneline `/Login/Index` üzerinden erişilir. Varsayılan giriş bilgileri veritabanının `login` tablosundan çekilmektedir.

---

## 📁 API Endpoint'leri

| Controller | Endpoint |
|---|---|
| About | `/api/About` |
| AboutList | `/api/AboutList` |
| Banner | `/api/Banner` |
| Client | `/api/Client` |
| Communication | `/api/Communication` |
| Counter | `/api/Counter` |
| Galery | `/api/Galery` |
| Message | `/api/Message` |
| NewsletterEmail | `/api/NewsletterEmail` |
| Product | `/api/Product` |
| Services | `/api/Services` |
| ServicesList | `/api/ServicesList` |
| SocialMedia | `/api/SocialMedia` |
| Team | `/api/Team` |
| Login | `/api/login` |

---

## 🔒 Güvenlik

- Admin paneli **Cookie Authentication** ile korunmaktadır
- Yetkisiz erişimlerde `/Login/Index` sayfasına yönlendirilir
- Oturum süresi **8 saat**, sliding expiration aktif
- Başarısız giriş denemelerinde **IP bazlı 24 saat kilitleme** (MemoryCache ile)

---

## 📄 Lisans

Bu proje [MIT Lisansı](LICENSE) altında yayımlanmaktadır.
