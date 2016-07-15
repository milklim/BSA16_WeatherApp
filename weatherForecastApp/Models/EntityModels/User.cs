using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace weatherForecastApp.Models
{
    public class User
    {
        public User()
        {
            FavorCities = new HashSet<City>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserId { get; set; }
        public virtual ICollection<City> FavorCities { get; set; }
    }
}