using Microsoft.EntityFrameworkCore;

namespace ConsoleApp.Models
{
    public class appDbContext : DbContext
    {
        public DbSet<Automobilis> Automobilis { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(@"Server=178.128.202.96;Port=3306;User=TestUser;Password=TestasTesta5;Database=Testas;");
        }
    }
}