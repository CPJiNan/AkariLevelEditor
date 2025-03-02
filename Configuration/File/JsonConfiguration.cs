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
        if (file is not { Exists: true })
            throw new FileNotFoundException("File does not exist.", file.FullName);

        file.Refresh();

        var jsonContent = System.IO.File.ReadAllText(file.FullName);

        if (string.IsNullOrWhiteSpace(jsonContent)) jsonContent = "{}";

        var deserializedData = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonContent);

        var config = new JsonConfiguration();

        SetConfiguration(config, deserializedData ?? new Dictionary<string, object>());

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

        var deserializedData = JsonConvert.DeserializeObject<Dictionary<string, object>>(contents);

        SetConfiguration(deserializedData ?? new Dictionary<string, object>());
    }

    public string SaveToString()
    {
        return JsonConvert.SerializeObject(GetValues(true), Formatting.Indented);
    }

    private static void SetConfiguration(IConfigurationSection? config, Dictionary<string, object> data)
    {
        foreach (var kvp in data)
            if (kvp.Value is JObject nestedObj)
            {
                var nestedDict = nestedObj.ToObject<Dictionary<string, object>>();
                SetConfiguration(config?.GetConfigurationSection(kvp.Key),
                    nestedDict ?? new Dictionary<string, object>());
            }
            else
            {
                config?.Set(kvp.Key, kvp.Value);
            }
    }

    private void SetConfiguration(Dictionary<string, object> data)
    {
        foreach (var kvp in data)
            if (kvp.Value is JObject nestedObj)
            {
                var nestedDict = nestedObj.ToObject<Dictionary<string, object>>();
                SetConfiguration(GetConfigurationSection(kvp.Key), nestedDict ?? new Dictionary<string, object>());
            }
            else
            {
                Set(kvp.Key, kvp.Value);
            }
    }
}