﻿using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ICSProj.App.Messages;
using ICSProj.App.Services;
using ICSProj.BL.Facades;
using ICSProj.BL.Models;
using ICSProj.App.Services;
using ICSProj.App.ViewModels;
namespace ICSProj.App.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class ProjectDetailViewModel : ViewModelBase, IRecipient<ProjectEditMessage>
{
    private readonly IProjectFacade projectFacade;
    private readonly IActivityFacade activityFacade;
    private readonly INavigationService navigationService;
    private readonly IAlertService alertService;
    private readonly IUserFacade userFacade;
    private readonly ILoginService _loginService;


    public Guid Id { get; set; }
    public ProjectDetailModel? Project { get; private set; }
    public IEnumerable<ActivityListModel>? Activities { get; set; }

    public UserDetailModel? UserData { get; set; }

    public string ButtonName
    {
        get; set;
    }

    public bool isProjectAssignedToUser { get; set; }

    public ProjectDetailViewModel(
        IProjectFacade projectFacade,
        IActivityFacade activityFacade,
        INavigationService navigationService,
        ILoginService loginService,
        IMessengerService MessengerService,
        IUserFacade userFacade,
        IAlertService alertService)
        : base(MessengerService)
    {
        this.projectFacade = projectFacade;
        this.activityFacade = activityFacade;
        this.navigationService = navigationService;
        this.alertService = alertService;
        this.userFacade = userFacade;
        _loginService = loginService;

    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Project = await projectFacade.GetAsync(Id);
        //Activities = await activityFacade.GetAsync();
        UserData = await userFacade.GetAsync(_loginService.CurrentUserId);
        isProjectAssignedToUser = UserData.ProjectAssigns.Any(p => p.UserId == _loginService.CurrentUserId
                                                                        && p.ProjectId == Project.Id);
        if (Project?.CreatorId == _loginService.CurrentUserId)
        {
            ButtonName = "Delete Project";
        }
        else
        {

            if (isProjectAssignedToUser)
            {
                ButtonName = "Leave Project";
            }
            else
            {
                ButtonName = "Register Project";
            }
        }
    }


    [RelayCommand]
    private async Task DeleteProjectAsync()
    {
        if (Project?.CreatorId == _loginService.CurrentUserId)
        {
            if (Project is not null)
            {
                try
                {
                    await projectFacade.DeleteAsync(Project.Id);
                    MessengerService.Send(new ProjectDeleteMessage());
                    navigationService.SendBackButtonPressed();

                }
                catch (InvalidOperationException)
                {
                    await alertService.DisplayAsync("Projekt", "Chyba");
                }
            }
        }
        else
        {
            if (Project is not null)
            {
                if (isProjectAssignedToUser)
                {
                    await projectFacade.LeaveProject(_loginService.CurrentUserId, Project.Id);

                }
                else
                {
                    await projectFacade.RegisterProject(_loginService.CurrentUserId, Project.Id);
                }
                MessengerService.Send(new UserProjectLeaveJoinMessage());
            }

        }
        await LoadDataAsync();
    }

    public async void Receive(ProjectEditMessage message)
    {
        if (message.ProjectId == Project?.Id)
        {
            await LoadDataAsync();
        }
    }

    [RelayCommand]
    private async Task GoToActivityDetailAsync(Guid activityId)
    {
        await navigationService.GoToAsync("/activity",
                new Dictionary<string, object?> { [nameof(ActivityDetailViewModel.Id)] = activityId});
    }
}
