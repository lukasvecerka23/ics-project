using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ICSProj.App.Messages;
using ICSProj.App.Services;
using ICSProj.BL.Facades;
using ICSProj.BL.Models;
namespace ICSProj.App.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class UserDetailViewModel : ViewModelBase, IRecipient<UserEditMessage>, IRecipient<UserProjectAddMessage>, IRecipient<UserProjectDeleteMessage>
{
    private readonly IUserFacade userFacade;
    private readonly INavigationService navigationService;

    public Guid Id { get; set; }
    public UserDetailModel? User { get; set; }

    public UserDetailViewModel(
        IUserFacade userFacade,
        INavigationService navigationService,
        IMessengerService MessengerService)
        : base(MessengerService)
    {
        this.userFacade = userFacade;
        this.navigationService = navigationService;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        User = await userFacade.GetAsync(Id);
    }

    [RelayCommand]
    private async Task DeleteAsync()
    {
        if (User is not null)
        {
            await userFacade.DeleteAsync(User.Id);

            MessengerService.Send(new UserDeleteMessage());
        }
    }

    public async void Receive(UserEditMessage message)
    {
        if (message.UserId == User?.Id)
        {
            await LoadDataAsync();
        }
    }

    public async void Receive(UserProjectAddMessage message)
    {
        await LoadDataAsync();
    }

    public async void Receive(UserProjectDeleteMessage message)
    {
        await LoadDataAsync();
    }
}
