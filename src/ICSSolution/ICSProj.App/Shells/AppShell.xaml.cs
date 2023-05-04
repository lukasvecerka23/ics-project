using ICSProj.App.Services;

namespace ICSProj.App.Shells;

public partial class AppShell
{
    private readonly INavigationService navigationService;
    public AppShell(INavigationService navigationService)
    {
        this.navigationService = navigationService;
        InitializeComponent();
    }
}
