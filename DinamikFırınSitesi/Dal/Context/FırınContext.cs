using AkademiqDinamikFırınSitesiApi.Dal.Entitys;
using DinamikFırınSitesiAPI.Dal.Entitys;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;

namespace DinamikFırınSitesi.Dal.Context
{
    public class FırınContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = Env.GetString("CONNECTION_STRING");
            optionsBuilder.UseSqlServer(connectionString);
        }
        public DbSet<About> Abouts { get; set; }
        public DbSet<AboutList> AboutLists { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Communication> Communications { get; set; }
        public DbSet<Counter> Counters { get; set; }
        public DbSet<Galery> Galeries { get; set; }
        public DbSet<Mail> Mail { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Services> Services { get; set; }
        public DbSet<ServicesList> ServicesList { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet <Banner> Banners { get; set; }
        public DbSet<NewsletterEmail> NewsletterEmails { get; set; }
        public DbSet<login> logins { get; set; }
    }
}
