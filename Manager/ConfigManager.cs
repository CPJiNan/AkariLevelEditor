using System.IO;
using AkariLevelEditor.Configuration.File;
using AkariLevelEditor.Utils;

namespace AkariLevelEditor.Manager;

public class ConfigManager
{
    public static YamlConfiguration Preferences =
        YamlConfiguration.LoadConfiguration(PreferencesFile());

    // Preferences
    public static string Theme = Preferences.GetString("Theme", "Light")!;

    public static void Reload()
    {
        Preferences = YamlConfiguration.LoadConfiguration(PreferencesFile());
    }

    public static FileInfo PreferencesFile()
    {
        var file = FileUtils.GetFileOrCreate("Preferences.yml");
        file.Refresh();
        return file;
    }
}