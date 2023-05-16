using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ICSProj.App.Messages;
using ICSProj.App.Services;
using ICSProj.BL.Facades;
using ICSProj.BL.Models;

namespace ICSProj.App.ViewModels;

public partial class UserListViewModel : ViewModelBase, IRecipient<UserEditMessage>, IRecipient<UserDeleteMessage>
{
    private readonly IUserFacade _userFacade;
    private readonly INavigationService _navigationService;
    private readonly ILoginService _loginService;
    private readonly IAlertService _alertService;

    public IEnumerable<UserListModel> Users { get; set; } = null!;
    public UserDetailModel User { get; set; } = UserDetailModel.Empty;

    public UserListViewModel(
        IUserFacade userFacade,
        INavigationService navigationService,
        ILoginService loginService,
        IAlertService alertService,
        IMessengerService messengerService) : base(messengerService)
    {
        _userFacade = userFacade;
        _navigationService = navigationService;
        _loginService = loginService;
        _alertService = alertService;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        Users = await _userFacade.GetAsync();
    }

    [RelayCommand]
    private async Task AddUserAsync()
    {
        if (User.Name != string.Empty && User.Surname != string.Empty)
        {
            await _userFacade.SaveAsync(User);
        }
        else
        {
            await _alertService.DisplayAsync("Vytvoření uživatele", "Nelze vytvořit prázdného uživatele");
        }
        User = UserDetailModel.Empty;
        await LoadDataAsync();
    }

    [RelayCommand]
    private async Task GoToActivitiesAsync(Guid userId)
    {
        _loginService.CurrentUserId = userId;
        var user = await _userFacade.GetAsync(userId);
        _loginService.CurrentUser = user;
        await _navigationService.GoToAsync<ActivityListViewModel>();
    }

    public async void Receive(UserEditMessage message)
    {
        await LoadDataAsync();
    }

    public async void Receive(UserDeleteMessage message)
    {
        await LoadDataAsync();
    }
}
