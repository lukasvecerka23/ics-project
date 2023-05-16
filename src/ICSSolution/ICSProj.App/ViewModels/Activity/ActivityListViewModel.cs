using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ICSProj.BL.Enums;
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
    private readonly IUserFacade _userFacade;
    public IEnumerable<TagListModel> Tags { get; set; } = null!;
    public IEnumerable<ProjectAssignListModel> Projects { get; set; } = null!;
    public IEnumerable<ActivityListModel> Activities { get; set; } = null!;
    public ActivityFilterModel Filter { get; set; } = ActivityFilterModel.Empty;
    public ActivityCreationModel Creation { get; set; } = ActivityCreationModel.Empty;
    public UserDetailModel CurrentUser { get; set; }
    public ActivityDetailModel Activity { get; set; } = ActivityDetailModel.Empty;
    public List<TimePeriod> TimePeriods { get; set; }

    public ActivityListViewModel(
        IActivityFacade activityFacade,
        INavigationService navigationService,
        ILoginService loginService,
        IMessengerService messengerService,
        IUserFacade userFacade,
        IAlertService alertService) : base(messengerService)
    {
        _activityFacade = activityFacade;
        _navigationService = navigationService;
        _loginService = loginService;
        _alertService = alertService;
        _userFacade = userFacade;
        TimePeriods = new List<TimePeriod>((TimePeriod[])Enum.GetValues(typeof(TimePeriod)));
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        CurrentUser = await _userFacade.GetAsync(_loginService.CurrentUserId);
        Tags = CurrentUser?.Tags;
        Projects = CurrentUser?.ProjectAssigns;
        Activities = _activityFacade.GetAsync().Result.Where(activity => activity.CreatorId == _loginService.CurrentUserId);
        Filter = ActivityFilterModel.Empty;
    }

    [RelayCommand]
    private async Task GoToDetailAsync(Guid id)
        => await _navigationService.GoToAsync<ActivityDetailViewModel>(
            new Dictionary<string, object>
            {
                [nameof(ActivityDetailViewModel.Id)] = id
            });

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
        Filter.AdjustDatesBasedOnTimePeriod(Filter);
        Activities = await _activityFacade.FilterActivities(CurrentUser.Id, Filter.Start, Filter.End, Filter.Project?.ProjectId, Filter.Tag?.Id);
    }

    [RelayCommand]
    private async Task AddActivityAsync()
    {
        Activity.CreatorId = CurrentUser.Id;
        Activity.Start = Creation.DateFrom + Creation.TimeFrom;
        Activity.End = Creation.DateTo + Creation.TimeTo;
        Activity.TagId = Creation.Tag?.Id;
        Activity.TagName = Creation.Tag?.Name;
        Activity.ProjectId = Creation.Project?.ProjectId;
        Activity.ProjectName = Creation.Project?.ProjectName;
        Activity.CreatorName = $"{_loginService.CurrentUser.Name} {_loginService.CurrentUser.Surname}";

        try
        {
            await _activityFacade.SaveAsync(CurrentUser.Id, Activity);
        }
        catch (Exception)
        {
            await _alertService.DisplayAsync("Vytvoření aktivity", "Nepodařilo se vytvořit novou aktivitu z důvodu kolize.");
        }

        Activity = ActivityDetailModel.Empty;
        Creation = ActivityCreationModel.Empty;
        await LoadDataAsync();
    }

    [RelayCommand]
    private async Task RefreshFilterAsync()
    {
        Filter = ActivityFilterModel.Empty;
        await LoadDataAsync();
    }

}
