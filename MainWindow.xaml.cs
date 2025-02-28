using System.Windows;

namespace AkariLevelEditor;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        Loaded += (_, _) =>
        {
            Wpf.Ui.Appearance.SystemThemeWatcher.Watch(
                this
            );
        };
    }
}