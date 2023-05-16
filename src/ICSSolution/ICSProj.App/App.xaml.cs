using ICSProj.App.Shells;

namespace ICSProj.App;

public partial class App : Application
{
    public App(IServiceProvider serviceProvider)
    {
        InitializeComponent();

        MainPage = serviceProvider.GetRequiredService<AppShell>();
    }

    protected override Window CreateWindow(IActivationState activationState)
    {
        var window = base.CreateWindow(activationState);

        const int newWidth = 950;
        const int newHeight = 700;

        window.Width = newWidth;
        window.Height = newHeight;
        window.MaximumWidth = newWidth;
        window.MaximumHeight = newHeight;
        window.MinimumWidth = newWidth;
        window.MinimumHeight = newHeight;

        return window;
    }
}
