namespace HouseControl.Library.Test;

[TestFixture]
public class ScheduleHelperTests
{
    readonly string fileName = AppDomain.CurrentDomain.BaseDirectory + "\\ScheduleData";
    // Tests:
    //   RollForwardToNextDay (past and future values)
    //   RollForwardToNextWeekdayDay (past and future values)
    //   RollForwardToNextWeekendDate (past and future values)

    // Tests for DateTime Helper Methods
    //   Now
    //   Today
    //   DurationFromNow
    //   IsInPast
    //   IsInFuture

    private static ScheduleItem FakeScheduleItem()
    {
        return new ScheduleItem(
            1, // device number
            DeviceCommand.On, // command
            new ScheduleInfo()
            {
                EventTime = DateTimeOffset.Now.AddMinutes(-2), // time 2 minutes in the past
                Type = ScheduleType.Once, // schedule type "Once"
            },
            true, // enabled
            "" // schedule set
        );
    }
}
