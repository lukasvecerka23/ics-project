using ICSProj.App.Shells;
using ICSProj.App.Views.User;
using ICSProj.App.ViewModels.User;

namespace ICSProj.App;

public partial class App : Application
{
    public App(IServiceProvider serviceProvider)
    {
        InitializeComponent();

        MainPage = new UserListView(serviceProvider.GetRequiredService<UserListViewModel>());
    }
}
