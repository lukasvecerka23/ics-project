using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ICSProj.App.Services;
using ICSProj.App.Messages;
using ICSProj.BL.Facades;
using ICSProj.BL.Models;

namespace ICSProj.App.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class ActivityDetailViewModel : ViewModelBase, IRecipient<ActivityEditMessage>
{
    private readonly IActivityFacade _activityFacade;
    private readonly INavigationService _navigationService;

    public Guid Id { get; set; }

    public ActivityDetailModel Activity { get; set; }

    public ActivityDetailViewModel(
        IActivityFacade activityFacade,
        INavigationService navigationService,
        IMessengerService messengerService) : base(messengerService)
    {
        _activityFacade = activityFacade;
        _navigationService = navigationService;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        Activity = await _activityFacade.GetAsync(Id);
    }

    [RelayCommand]
    private async Task GoToEditAsync()
    {
        if (Activity is not null)
        {
            await _navigationService.GoToAsync<ActivityEditViewModel>(
                new Dictionary<string, object>
                {
                    [nameof(ActivityEditViewModel.Activity)] = Activity
                });
        }
    }

    [RelayCommand]
    private async Task DeleteAsync()
    {
        if (Activity is not null)
        {
            await _activityFacade.DeleteAsync(Activity.Id);
            MessengerService.Send(new ActivityDeleteMessage());
            _navigationService.SendBackButtonPressed();
        }
    }

    public async void Receive(ActivityEditMessage message)
    {
        if (message.ActivityId == Activity?.Id)
        {
            await LoadDataAsync();
        }
    }
}
