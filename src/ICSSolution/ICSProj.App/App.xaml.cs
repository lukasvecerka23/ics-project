using ICSProj.App.Shells;
namespace ICSProj.App;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
    }
}
