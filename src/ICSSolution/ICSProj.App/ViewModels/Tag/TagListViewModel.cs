using CommunityToolkit.Mvvm.Input;
using ICSProj.App.Services;
using ICSProj.App.Views.Popups;
using ICSProj.BL.Facades;
using ICSProj.BL.Models;

namespace ICSProj.App.ViewModels;

public partial class TagListViewModel : ViewModelBase
{
    private readonly ITagFacade _tagFacade;
    private readonly INavigationService _navigationService;
    private readonly ILoginService _loginService;

    public IEnumerable<TagListModel> Tags { get; set; } = null!;
    public TagDetailModel Tag { get; set; } = TagDetailModel.Empty;

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

    [RelayCommand]
    private async Task GoToDetailAsync(Guid id)
    {
        await _navigationService.GoToAsync<TagDetailViewModel>(
            new Dictionary<string, object?> { [nameof(TagDetailViewModel.Id)] = id });
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
}
