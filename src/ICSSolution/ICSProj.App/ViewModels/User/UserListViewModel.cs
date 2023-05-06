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

    public IEnumerable<UserListModel> Users { get; set; } = null!;
    public UserDetailModel User { get; set; } = UserDetailModel.Empty;

    public UserListViewModel(
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
        Users = await _userFacade.GetAsync();
    }

    [RelayCommand]
    private async Task AddUserAsync()
    {
        await _userFacade.SaveAsync(User);
        User = UserDetailModel.Empty;
        await LoadDataAsync();
    }

    [RelayCommand]
    private async Task GoToActivitiesAsync(Guid userId)
    {
        _loginService.CurrentUserId = userId;
        await _navigationService.GoToAsync<ActivityListViewModel>();
    }

    // [RelayCommand]
    // private async Task GoToTagsAsync(Guid userId)
    // {
    //     _loginService.CurrentUserId = userId;
    //     await _navigationService.GoToAsync<TagListViewModel>();
    // }

    public async void Receive(UserEditMessage message)
    {
        await LoadDataAsync();
    }

    public async void Receive(UserDeleteMessage message)
    {
        await LoadDataAsync();
    }

    // TODO: Add command for deleting user
    // TODO: Add command for creating new user
    // TODO: Add command for editing user
    // TODO: Add command to navigate inside the app and store id of selected user to service
    // TODO: Refresh data when user is deleted, edited or created
}
