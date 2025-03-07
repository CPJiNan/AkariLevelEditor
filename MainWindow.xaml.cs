namespace AkariLevelEditor;

public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();
        Loaded += (_, _) => RootNavigation.Navigate("主页");

        AppLoader.Startup();
    }
}