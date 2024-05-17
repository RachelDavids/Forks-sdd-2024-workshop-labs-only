using HouseControl.Sunset;

namespace HouseControl.Library.Test;

[TestFixture]
public class ScheduleTests
{
    private readonly string fileName = AppDomain.CurrentDomain.BaseDirectory + "\\ScheduleData";

    private sealed class FakeSunsetProvider
        : ISolarServiceSunsetProvider
    {
        public DateTimeOffset GetSunrise(DateTime date)
        {
            DateTime time = DateTime.Parse("06:15:00");
            return new DateTimeOffset(date.Date + time.TimeOfDay);
        }

        public DateTimeOffset GetSunset(DateTime date)
        {
            DateTime time = DateTime.Parse("20:25:00");
            return new DateTimeOffset(date.Date + time.TimeOfDay);
        }
    }

    [Test]
    public void ScheduleItems_OnCreation_IsPopulated()
    {
        // Arrange / Act
        FakeSunsetProvider ssProvider = new();
        Schedule schedule = new(fileName, ssProvider, new LocalTimeProvider());
        // Assert
        Assert.That(schedule.Count, Is.GreaterThan(0));
    }

    [Test]
    public void ScheduleItems_OnCreation_AreInFuture() =>
        // Arrange / Act

        // Assert
        Assert.Inconclusive();

    [Test]
    public void ScheduleItems_AfterRoll_AreInFuture() =>
        // Arrange

        // Act

        // Assert
        Assert.Inconclusive();

    [Test]
    public void OneTimeItemInPast_AfterRoll_IsRemoved() =>
        // Arrange

        // Act

        // Assert
        Assert.Inconclusive();

    [Test]
    public void OneTimeItemInFuture_AfterRoll_IsStillThere() =>
        // Arrange

        // Act

        // Assert
        Assert.Inconclusive();
}
