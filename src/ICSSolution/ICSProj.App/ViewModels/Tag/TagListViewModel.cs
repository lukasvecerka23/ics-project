using CommunityToolkit.Mvvm.Input;
using ICSProj.App.Messages;
using ICSProj.App.Services;
using ICSProj.App.Views.Popups;
using ICSProj.BL.Facades;
using ICSProj.BL.Models;

namespace ICSProj.App.ViewModels;

public partial class TagListViewModel : ViewModelBase
{
    private readonly ITagFacade tagFacade;
    private readonly INavigationService navigationService;
    private readonly ILoginService loginService;

    public IEnumerable<TagListModel> Tags { get; set; } = null!;

    public TagDetailModel Tag { get; set; } = TagDetailModel.Empty;

    public TagListViewModel(
        ITagFacade tagFacade,
        INavigationService navigationService,
        ILoginService loginService,
        IMessengerService MessengerService) : base(MessengerService)
    {
        this.tagFacade = tagFacade;
        this.navigationService = navigationService;
        this.loginService = loginService;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        var tags = await tagFacade.GetAsync();
        Tags = tags.Where(tag => tag.CreatorId == loginService.CurrentUserId);
    }

    [RelayCommand]
    private async Task GoToCreateAsync()
    {
        await navigationService.GoToAsync("/edit");
    }

    [RelayCommand]
    private async Task GoToDetailAsync(Guid id)
    {
        await navigationService.GoToAsync<TagDetailViewModel>(
            new Dictionary<string, object?> { [nameof(TagDetailViewModel.Id)] = id });
    }

    [RelayCommand]
    private async Task AddTagAsync()
    {
        Console.WriteLine("test");
        Tag.CreatorId = loginService.CurrentUserId;
        await tagFacade.SaveAsync(Tag);

        MessengerService.Send(new TagEditMessage { TagId = Tag.Id });

        await LoadDataAsync();

        navigationService.SendBackButtonPressed();
    }

    [RelayCommand]
    private async Task ShowMenuPopupAsync()
    {
        await navigationService.ShowPopupAsync(new MenuPopupView());
    }

    public async void Receive(TagEditMessage message)
    {
        await LoadDataAsync();
    }

    public async void Receive(TagDeleteMessage message)
    {
        await LoadDataAsync();
    }

    [RelayCommand]
    private async Task ShowUserSettingsAsync()
    {
        await navigationService.ShowPopupAsync(new UserSettingsPopupView());
    }
}
