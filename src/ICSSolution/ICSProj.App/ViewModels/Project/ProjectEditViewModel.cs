using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ICSProj.App.Messages;
using ICSProj.App.Services;
using ICSProj.BL.Facades;
using ICSProj.BL.Models;

namespace ICSProj.App.ViewModels;

[QueryProperty(nameof(Project), nameof(Project))]
public partial class ProjectEditViewModel : ViewModelBase, IRecipient<ProjectEditMessage>
{
    private readonly IProjectFacade projectFacade;
    private readonly INavigationService navigationService;
    private readonly IAlertService alertService;

    public Guid Id { get; set; }
    public ProjectDetailModel Project { get; private set; }

    public ProjectEditViewModel(
        IProjectFacade projectFacade,
        INavigationService navigationService,
        IMessengerService MessengerService,
        IAlertService alertService)
        : base(MessengerService)
    {
        this.projectFacade = projectFacade;
        this.navigationService = navigationService;
        this.alertService = alertService;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Project = await projectFacade.GetAsync(Id);
    }

    [RelayCommand]
    private async Task DeleteAsync()
    {
        if (Project is not null)
        {
            try
            {
                await projectFacade.DeleteAsync(Project.Id);
                MessengerService.Send(new ProjectDeleteMessage());
            }
            catch (InvalidOperationException)
            {
                await alertService.DisplayAsync("Projekt", "Chyba");
            }
        }
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        await projectFacade.SaveAsync(Project);

        MessengerService.Send(new ProjectEditMessage { ProjectId = Project.Id });

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
