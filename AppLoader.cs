using AkariLevelEditor.Manager;
using AkariLevelEditor.Utils;
using Wpf.Ui.Appearance;

namespace AkariLevelEditor;

public class AppLoader
{
    /** 启动 AkariLevelEditor 服务 **/
    public static void Startup()
    {
        FileUtils.SaveResource("Preferences.yml");

        switch (ConfigManager.Preferences.GetString("Theme", "Light"))
        {
            case "Light":
                ApplicationThemeManager.Apply(ApplicationTheme.Light);
                break;
            case "Dark":
                ApplicationThemeManager.Apply(ApplicationTheme.Dark);
                break;
        }
    }
}