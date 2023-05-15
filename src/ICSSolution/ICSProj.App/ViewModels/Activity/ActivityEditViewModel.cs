﻿using CommunityToolkit.Mvvm.Input;
using ICSProj.App.Services;
using ICSProj.App.Messages;
using ICSProj.BL.Facades;
using ICSProj.BL.Models;

namespace ICSProj.App.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class ActivityEditViewModel : ViewModelBase
{
    private readonly IActivityFacade _activityFacade;
    private readonly INavigationService _navigationService;

    public Guid Id { get; set; }

    public ActivityDetailModel? Activity { get; set; }

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
}
