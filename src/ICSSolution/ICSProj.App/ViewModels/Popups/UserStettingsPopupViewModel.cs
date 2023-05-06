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

    [RelayCommand]
    private async Task GoToUserList()
    {
        await _navigationService.GoToAsync<UserListViewModel>();
    }

    [RelayCommand]
    private async Task GoToUserProfile()
    {
        await _navigationService.GoToAsync<UserProfileViewModel>();
    }
}
