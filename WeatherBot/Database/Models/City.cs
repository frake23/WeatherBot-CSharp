using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherBot.Database.Models
{
    [Table("cities")]
    public class City
    {
        [Column("id")]
        public int Id { get; set; }
        
        [Column("geonameId")]
        public int GeonameId { get; set; }

        [Column("name")]
        public string Name { get; set; }
        
        [Column("adminName")]
        public string AdminName { get; set; }

        [Column("countryCode")]
        public string CountryCode { get; set; }
        
        [Column("latitude")]
        public float Latitude { get; set; }
        
        [Column("longitude")]
        public float Longitude { get; set; }
        
        [Column("currentWeather")]
        public string CurrentWeather { get; set; }
        
        [Column("todayWeather")]
        public string TodayWeather { get; set; }
        
        [Column("tomorrowWeather")]
        public string TomorrowWeather { get; set; }
        
        [Column("dayAfterTomorrowWeather")]
        public string DayAfterTomorrowWeather { get; set; }
        
        [Column("currentWeatherUpdatedTime")]
        public long CurrentWeatherUpdatedTime { get; set; }
        
        [Column("forecastUpdatedTime")]
        public long ForecastUpdatedTime { get; set; }
        
        [Column("timezone")]
        public string Timezone { get; set; }
    }
}