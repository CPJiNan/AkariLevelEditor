using System.IO;
using YamlDotNet.Serialization;

namespace AkariLevelEditor.Configuration.File;

public class YamlConfiguration : Config
{
    public YamlConfiguration()
    {
    }

    public YamlConfiguration(IConfiguration defaults) : base(defaults)
    {
    }

    public static YamlConfiguration LoadConfiguration(FileInfo file)
    {
        if (file == null || !file.Exists)
            throw new FileNotFoundException("File does not exist.", file?.FullName);

        file.Refresh();

        var yamlContent = System.IO.File.ReadAllText(file.FullName);

        if (string.IsNullOrWhiteSpace(yamlContent)) yamlContent = "{}";

        var deserializer = new DeserializerBuilder()
            .Build();

        var deserializedData = deserializer.Deserialize<Dictionary<object, object>>(yamlContent);

        var config = new YamlConfiguration();

        SetConfiguration(config, deserializedData);

        return config;
    }

    public void SaveToFile(FileInfo file)
    {
        if (file == null)
            throw new ArgumentNullException(nameof(file));

        file.Refresh();

        var serializer = new SerializerBuilder()
            .Build();

        var yamlContent = serializer.Serialize(GetValues(true));

        System.IO.File.WriteAllText(file.FullName, yamlContent);
    }

    public void LoadFromString(string contents)
    {
        if (string.IsNullOrWhiteSpace(contents)) contents = "{}";

        var deserializer = new DeserializerBuilder()
            .Build();

        var deserializedData = deserializer.Deserialize<Dictionary<object, object>>(contents);

        SetConfiguration(deserializedData);
    }

    public string SaveToString()
    {
        var serializer = new SerializerBuilder()
            .Build();

        return serializer.Serialize(GetValues(true));
    }

    private static void SetConfiguration(IConfigurationSection config, Dictionary<object, object> data)
    {
        foreach (var kvp in data)
            if (kvp.Value is Dictionary<object, object> nestedDict)
                SetConfiguration(config.GetConfigurationSection(kvp.Key.ToString()), nestedDict);
            else
                config.Set(kvp.Key.ToString(), kvp.Value);
    }

    private void SetConfiguration(Dictionary<object, object> data)
    {
        foreach (var kvp in data)
            if (kvp.Value is Dictionary<object, object> nestedDict)
                SetConfiguration(GetConfigurationSection(kvp.Key.ToString()), nestedDict);
            else
                Set(kvp.Key.ToString(), kvp.Value);
    }
}