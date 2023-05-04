using ICSProj.App.Services;
using ICSProj.BL.Facades;
using ICSProj.BL.Models;

namespace ICSProj.App.ViewModels;

public partial class TagListViewModel : ViewModelBase
{
    private readonly ITagFacade _tagFacade;
    private readonly INavigationService _navigationService;
    private readonly ILoginService _loginService;

    public IEnumerable<TagListModel> Tags { get; set; } = null!;

    public TagListViewModel(
        ITagFacade tagFacade,
        INavigationService navigationService,
        ILoginService loginService,
        IMessengerService messengerService) : base(messengerService)
    {
        _tagFacade = tagFacade;
        _navigationService = navigationService;
        _loginService = loginService;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        var tags = await _tagFacade.GetAsync();
        Tags = tags.Where(tag => tag.CreatorId == _loginService.CurrentUserId);
    }
}
