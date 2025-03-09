using Wpf.Ui.Controls;

namespace AkariLevelEditor;

public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();
        Loaded += (_, _) => RootNavigation.Navigate("配置");
        RootNavigation.Navigating += OnNavigatingFromOptionsPage;
        RootNavigation.Navigating += OnNavigatingFromLevelGroupPage;

        AppLoader.Startup();
    }

    public static bool IsOptionsPageModified = false;
    public static bool IsLevelGroupPageModified = false;

    private async void OnNavigatingFromOptionsPage(object sender, NavigatingCancelEventArgs e)
    {
        if (RootNavigation.SelectedItem?.TargetPageTag != "配置" || !IsOptionsPageModified) return;

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

    private async void OnNavigatingFromLevelGroupPage(object sender, NavigatingCancelEventArgs e)
    {
        if (RootNavigation.SelectedItem?.TargetPageTag != "等级组" || !IsLevelGroupPageModified) return;

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