using ICSProj.App.Services;
using ICSProj.BL.Facades;
using ICSProj.BL.Models;

namespace ICSProj.App.ViewModels.User;

public class UserListViewModel : ViewModelBase
{
    private readonly UserFacade _userFacade;
    private readonly INavigationService _navigationService;

    public IEnumerable<UserListModel> Users { get; set; } = null!;

    public UserListViewModel(
        UserFacade userFacade,
        INavigationService navigationService,
        IMessengerService messengerService) : base(messengerService)
    {
        _userFacade = userFacade;
        _navigationService = navigationService;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        Users = await _userFacade.GetAsync();
    }

    // TODO: Add command for deleting user
    // TODO: Add command for creating new user
    // TODO: Add command for editing user
    // TODO: Add command to navigate inside the app and store id of selected user to service
    // TODO: Refresh data when user is deleted, edited or created
}
