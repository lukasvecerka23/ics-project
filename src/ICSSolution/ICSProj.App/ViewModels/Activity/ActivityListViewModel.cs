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
    public TagListModel Tag { get; set; }
    public ProjectAssignListModel Project  { get; set; }
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
        Tags = _loginService.CurrentUser.Tags;
        Projects = _loginService.CurrentUser.ProjectAssigns;
    }

    public IEnumerable<ActivityListModel> Activities { get; set; } = null!;

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
<<<<<<< HEAD
        var tagsByUser = await _tagFacade.GetTagsByUser(_loginService.CurrentUserId);
        var tagId = tagsByUser?.FirstOrDefault(tag => tag.Name == TagName)?.Id;

        var projectsByUser = await _projectFacade.GetAsync();
        var projectId = projectsByUser?.FirstOrDefault(project => project.Name == ProjectName)?.Id;

        Activities = await _activityFacade.FilterActivities(_loginService.CurrentUserId, Start, End, projectId, tagId);
=======

        Activities = _activityFacade.GetAsync().Result.Where(activity => activity.CreatorId == _loginService.CurrentUserId);
>>>>>>> feature/add-view-models
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
        Activities = await _activityFacade.FilterActivities(_loginService.CurrentUserId, Start, End, Project.ProjectId, Tag.Id);
    }

}
