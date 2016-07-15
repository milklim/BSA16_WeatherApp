using System;
using System.ComponentModel.DataAnnotations.Schema;


namespace weatherForecastApp.Models
{
    public class History
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HistoryId { get; set; }
        public Guid UserId { get; set; }
        public int CityId { get; set; }
        public DateTime Date { get; set; }

        public User user { get; set; }
        public City city { get; set; }
    }
}