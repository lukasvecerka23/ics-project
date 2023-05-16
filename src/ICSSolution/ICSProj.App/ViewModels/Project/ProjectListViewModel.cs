using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ICSProj.App.Messages;
using ICSProj.App.Services;
using ICSProj.App.Views.Popups;
using ICSProj.BL.Facades;
using ICSProj.BL.Models;

namespace ICSProj.App.ViewModels;

public partial class ProjectListViewModel: ViewModelBase, IRecipient<ProjectDeleteMessage>, IRecipient<UserProjectLeaveJoinMessage>
{
    private readonly IProjectFacade _projectFacade;
    private readonly INavigationService _navigationService;
    private readonly ILoginService _loginService;

    public IEnumerable<ProjectListModel> Projects { get; set; } = null!;
    public UserDetailModel CurrentUser { get; set; }

    public ProjectDetailModel Project { get; set; } = ProjectDetailModel.Empty;

    public ProjectListViewModel(
        IProjectFacade projectFacade,
        INavigationService navigationService,
        ILoginService loginService,
        IMessengerService MessengerService) : base(MessengerService)
    {
        _projectFacade = projectFacade;
        _navigationService = navigationService;
        _loginService = loginService;
        CurrentUser = _loginService.CurrentUser;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        CurrentUser = _loginService.CurrentUser;
        Projects = await _projectFacade.GetAsync();
    }

    public async Task ShowUserProjects()
    {
        Projects = await _projectFacade.GetProjectsAssignedToUser(_loginService.CurrentUserId);
        MessengerService.Send(new ProjectEditMessage{ProjectId = Projects.First().Id});
    }

    public async Task ShowAllProjects()
    {
        await LoadDataAsync();
        MessengerService.Send(new ProjectEditMessage{ProjectId = Projects.First().Id});
    }

    [RelayCommand]
    private async Task GoToDetailAsync(Guid id)
    {
        await _navigationService.GoToAsync<ProjectDetailViewModel>(
            new Dictionary<string, object?> { [nameof(ProjectDetailViewModel.Id)] = id });
    }

    [RelayCommand]
    private async Task AddProjectAsync()
    {
        await base.LoadDataAsync();

        Project.CreatorId = _loginService.CurrentUserId;

        Project = await _projectFacade.SaveAsync(Project);
        await _projectFacade.RegisterProject(_loginService.CurrentUserId, Project.Id);
        Project = ProjectDetailModel.Empty;

        MessengerService.Send(new ProjectEditMessage { ProjectId = Project.Id });

        await LoadDataAsync();

        _navigationService.SendBackButtonPressed();
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

    public async void Receive(ProjectDeleteMessage message)
    {
        await LoadDataAsync();
    }

    public async void Receive(UserProjectLeaveJoinMessage message)
    {
        await LoadDataAsync();
    }
}
