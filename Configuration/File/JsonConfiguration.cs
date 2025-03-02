using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AkariLevelEditor.Configuration.File;

public class JsonConfiguration : Config
{
    public JsonConfiguration()
    {
    }

    public JsonConfiguration(IConfiguration defaults) : base(defaults)
    {
    }

    public static JsonConfiguration LoadConfiguration(FileInfo file)
    {
        if (file == null || !file.Exists)
            throw new FileNotFoundException("File does not exist.", file?.FullName);

        file.Refresh();

        var jsonContent = System.IO.File.ReadAllText(file.FullName);

        if (string.IsNullOrWhiteSpace(jsonContent)) jsonContent = "{}";

        var deserializedData = JsonConvert.DeserializeObject<Dictionary<object, object>>(jsonContent);

        var config = new JsonConfiguration();

        SetConfiguration(config, deserializedData);

        return config;
    }

    public void SaveToFile(FileInfo file)
    {
        if (file == null)
            throw new ArgumentNullException(nameof(file));

        file.Refresh();

        var jsonContent = JsonConvert.SerializeObject(GetValues(true), Formatting.Indented);

        System.IO.File.WriteAllText(file.FullName, jsonContent);
    }

    public void LoadFromString(string contents)
    {
        if (string.IsNullOrWhiteSpace(contents)) contents = "{}";

        var deserializedData = JsonConvert.DeserializeObject<Dictionary<object, object>>(contents);

        SetConfiguration(deserializedData);
    }

    public string SaveToString()
    {
        return JsonConvert.SerializeObject(GetValues(true), Formatting.Indented);
    }

    private static void SetConfiguration(IConfigurationSection config, Dictionary<object, object> data)
    {
        foreach (var kvp in data)
            if (kvp.Value is JObject nestedObj)
            {
                var nestedDict = nestedObj.ToObject<Dictionary<object, object>>();
                SetConfiguration(config.GetConfigurationSection(kvp.Key.ToString()), nestedDict);
            }
            else
            {
                config.Set(kvp.Key.ToString(), kvp.Value);
            }
    }

    private void SetConfiguration(Dictionary<object, object> data)
    {
        foreach (var kvp in data)
            if (kvp.Value is JObject nestedObj)
            {
                var nestedDict = nestedObj.ToObject<Dictionary<object, object>>();
                SetConfiguration(GetConfigurationSection(kvp.Key.ToString()), nestedDict);
            }
            else
            {
                Set(kvp.Key.ToString(), kvp.Value);
            }
    }
}