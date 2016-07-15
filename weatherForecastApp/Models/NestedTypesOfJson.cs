using System.Collections.Generic;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;


namespace weatherForecastApp.Models
{
    public class Weather
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class Main
    {
        public double temp { get; set; }
        public double pressure { get; set; }
        public int humidity { get; set; }
        public double temp_min { get; set; }
        public double temp_max { get; set; }
    }

    public class Wind
    {
        public double speed { get; set; }
        public double deg { get; set; }
    }

    public class Clouds
    {
        public int all { get; set; }
    }

    public class Sys
    {
        public int type { get; set; }
        public int id { get; set; }
        public double message { get; set; }
        public string country { get; set; }
        public int sunrise { get; set; }
        public int sunset { get; set; }
    }
    public class Coord
    {
        public double lon { get; set; }
        public double lat { get; set; }
    }

    public class City
    {
       [JsonProperty("id")]
       [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CityId { get; set; }
        public string name { get; set; }
        [NotMapped]
        public Coord coord { get; set; }
        public string country { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }

    public class Temp
    {
        public double day { get; set; }
        public double min { get; set; }
        public double max { get; set; }
    }

    public class Forecast
    {
        public int dt { get; set; }
        public Temp temp { get; set; }
        public double pressure { get; set; }
        public int humidity { get; set; }
        public List<Weather> weather { get; set; }
        public double speed { get; set; }
        public int deg { get; set; }
        public int clouds { get; set; }
    }

    public class Rain
    {
        [JsonProperty("3h")]
        public double Rain3Hours { get; set; }
    }
    public class Snow
    {
        [JsonProperty("3h")]
        public double Snow3Hours { get; set; }
    }

    public class Forecast3HoursDetailed
    {
        public int dt { get; set; }
        public Main main { get; set; }
        public List<Weather> weather { get; set; }
        public Clouds clouds { get; set; }
        public Wind wind { get; set; }
        public Rain rain { get; set; }
        public string dt_txt { get; set; }
    }


}