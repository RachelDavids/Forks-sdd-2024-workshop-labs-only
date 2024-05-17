namespace HouseControl.Sunset;

public interface ISolarServiceSunsetProvider
{
    DateTimeOffset GetSunrise(DateTime date);
    DateTimeOffset GetSunset(DateTime date);
}