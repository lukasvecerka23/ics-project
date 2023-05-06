using CommunityToolkit.Mvvm.Input;
using ICSProj.App.Services;
using ICSProj.BL.Facades;
using ICSProj.BL.Models;

namespace ICSProj.App.ViewModels;

public partial class UserProfileViewModel : ViewModelBase
{
    private readonly IUserFacade _userFacade;
    private readonly INavigationService _navigationService;
    private readonly ILoginService _loginService;

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
}
