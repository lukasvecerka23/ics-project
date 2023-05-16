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
    private readonly IAlertService _alertService;
    public IEnumerable<TagListModel> Tags { get; set; } = null!;
    public IEnumerable<ProjectAssignListModel> Projects { get; set; } = null!;
    public IEnumerable<ActivityListModel> Activities { get; set; } = null!;
    public TagListModel Tag { get; set; } = null!;
    public ProjectAssignListModel Project  { get; set; } = null!;
    public UserDetailModel CurrentUser { get; set; }
    public DateTime Start { get; set; } = DateTime.Today;
    public DateTime End  { get; set; } = DateTime.Today;
    public ActivityDetailModel Activity { get; set; } = ActivityDetailModel.Empty;
    public TagListModel CreationTag { get; set; } = TagListModel.Empty;
    public ProjectAssignListModel CreationProject { get; set; } = ProjectAssignListModel.Empty;
    public TimeSpan SelectedTimeFrom { get; set; } = TimeSpan.Zero;
    public DateTime SelectedDateFrom { get; set; } = DateTime.Today;
    public TimeSpan SelectedTimeTo { get; set; } = TimeSpan.Zero;
    public DateTime SelectedDateTo { get; set; } = DateTime.Today;

    public ActivityListViewModel(
        IActivityFacade activityFacade,
        INavigationService navigationService,
        ILoginService loginService,
        IMessengerService messengerService,
        IAlertService alertService) : base(messengerService)
    {
        _activityFacade = activityFacade;
        _navigationService = navigationService;
        _loginService = loginService;
        _alertService = alertService;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        CurrentUser = _loginService.CurrentUser;
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
        Activities = await _activityFacade.FilterActivities(CurrentUser.Id, Start, End, Project?.ProjectId, Tag?.Id);
        RefreshFilter();
    }

    private void RefreshFilter()
    {
        Tags = CurrentUser.Tags;
        Projects = CurrentUser.ProjectAssigns;
        Tag = null;
        Project = null;
        End = DateTime.Today;
        Start = DateTime.Today;
    }

    [RelayCommand]
    private async Task AddActivityAsync()
    {
        Activity.CreatorId = CurrentUser.Id;
        Activity.Start = SelectedDateFrom + SelectedTimeFrom;
        Activity.End = SelectedDateTo + SelectedTimeTo;
        Activity.TagId = CreationTag?.Id;
        Activity.ProjectId = CreationProject?.ProjectId;
        Activity.TagName = CreationTag?.Name;
        Activity.ProjectName = CreationProject?.ProjectName;
        Activity.CreatorName = $"{_loginService.CurrentUser.Name} {_loginService.CurrentUser.Surname}";

        try
        {
            await _activityFacade.SaveAsync(CurrentUser.Id, Activity);
        }
        catch (Exception)
        {
            await _alertService.DisplayAsync("Test", "Test");
        }

        Activity = ActivityDetailModel.Empty;
        CreationTag = TagListModel.Empty;
        CreationProject = ProjectAssignListModel.Empty;
        SelectedTimeFrom = TimeSpan.Zero;
        SelectedDateFrom = DateTime.Today;
        SelectedTimeTo = TimeSpan.Zero;
        SelectedDateTo = DateTime.Today;
        await LoadDataAsync();
    }

}
