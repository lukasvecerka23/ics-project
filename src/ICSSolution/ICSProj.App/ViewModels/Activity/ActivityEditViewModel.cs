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
        IMessengerService messengerService) : base(messengerService)
    {
        _activityFacade = activityFacade;
        _navigationService = navigationService;
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        Activity.Start = SelectedDateFrom + SelectedTimeFrom;
        Activity.End = SelectedDateTo + SelectedTimeTo;
        Activity.TagId = CreationTag?.Id ?? Activity.TagId;
        Activity.ProjectId = CreationProject?.ProjectId ?? Activity.ProjectId;
        Activity.TagName = CreationTag?.Name ?? Activity.TagName;
        Activity.ProjectName = CreationProject?.ProjectName ?? Activity.ProjectName;

        await _activityFacade.SaveAsync(Activity);

        MessengerService.Send(new ActivityEditMessage { ActivityId = Activity.Id });

        _navigationService.SendBackButtonPressed();
    }
}
