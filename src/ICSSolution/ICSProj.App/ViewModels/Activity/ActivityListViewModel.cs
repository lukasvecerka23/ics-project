using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ICSProj.App.Messages;
using ICSProj.App.Services;
using ICSProj.App.Views.Popups;
using ICSProj.BL.Facades;
using ICSProj.BL.Models;

namespace ICSProj.App.ViewModels;

public partial class ActivityListViewModel: ViewModelBase, IRecipient<ActivityDeleteMessage>
{
    private readonly IActivityFacade _activityFacade;
    private readonly ITagFacade _tagFacade;
    private readonly IProjectFacade _projectFacade;
    private readonly INavigationService _navigationService;
    private readonly ILoginService _loginService;
    private string tagName = null;
    private string projectName = null;
    private DateTime start = DateTime.MinValue;
    private DateTime end = DateTime.MaxValue;

    public ActivityListViewModel(
        IActivityFacade activityFacade,
        ITagFacade tagFacade,
        IProjectFacade projectFacade,
        INavigationService navigationService,
        ILoginService loginService,
        IMessengerService messengerService) : base(messengerService)
    {
        _activityFacade = activityFacade;
        _tagFacade = tagFacade;
        _projectFacade = projectFacade;
        _navigationService = navigationService;
        _loginService = loginService;
    }

    public IEnumerable<ActivityListModel> Activities { get; set; } = null!;

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        Console.WriteLine(_loginService.CurrentUserId);
        var tagsByUser = await _tagFacade.GetTagsByUser(_loginService.CurrentUserId);
        var tagId = tagsByUser?.FirstOrDefault(tag => tag.Name == tagName)?.Id;

        var projectsByUser = await _projectFacade.GetAsync();
        var projectId = projectsByUser?.FirstOrDefault(project => project.Name == projectName)?.Id;

        Activities = await _activityFacade.FilterActivities(_loginService.CurrentUserId, start, end, projectId, tagId);
        Console.WriteLine(Activities.Count());
    }

    [RelayCommand]
    private async Task GoToDetailAsync(Guid id)
        => await _navigationService.GoToAsync<ActivityDetailViewModel>(
            new Dictionary<string, object?> { [nameof(ActivityDetailViewModel.Id)] = id });

    public async void Receive(ActivityDeleteMessage message)
    {
        await LoadDataAsync();
    }

    [RelayCommand]
    private async Task ShowMenuPopupAsync()
    {
        await _navigationService.ShowPopupAsync(new MenuPopupView());
    }

    [RelayCommand]
    private async Task ShowUserSettingsAsync()
    {
        await _navigationService.ShowPopupAsync(new UserSettingsPopupView());
    }

}
