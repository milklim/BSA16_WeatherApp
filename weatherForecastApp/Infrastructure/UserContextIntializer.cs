using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using weatherForecastApp.Models;

namespace weatherForecastApp.Infrastructure
{
    public class UserContextIntializer : DropCreateDatabaseAlways<UserContext>
    {
        protected override void Seed(UserContext context)
        {
            City newCity = new Models.City
            {
                CityId = 702550,
                name = "Lviv",
                country = "UA"

            };
            User newUser = new User();
            newUser.FavorCities.Add(newCity);

            context.Cities.Add(newCity);
            context.Users.Add(newUser);
            context.SaveChanges();
        }
    }
}