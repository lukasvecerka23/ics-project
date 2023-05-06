using CommunityToolkit.Mvvm.Input;
using ICSProj.App.Services;
using ICSProj.App.Messages;
using ICSProj.BL.Facades;
using ICSProj.BL.Models;

namespace ICSProj.App.ViewModels;

public partial class UserProfileViewModel : ViewModelBase
{
    private readonly IUserFacade _userFacade;
    private readonly INavigationService _navigationService;
    private readonly ILoginService _loginService;

    public UserDetailModel? User { get; set; }

    public UserProfileViewModel(
        IUserFacade userFacade,
        INavigationService navigationService,
        ILoginService loginService,
        IMessengerService messengerService) : base(messengerService)
    {
        _userFacade = userFacade;
        _navigationService = navigationService;
        _loginService = loginService;
    }



    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        User = await _userFacade.GetAsync(_loginService.CurrentUserId);
    }

    [RelayCommand]
    private async Task DeleteAsync()
    {
        if (User is not null)
        {
            await _userFacade.DeleteAsync(_loginService.CurrentUserId);
            MessengerService.Send(new UserDeleteMessage());
            _navigationService.SendBackButtonPressed();
        }

        await _navigationService.GoToAsync<UserListViewModel>();
    }

    [RelayCommand]
    private async Task GoToUserListAsync()
    {
        await _navigationService.GoToAsync<UserListViewModel>();
    }
}
