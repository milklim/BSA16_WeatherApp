using System.Data.Entity;
using weatherForecastApp.Models;

namespace weatherForecastApp.Infrastructure
{
    public class UserContext : DbContext
    {
        public UserContext() : base("WeatherAppDB")
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<History> History { get; set; }
    }
}