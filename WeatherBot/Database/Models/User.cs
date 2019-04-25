using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherBot.Database.Models
{
    [Table("users")]
    public class User
    {
        [Column("id")]
        public long Id { get; set; }
        
        [Column("username")]
        public string Username { get; set; }
        
        [Column("lang")]
        public string Lang { get; set; }
        
        [Column("cityId")]
        public long? CityId { get; set; }
        
        [Column("distributionTime")]
        public long? DistributionTime { get; set; }
        
        [Column("geoState")]
        public int GeoState { get; set; }
    }
}