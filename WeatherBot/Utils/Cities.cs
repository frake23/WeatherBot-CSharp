using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;
using WeatherBot.Api.Geonames;
using WeatherBot.Database;
using WeatherBot.KeyboardMarkups;

namespace WeatherBot.Utils
{
    internal class Cities
    {
        internal Cities(string[] geonamesTokens, Db db)
        {
            _geonames = new Geonames(geonamesTokens);
            _db = db;
        }

        private readonly Geonames _geonames;
        private readonly Db _db;

        internal async Task<int> CityId(int geonameId, string lang)
        {
            var city = await _db.GetCityByGeonameId(geonameId, lang);
            if (city == null)
            {
                var geoname = await _geonames.CityForGeonameId(geonameId, lang);
                await _db.AddCity(geoname.GeonameId, geoname.Name, geoname.AdminName, geoname.CountryCode,
                    geoname.Latitude, geoname.Longitude, lang, geoname.Timezone);
                city = await _db.GetCityByGeonameId(geonameId, lang);
            }

            return city.Id;
        }

        internal async Task<int> NearbyGeonameId(float latitude, float longitude, string lang)
        {
            var nearbyPlacenames = await _geonames.NearbyPlacename(latitude, longitude, lang);
            var nearbyPlacename = nearbyPlacenames.Geonames[0];

            return nearbyPlacename.GeonameId;
        }

        internal async Task<InlineKeyboardMarkup> CitiesInlineKeyboard(string cityName, string country, string lang)
        {
            var jsonGeonames = await _geonames.SearchCity(cityName, country, lang);
            return InlineKeyboardMarkups.CitiesInlineMarkup(jsonGeonames);
        }
}
}