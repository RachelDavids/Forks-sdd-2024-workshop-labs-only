using System.Text.Json;

namespace HouseControl.Library;

public class JsonLoader : IScheduleLoader
{
    public IEnumerable<ScheduleItem> LoadScheduleItems(string filename)
    {
        List<ScheduleItem>? output = new();
        filename += ".json";

        if (File.Exists(filename))
        {
            using StreamReader reader = new(filename);
            output = JsonSerializer.Deserialize<List<ScheduleItem>>(
                reader.ReadToEnd());
        }

        return output!;
    }
}
