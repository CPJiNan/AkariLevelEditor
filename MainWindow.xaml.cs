using AkariLevelEditor.Manager;

namespace AkariLevelEditor;

public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();

        AppLoader.Startup();
    }
}