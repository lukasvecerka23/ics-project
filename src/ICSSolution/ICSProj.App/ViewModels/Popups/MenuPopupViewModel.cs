using CommunityToolkit.Mvvm.Input;
using ICSProj.App.Services;

namespace ICSProj.App.ViewModels;

public partial class MenuPopupViewModel
{
    private readonly INavigationService _navigationService;

    public MenuPopupViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }


    [RelayCommand]
    private async Task GoToActivitiesAsync(Guid userId)
    {
        await _navigationService.GoToAsync<ActivityListViewModel>();
    }

    [RelayCommand]
    private async Task GoToTagsAsync(Guid userId)
    {
        await _navigationService.GoToAsync<TagListViewModel>();
    }

    [RelayCommand]
    private async Task GoToProjectsAsync(Guid userId)
    {
        await _navigationService.GoToAsync<ProjectListViewModel>();
    }
}
