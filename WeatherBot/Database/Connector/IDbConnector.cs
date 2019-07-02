using System.Data;

namespace WeatherBot.Database.Connector
{
    public interface IDbConnector
    {
        IDbConnection Connection();
    }
}