using CommunityToolkit.Mvvm.Input;
using ICSProj.App.Services;
using ICSProj.App.Messages;
using ICSProj.BL.Facades;
using ICSProj.BL.Models;

namespace ICSProj.App.ViewModels;

[QueryProperty(nameof(Activity), nameof(Activity))]
public partial class ActivityEditViewModel : ViewModelBase
{
    private readonly IActivityFacade _activityFacade;
    private readonly INavigationService _navigationService;

    public ActivityDetailModel Activity { get; set; } = ActivityDetailModel.Empty;

    public ActivityEditViewModel(
        IActivityFacade activityFacade,
        INavigationService navigationService,
        IMessengerService messengerService) : base(messengerService)
    {
        _activityFacade = activityFacade;
        _navigationService = navigationService;
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        await _activityFacade.SaveAsync(Activity);

        MessengerService.Send(new ActivityEditMessage { ActivityId = Activity.Id });

        _navigationService.SendBackButtonPressed();
    }
}
