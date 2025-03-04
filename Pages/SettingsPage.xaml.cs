using System.Windows.Controls;
using AkariLevelEditor.Manager;
using Wpf.Ui.Appearance;

namespace AkariLevelEditor.Pages;

public partial class SettingsPage
{
    public SettingsPage()
    {
        InitializeComponent();

        ThemeComboBox.SelectedIndex = ConfigManager.Preferences.GetString("Theme", "Light") switch
        {
            "Light" => 0,
            "Dark" => 1,
            _ => ThemeComboBox.SelectedIndex
        };
    }

    private void ThemeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (ThemeComboBox.SelectedItem is not ComboBoxItem selectedComboBoxItem) return;
        switch (selectedComboBoxItem.Tag)
        {
            case "Light":
                ApplicationThemeManager.Apply(ApplicationTheme.Light);
                ConfigManager.Preferences.Set("Theme", "Light");
                ConfigManager.Preferences.SaveToFile(ConfigManager.PreferencesFile());
                ConfigManager.Reload();
                break;
            case "Dark":
                ApplicationThemeManager.Apply(ApplicationTheme.Dark);
                ConfigManager.Preferences.Set("Theme", "Dark");
                ConfigManager.Preferences.SaveToFile(ConfigManager.PreferencesFile());
                ConfigManager.Reload();
                break;
        }
    }
}