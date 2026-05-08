using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Claims;
using System.Text.Json;

namespace DinamikFırınSitesiUı.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMemoryCache _memoryCache;

        // Maksimum yanlış deneme hakkı
        private const int MaxFailedAttempts = 3;
        // Engel süresi (24 saat)
        private static readonly TimeSpan LockoutDuration = TimeSpan.FromHours(24);

        public LoginController(IHttpClientFactory httpClientFactory, IMemoryCache memoryCache)
        {
            _httpClientFactory = httpClientFactory;
            _memoryCache = memoryCache;
        }

        // GET: /Login/Index  → Giriş formunu göster
        [HttpGet]
        public IActionResult Index()
        {
            // Kullanıcı zaten giriş yapmışsa Admin'e yönlendir
            if (User.Identity != null && User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Admin");

            var ip = GetClientIp();
            var lockoutKey = $"lockout_{ip}";
            var attemptsKey = $"attempts_{ip}";

            // IP engelli mi?
            if (_memoryCache.TryGetValue(lockoutKey, out DateTime lockoutUntil))
            {
                if (DateTime.UtcNow < lockoutUntil)
                {
                    var remaining = lockoutUntil - DateTime.UtcNow;
                    ViewBag.LockoutMessage = $"Çok fazla başarısız giriş denemesi yaptınız. " +
                        $"Hesabınız {remaining.Hours} saat {remaining.Minutes} dakika {remaining.Seconds} saniye süreyle engellenmiştir.";
                    return View();
                }
                else
                {
                    // Engel süresi dolmuş, temizle
                    _memoryCache.Remove(lockoutKey);
                    _memoryCache.Remove(attemptsKey);
                }
            }

            return View();
        }

        // POST: /Login/Index  → Giriş doğrulama
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string username, string password)
        {
            var ip = GetClientIp();
            var lockoutKey = $"lockout_{ip}";
            var attemptsKey = $"attempts_{ip}";

            // IP engelli mi kontrol et
            if (_memoryCache.TryGetValue(lockoutKey, out DateTime lockoutUntil))
            {
                if (DateTime.UtcNow < lockoutUntil)
                {
                    var remaining = lockoutUntil - DateTime.UtcNow;
                    ViewBag.LockoutMessage = $"Çok fazla başarısız giriş denemesi yaptınız. " +
                        $"Hesabınız {remaining.Hours} saat {remaining.Minutes} dakika {remaining.Seconds} saniye süreyle engellenmiştir.";
                    return View();
                }
                else
                {
                    _memoryCache.Remove(lockoutKey);
                    _memoryCache.Remove(attemptsKey);
                }
            }

            // API'den kullanıcıları çek ve doğrula
            bool loginSuccess = false;
            try
            {
                var client = _httpClientFactory.CreateClient("FirinApi");
                var response = await client.GetAsync("/api/login");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var logins = JsonSerializer.Deserialize<List<LoginDto>>(json,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    loginSuccess = logins != null &&
                                   logins.Any(l => l.Username == username && l.Password == password);
                }
            }
            catch
            {
                ViewBag.ErrorMessage = "Sunucuya bağlanılamadı. Lütfen daha sonra tekrar deneyin.";
                return View();
            }

            if (loginSuccess)
            {
                // Başarılı giriş → deneme sayacını sıfırla
                _memoryCache.Remove(attemptsKey);
                _memoryCache.Remove(lockoutKey);

                // Cookie Authentication ile oturum aç
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, "Admin")
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
                    new AuthenticationProperties { IsPersistent = false });

                return RedirectToAction("Index", "Admin");
            }
            else
            {
                // Başarısız giriş → deneme sayacını artır
                int attempts = _memoryCache.TryGetValue(attemptsKey, out int currentAttempts) ? currentAttempts : 0;
                attempts++;

                int remaining = MaxFailedAttempts - attempts;

                if (attempts >= MaxFailedAttempts)
                {
                    // 24 saat engelle
                    var expiry = DateTime.UtcNow.Add(LockoutDuration);
                    _memoryCache.Set(lockoutKey, expiry, LockoutDuration);
                    _memoryCache.Remove(attemptsKey);

                    ViewBag.LockoutMessage = $"3 kez yanlış giriş yaptınız. " +
                        $"Bu cihaz 24 saat süreyle engellenmiştir.";
                }
                else
                {
                    _memoryCache.Set(attemptsKey, attempts, TimeSpan.FromHours(24));
                    ViewBag.ErrorMessage = $"Kullanıcı adı veya şifre hatalı! " +
                        $"Kalan deneme hakkınız: {remaining}";
                }

                return View();
            }
        }

        // GET: /Login/Logout → Çıkış yap
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Default");
        }

        // İstemci IP adresini al
        private string GetClientIp()
        {
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";
            // IPv6 loopback → IPv4'e çevir
            if (ip == "::1") ip = "127.0.0.1";
            return ip;
        }
    }

    // API yanıt DTO'su
    public class LoginDto
    {
        public int LoginId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
