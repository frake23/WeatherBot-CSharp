using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherBot.Database.Models
{
    [Table("users")]
    public class User
    {
        [Column("id")]
        public int Id { get; set; }
        
        [Column("username")]
        public string Username { get; set; }
        
        [Column("lang")]
        public string Lang { get; set; }
        
        [Column("cityId")]
        public int? CityId { get; set; }
        
        [Column("distributionTime")]
        public int? DistributionTime { get; set; }
        
        [Column("geoState")]
        public int GeoState { get; set; }
    }
}