using Innovative.Geometry;
using Innovative.SolarCalculator;

namespace HouseControl.Sunset;

public class CachedLocalSunsetProvider(double latitude, double longitude) :
    ISolarServiceSunsetProvider
{
    private readonly Angle latitude = latitude;
    private readonly Angle longitude = longitude;

    private Dictionary<DateTime, SolarTimes> _cache = [];
    public DateTimeOffset GetSunrise(DateTime date)
    {
        SolarTimes solarTimes = CheckCache(date);
        DateTime sunrise = solarTimes.Sunrise.ToLocalTime();
        return new(sunrise);
    }

    private SolarTimes CheckCache(DateTime date)
    {
        if (!_cache.TryGetValue(date, out SolarTimes? solarTimes))
        {
            solarTimes = new(date, latitude, longitude);
            _cache.Add(date, solarTimes);
        }

        return solarTimes;
    }

    public DateTimeOffset GetSunset(DateTime date)
    {
        SolarTimes solarTimes = CheckCache(date);
        DateTime sunset = solarTimes.Sunset.ToLocalTime();
        return new(sunset);
    }
}