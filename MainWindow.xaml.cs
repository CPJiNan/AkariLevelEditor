using Wpf.Ui.Controls;

namespace AkariLevelEditor;

public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();
        Loaded += (_, _) => RootNavigation.Navigate("主页");
        RootNavigation.Navigating += OnNavigatingFromHomePage;

        AppLoader.Startup();
    }

    public static bool IsHomePageModified = false;

    private async void OnNavigatingFromHomePage(object sender, NavigatingCancelEventArgs e)
    {
        if (RootNavigation.SelectedItem?.TargetPageTag != "主页" || !IsHomePageModified) return;

        var dialog = new MessageBox
        {
            Title = "确认离开",
            Content = "您已对当前页面进行了修改，是否仍要离开？",
            PrimaryButtonText = "离开",
            CloseButtonText = "取消"
        };

        var result = await dialog.ShowDialogAsync();

        switch (result)
        {
            case MessageBoxResult.Primary:
                break;
            case MessageBoxResult.None:
                e.Cancel = true;
                break;
        }
    }
}