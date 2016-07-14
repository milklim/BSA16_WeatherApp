using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Metadata.Edm;

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