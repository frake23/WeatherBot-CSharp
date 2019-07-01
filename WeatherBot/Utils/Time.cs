using System;

namespace WeatherBot.Utils
{
    internal static class Time
    {
        internal static long Now()
        {
            var dateTime = DateTime.Now;
            var dateTimeOffset = new DateTimeOffset(dateTime);
            return dateTimeOffset.ToUnixTimeSeconds();
        }
    }
}