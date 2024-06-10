using Microsoft.EntityFrameworkCore;
using MyApp.Models;

namespace MyApp.Data
{
    public class MyAppContext : DbContext
    {
        public MyAppContext(DbContextOptions<MyAppContext> options) : base(options)
        {
        }

        public DbSet<Abonnement> Abonnements { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientListe> ClientListes { get; set; }

        public DbSet<Chaine> Chaines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ClientListe>().HasNoKey();
        base.OnModelCreating(modelBuilder);
    }
    }
}
