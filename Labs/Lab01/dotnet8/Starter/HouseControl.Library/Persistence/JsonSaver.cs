using System.Text.Json;

namespace HouseControl.Library;

public class JsonSaver : IScheduleSaver
{
    public void SaveScheduleItems(string filename, IEnumerable<ScheduleItem> schedule)
    {
        filename += ".json";
        string output = JsonSerializer.Serialize(schedule, new JsonSerializerOptions() { WriteIndented = true });

        using StreamWriter writer = new(filename, false);
        writer.WriteLine(output);
    }
}
