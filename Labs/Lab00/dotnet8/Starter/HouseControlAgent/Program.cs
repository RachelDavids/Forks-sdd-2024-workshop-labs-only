using HouseControl.Library;
using HouseControl.Sunset;

namespace HouseControlAgent;

internal class Program
{
    private static async Task Main(string[] args)
    {
        Console.WriteLine("Initializing Controller");

        HouseController controller = InitializeHouseController();

        await Task.Delay(1); // placeholder to keep Main signature when test code is not used

        // For hardware/scheduling testing purposes
        // Uncomment this section to ensure that the hardware
        // and scheduling is working as expected.
        await controller.SendCommand(5, DeviceCommand.On);
        await controller.SendCommand(5, DeviceCommand.Off);

        DateTime currentTime = DateTime.Now;
        controller.ScheduleOneTimeItem(currentTime.AddMinutes(1), 3, DeviceCommand.On);
        controller.ScheduleOneTimeItem(currentTime.AddMinutes(2), 5, DeviceCommand.On);
        controller.ScheduleOneTimeItem(currentTime.AddMinutes(3), 3, DeviceCommand.Off);
        controller.ScheduleOneTimeItem(currentTime.AddMinutes(4), 5, DeviceCommand.Off);

        Console.WriteLine("Initialization Complete");

        string command = "";
        while (command != "q")
        {
            command = Console.ReadLine() ?? "";
            if (command == "s")
            {
                List<ScheduleItem> schedule = controller.GetCurrentScheduleItems();
                foreach (ScheduleItem item in schedule)
                {
                    Console.WriteLine($"{item.Info.EventTime:G} - {item.Info.TimeType} ({item.Info.RelativeOffset}), Device: {item.Device}, Command: {item.Command}");
                }
            }
            if (command == "r")
            {
                controller.ReloadSchedule();
            }
        }
    }

    private static HouseController InitializeHouseController()
    {
        //45.6382,-122.7013 = Vancouver, WA, USA
        //51.4768,-0.0030 = Royal Observatory, Greenwich
        //51.520,-0.0963 = Barbican Centre

        string fileName = AppDomain.CurrentDomain.BaseDirectory + "ScheduleData";
        SolarServiceSunsetProvider sunsetProvider = new(51.520, -0.0963);
        Schedule schedule = new(fileName, sunsetProvider);
        HouseController controller = new(schedule);

        DateTimeOffset sunset = sunsetProvider.GetSunset(DateTime.Today.AddDays(1));
        Console.WriteLine($"Sunset Tomorrow: {sunset.ToLocalTime():G}");

        return controller;
    }
}
