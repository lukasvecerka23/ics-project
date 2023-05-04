using ICSProj.App.Services;
using ICSProj.BL.Facades;
using ICSProj.BL.Models;

namespace ICSProj.App.ViewModels;

public partial class ActivityListViewModel: ViewModelBase
{
    private readonly IActivityFacade _activityFacade;
    private readonly INavigationService _navigationService;
    private readonly ILoginService _loginService;

    public ActivityListViewModel(
        IActivityFacade activityFacade,
        INavigationService navigationService,
        ILoginService loginService,
        IMessengerService messengerService) : base(messengerService)
    {
        _activityFacade = activityFacade;
        _navigationService = navigationService;
        _loginService = loginService;
    }

    public IEnumerable<ActivityListModel> Activities { get; set; } = null!;

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        var activities = await _activityFacade.GetAsync();
        Activities = activities.Where(activity => activity.CreatorId == _loginService.CurrentUserId);
    }

}
