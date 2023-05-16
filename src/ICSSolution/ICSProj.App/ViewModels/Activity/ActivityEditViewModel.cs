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

    public IEnumerable<TagListModel> Tags { get; set; } = null!;
    public IEnumerable<ProjectAssignListModel> Projects { get; set; } = null!;

    public TagListModel Tag { get; set; } = null!;
    public ProjectAssignListModel Project { get; set; } = null!;
    public TagListModel CreationTag { get; set; } = TagListModel.Empty;
    public ProjectAssignListModel CreationProject { get; set; } = ProjectAssignListModel.Empty;
    public TimeSpan SelectedTimeFrom { get; set; } = TimeSpan.Zero;
    public DateTime SelectedDateFrom { get; set; } = DateTime.Today;
    public TimeSpan SelectedTimeTo { get; set; } = TimeSpan.Zero;
    public DateTime SelectedDateTo { get; set; } = DateTime.Today;

    public ActivityDetailModel Activity { get; init; } = ActivityDetailModel.Empty;

    public ActivityEditViewModel(
        IActivityFacade activityFacade,
        INavigationService navigationService,
        ILoginService loginService,
        IAlertService alertService,
        IMessengerService messengerService) : base(messengerService)
    {
        _activityFacade = activityFacade;
        _navigationService = navigationService;
        _loginService = loginService;
        _alertService = alertService;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        Tags = _loginService.CurrentUser.Tags;
        Projects = _loginService.CurrentUser.ProjectAssigns;
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        Activity.Start = SelectedDateFrom + SelectedTimeFrom;
        Activity.End = SelectedDateTo + SelectedTimeTo;
        if (CreationTag?.Id != Guid.Empty)
        {
            Activity.TagId = CreationTag?.Id;
            Activity.TagName = CreationTag?.Name;
        }
        if (CreationProject?.ProjectId != Guid.Empty)
        {
            Activity.ProjectId = CreationProject?.ProjectId;
            Activity.ProjectName = CreationProject?.ProjectName;
        }


        try
        {
            await _activityFacade.SaveAsync(_loginService.CurrentUserId, Activity);
        }
        catch (Exception)
        {
            await _alertService.DisplayAsync("Test", "Test");
        }

        MessengerService.Send(new ActivityEditMessage { ActivityId = Activity.Id });

        _navigationService.SendBackButtonPressed();
    }
}
