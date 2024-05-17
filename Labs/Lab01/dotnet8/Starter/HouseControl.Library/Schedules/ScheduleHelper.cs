using HouseControl.Sunset;

namespace HouseControl.Library;

public class ScheduleHelper(ISolarServiceSunsetProvider sunsetProvider)
{
    public static DateTimeOffset Tomorrow()
    {
        return new DateTimeOffset(
            DateTimeOffset.Now.Date.AddDays(1),
            DateTimeOffset.Now.Offset);
    }

    public static bool IsInFuture(DateTimeOffset checkTime) => checkTime > DateTimeOffset.Now;

    public DateTimeOffset RollForwardToNextDay(ScheduleInfo info)
    {
        if (IsInFuture(info.EventTime))
        {
            return info.EventTime;
        }

        DateTimeOffset nextDay = Tomorrow();
        return info.TimeType switch
        {
            ScheduleTimeType.Standard => nextDay + info.EventTime.TimeOfDay + info.RelativeOffset,
            ScheduleTimeType.Sunset => sunsetProvider.GetSunset(nextDay.Date) + info.RelativeOffset,
            ScheduleTimeType.Sunrise => sunsetProvider.GetSunrise(nextDay.Date) + info.RelativeOffset,
            _ => info.EventTime
        };
    }

    public DateTimeOffset RollForwardToNextWeekdayDay(ScheduleInfo info)
    {
        if (IsInFuture(info.EventTime))
        {
            return info.EventTime;
        }

        DateTimeOffset nextDay = Tomorrow();
        while (nextDay.DayOfWeek is DayOfWeek.Saturday
            or DayOfWeek.Sunday)
        {
            nextDay = nextDay.AddDays(1);
        }

        return info.TimeType switch
        {
            ScheduleTimeType.Standard => nextDay + info.EventTime.TimeOfDay + info.RelativeOffset,
            ScheduleTimeType.Sunset => sunsetProvider.GetSunset(nextDay.Date) + info.RelativeOffset,
            ScheduleTimeType.Sunrise => sunsetProvider.GetSunrise(nextDay.Date) + info.RelativeOffset,
            _ => info.EventTime
        };
    }

    public DateTimeOffset RollForwardToNextWeekendDay(ScheduleInfo info)
    {
        if (IsInFuture(info.EventTime))
        {
            return info.EventTime;
        }

        DateTimeOffset nextDay = Tomorrow();
        while (nextDay.DayOfWeek is not DayOfWeek.Saturday
            and not DayOfWeek.Sunday)
        {
            nextDay = nextDay.AddDays(1);
        }
        return info.TimeType switch
        {
            ScheduleTimeType.Standard => nextDay + info.EventTime.TimeOfDay + info.RelativeOffset,
            ScheduleTimeType.Sunset => sunsetProvider.GetSunset(nextDay.Date) + info.RelativeOffset,
            ScheduleTimeType.Sunrise => sunsetProvider.GetSunrise(nextDay.Date) + info.RelativeOffset,
            _ => info.EventTime
        };
    }
}
