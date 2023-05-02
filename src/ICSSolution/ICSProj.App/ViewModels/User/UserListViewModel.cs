using CommunityToolkit.Mvvm.Input;
using ICSProj.App.Services;
using ICSProj.BL.Facades;
using ICSProj.BL.Models;

namespace ICSProj.App.ViewModels.User;

public partial class UserListViewModel : ViewModelBase
{
    private readonly IUserFacade _userFacade;
    // private readonly INavigationService _navigationService;

    public IEnumerable<UserListModel> Users { get; set; } = null!;
    public UserDetailModel User { get; set; } = UserDetailModel.Empty;

    public UserListViewModel(
        IUserFacade userFacade,
        // INavigationService navigationService,
        IMessengerService messengerService) : base(messengerService)
    {
        _userFacade = userFacade;
        // _navigationService = navigationService;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        Users = await _userFacade.GetAsync();
        Console.WriteLine(Users);
    }

    [RelayCommand]
    private async Task AddUserAsync()
    {
        await _userFacade.SaveAsync(User);
        User = UserDetailModel.Empty;
        await LoadDataAsync();
    }



    // TODO: Add command for deleting user
    // TODO: Add command for creating new user
    // TODO: Add command for editing user
    // TODO: Add command to navigate inside the app and store id of selected user to service
    // TODO: Refresh data when user is deleted, edited or created
}
