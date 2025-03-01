namespace AkariLevelEditor;

public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();

        Loaded += (_, _) =>
        {
            Wpf.Ui.Appearance.SystemThemeWatcher.Watch(
                this
            );
        };
    }
}