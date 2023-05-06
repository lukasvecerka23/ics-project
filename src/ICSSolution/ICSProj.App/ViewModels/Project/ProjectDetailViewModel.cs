using CommunityToolkit.Mvvm.Input;
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
    private readonly ILoginService _loginService;
   

    public Guid Id { get; set; }
    public ProjectDetailModel? Project { get; private set; }
    public IEnumerable<ActivityListModel>? Activities { get; set; }

    public string ButtonName
    {
        get; set;
    }

    public ProjectDetailViewModel(
        IProjectFacade projectFacade,
        IActivityFacade activityFacade,
        INavigationService navigationService,
        ILoginService loginService,
        IMessengerService MessengerService,
        IAlertService alertService)
        : base(MessengerService)
    {
        this.projectFacade = projectFacade;
        this.activityFacade = activityFacade;
        this.navigationService = navigationService;
        this.alertService = alertService;
        _loginService = loginService;

    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Project = await projectFacade.GetAsync(Id);
        Activities = await activityFacade.GetAsync();
        if (Project?.CreatorId == _loginService.CurrentUserId)
        {
            ButtonName = "Delete Project";
        }
        else
        {
            ButtonName = "Leave Project";
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
                // find userId that matches Id in projectassigns Project.ProjectAssigns.First().UserId and delete it from collection

                // finds project assign user id that corresponds to user
                //Project.ProjectAssigns.FirstOrDefault(item => item.UserId == _loginService.CurrentUserId));
                // Todo delete projectssign user id from the collection and save on DB

            }

        }
        MessengerService.Send(new ProjectDeleteMessage());
        navigationService.SendBackButtonPressed();
    }

    public async void Receive(ProjectEditMessage message)
    {
        if (message.ProjectId == Project?.Id)
        {
            await LoadDataAsync();
        }
    }
}
