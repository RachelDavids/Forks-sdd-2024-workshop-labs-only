namespace HouseControl.Library;

public interface ITimeProvider
{
    DateTimeOffset Tomorrow();
    DateTimeOffset Now { get; }
    bool IsInFuture(DateTimeOffset checkTime);
}

public class LocalTimeProvider : ITimeProvider
{
    public DateTimeOffset Tomorrow()
    {
        return new DateTimeOffset(
            Now.Date.AddDays(1),
            Now.Offset);
    }

    public DateTimeOffset Now => DateTimeOffset.Now;
    public bool IsInFuture(DateTimeOffset checkTime) => checkTime > Now;
}