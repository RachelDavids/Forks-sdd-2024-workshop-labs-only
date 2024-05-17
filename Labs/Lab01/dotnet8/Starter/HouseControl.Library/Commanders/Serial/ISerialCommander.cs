namespace HouseControl.Library;

public interface ISerialCommander
{
    Task SendCommand(int deviceNumber, DeviceCommand command);
}

public class DoNothingSerialCommander
    : ISerialCommander
{
    public Task SendCommand(int deviceNumber, DeviceCommand command)
    {
        Console.WriteLine($"Send Command {command} to Device {deviceNumber}");
        return Task.CompletedTask;
    }
}