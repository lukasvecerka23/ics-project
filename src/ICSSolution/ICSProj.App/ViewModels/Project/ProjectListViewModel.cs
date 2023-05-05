using CommunityToolkit.Mvvm.Input;
using ICSProj.App.Services;
using ICSProj.App.Views.Popups;
using ICSProj.BL.Facades;
using ICSProj.BL.Models;

namespace ICSProj.App.ViewModels;

public partial class ProjectListViewModel: ViewModelBase
{
    private readonly IProjectFacade _projectFacade;
    private readonly INavigationService _navigationService;
    private readonly ILoginService _loginService;

    public IEnumerable<ProjectListModel> Projects { get; set; } = null!;

    public ProjectListViewModel(
        IProjectFacade projectFacade,
        INavigationService navigationService,
        ILoginService loginService,
        IMessengerService messengerService) : base(messengerService)
    {
        _projectFacade = projectFacade;
        _navigationService = navigationService;
        _loginService = loginService;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        Projects = await _projectFacade.GetAsync();
    }

    [RelayCommand]
    private async Task GoToDetailAsync(Guid id)
    {
        await _navigationService.GoToAsync<ProjectDetailViewModel>(
            new Dictionary<string, object?> { [nameof(ProjectDetailViewModel.Id)] = id });
    }

    [RelayCommand]
    private async Task ShowMenuPopupAsync()
    {
        await _navigationService.ShowPopupAsync(new MenuPopupView());
    }
}
