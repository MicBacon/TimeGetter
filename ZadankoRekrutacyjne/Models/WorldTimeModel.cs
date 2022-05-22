using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ZadankoRekrutacyjne.Models;

public class WorldTimeModel
{
    public string JsonData { get; set; } = "";
    public string RequestedTimeZone { get; set; } = "";
    public bool IsInvalidInput { get; set; }
    public bool IsApiAvailable { get; set; } = true;
    private const int StartIndex = 11;
    private const int LengthOfFullTime = 8;

    public string GetTime()
    {
        using JsonReader reader = new JsonTextReader(new StringReader(JsonData));
        reader.DateParseHandling = DateParseHandling.None;
        var obj = JObject.Load(reader);
        var dateTime = (string)obj["datetime"]!;
        var time = dateTime.Substring(StartIndex, LengthOfFullTime);
        return time;
    }

    public List<string>? GetTimeZonesList()
    {
        var timeZones = JsonConvert.DeserializeObject<List<string>>(JsonData);
        return timeZones;
    }

    public static string GetApiNotAvailableErrorMessage()
    {
        return "World Time API is not available at the moment.";
    }
    
}