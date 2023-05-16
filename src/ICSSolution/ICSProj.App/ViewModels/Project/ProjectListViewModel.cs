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
    private readonly IUserFacade _userFacade;

    public IEnumerable<ProjectListModel> Projects { get; set; } = null!;
    public UserDetailModel CurrentUser { get; set; }

    public ProjectDetailModel Project { get; set; } = ProjectDetailModel.Empty;

    public ProjectListViewModel(
        IProjectFacade projectFacade,
        INavigationService navigationService,
        ILoginService loginService,
        IMessengerService MessengerService,
        IUserFacade userFacade) : base(MessengerService)
    {
        _projectFacade = projectFacade;
        _navigationService = navigationService;
        _loginService = loginService;
        _userFacade = userFacade;
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
        //var ProjectsAssigned = _loginService.CurrentUser.ProjectAssigns
        //Projects = Projects.Where(i => i.CreatorId == _loginService.CurrentUserId);
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
        var Users = await _userFacade.GetAsync();
        var User = Users?.FirstOrDefault(user => user.Id == _loginService.CurrentUserId);

        Project.CreatorId = _loginService.CurrentUserId;
        Project.CreatorName = User.Name + User.Surname;

        await _projectFacade.SaveAsync(Project);
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
