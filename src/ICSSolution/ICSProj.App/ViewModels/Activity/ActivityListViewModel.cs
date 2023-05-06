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
    private readonly INavigationService _navigationService;
    private readonly ILoginService _loginService;
    public IEnumerable<TagListModel> Tags { get; set; } = null!;
    public IEnumerable<ProjectAssignListModel> Projects { get; set; } = null!;
    public TagListModel Tag { get; set; } = null!;
    public ProjectAssignListModel Project  { get; set; } = null!;
    public DateTime Start { get; set; } = DateTime.Today;
    public DateTime End  { get; set; } = DateTime.Today;

    public ActivityListViewModel(
        IActivityFacade activityFacade,
        INavigationService navigationService,
        ILoginService loginService,
        IMessengerService messengerService) : base(messengerService)
    {
        _activityFacade = activityFacade;
        _navigationService = navigationService;
        _loginService = loginService;
    }

    public IEnumerable<ActivityListModel> Activities { get; set; } = null!;

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Activities = _activityFacade.GetAsync().Result.Where(activity => activity.CreatorId == _loginService.CurrentUserId);
        RefreshFilter();
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

    [RelayCommand]
    private async Task FilterAsync()
    {
        Console.WriteLine(Project.ProjectId);
        Console.WriteLine(Tag.Id);
        Activities = await _activityFacade.FilterActivities(_loginService.CurrentUserId, Start, End, Project.ProjectId, Tag.Id);
        RefreshFilter();
    }

    private void RefreshFilter()
    {
        Tags = _loginService.CurrentUser.Tags;
        Console.WriteLine(Tags.Count());
        Projects = _loginService.CurrentUser.ProjectAssigns;
        Tag = null;
        Project = null;
        End = DateTime.Today;
        Start = DateTime.Today;
    }

}
