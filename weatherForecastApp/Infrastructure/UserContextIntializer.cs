using System;
using System.Data.Entity;
using weatherForecastApp.Models;
using System.IO;
using Newtonsoft.Json;

namespace weatherForecastApp.Infrastructure
{
    public class UserContextIntializer : DropCreateDatabaseIfModelChanges<UserContext> 
    {
        protected override void Seed(UserContext context)
        {
            string path = string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "App_Data\\city.list.UA.json");

            if (File.Exists(path))
            {
                foreach (string line in File.ReadLines(path))
                {
                    context.Cities.Add(JsonConvert.DeserializeObject<City>(line));
                }

            }
            context.SaveChanges();
        }
    }
}