using AkademiqDinamikFırınSitesiApi.Dal.Entitys;
using DinamikFırınSitesiAPI.Dal.Entitys;
using Microsoft.EntityFrameworkCore;

namespace DinamikFırınSitesi.Dal.Context
{
    public class FırınContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost; initial catalog=DbFırın;integrated Security = true; trust Server certificate=true");
        }
        public DbSet<About> Abouts { get; set; }
        public DbSet<AboutList> AboutLists { get; set; }
        public DbSet<Clıent> Clıents { get; set; }
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
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }
        public DbSet<NewsletterEmail> NewsletterEmails { get; set; }
    }
}
