using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Web;
using weatherForecastApp.Models;

namespace weatherForecastApp.Infrastructure
{
    public class UserContext : DbContext
    {
        public UserContext() : base("WeatherAppDBBBB")
        {
        }


        public DbSet<User> Users { get; set; }
        public DbSet<City> Cities { get; set; }


    }
}