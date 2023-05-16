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
    private readonly ILoginService _loginService;
    private readonly IAlertService _alertService;
    private readonly IUserFacade _userFacade;

    public IEnumerable<TagListModel> Tags { get; set; } = null!;
    public IEnumerable<ProjectAssignListModel> Projects { get; set; } = null!;
    public UserDetailModel CurrentUser { get; set; } = null!;
    public ActivityCreationModel EditActivity { get; set; } = ActivityCreationModel.Empty;
    public ActivityDetailModel Activity { get; init; } = ActivityDetailModel.Empty;

    public ActivityEditViewModel(
        IActivityFacade activityFacade,
        INavigationService navigationService,
        ILoginService loginService,
        IAlertService alertService,
        IUserFacade userFacade,
        IMessengerService messengerService) : base(messengerService)
    {
        _activityFacade = activityFacade;
        _navigationService = navigationService;
        _loginService = loginService;
        _alertService = alertService;
        _userFacade = userFacade;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        CurrentUser = await _userFacade.GetAsync(_loginService.CurrentUserId);
        Tags = CurrentUser?.Tags;
        Projects = CurrentUser?.ProjectAssigns;
        FillEditActivity();
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        Activity.Start = EditActivity.DateFrom + EditActivity.TimeFrom;
        Activity.End = EditActivity.DateTo + EditActivity.TimeTo;
        Activity.TagId = EditActivity.Tag?.Id;
        Activity.TagName = EditActivity.Tag?.Name;
        Activity.ProjectId = EditActivity.Project?.ProjectId;
        Activity.ProjectName = EditActivity.Project?.ProjectName;

        try
        {
            await _activityFacade.SaveAsync(_loginService.CurrentUserId, Activity);
        }
        catch (Exception)
        {
            await _alertService.DisplayAsync("Editace aktivity", "Aktivita je v kolizi s jinou aktivitou.");
        }

        MessengerService.Send(new ActivityEditMessage { ActivityId = Activity.Id });

        _navigationService.SendBackButtonPressed();
    }

    private void FillEditActivity()
    {
        EditActivity.DateFrom = Activity.Start.Date;
        EditActivity.DateTo = Activity.End.Date;
        EditActivity.TimeFrom = Activity.Start.TimeOfDay;
        EditActivity.TimeTo = Activity.End.TimeOfDay;
        EditActivity.Tag = Tags.FirstOrDefault(tag => tag.Id == Activity.TagId);
        EditActivity.Project = Projects.FirstOrDefault(project => project.ProjectId == Activity.ProjectId);
    }
}
