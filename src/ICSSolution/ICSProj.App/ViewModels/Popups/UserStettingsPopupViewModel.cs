using CommunityToolkit.Mvvm.Input;
using ICSProj.App.Services;

namespace ICSProj.App.ViewModels;

public partial class UserSettingsPopupViewModel
{
    private readonly INavigationService _navigationService;

    public UserSettingsPopupViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }
}
