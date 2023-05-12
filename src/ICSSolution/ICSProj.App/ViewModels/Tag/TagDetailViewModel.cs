using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ICSProj.App.Messages;
using ICSProj.App.Services;
using ICSProj.BL.Facades;
using ICSProj.BL.Models;
namespace ICSProj.App.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class TagDetailViewModel : ViewModelBase, IRecipient<TagEditMessage>
{
    private readonly ITagFacade tagFacade;
    private readonly INavigationService navigationService;
    private readonly IAlertService alertService;

    public Guid Id { get; set; }
    public TagDetailModel? Tag { get; private set; }

    [ObservableProperty]
    public Color tagColor;

    public TagDetailViewModel(
        ITagFacade tagFacade,
        INavigationService navigationService,
        IMessengerService MessengerService,
        IAlertService alertService)
        : base(MessengerService)
    {
        this.tagFacade = tagFacade;
        this.navigationService = navigationService;
        this.alertService = alertService;
        tagColor = Colors.Red;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Tag = await tagFacade.GetAsync(Id);

        TagColor = Color.FromArgb(Tag.Color);
    }

    [RelayCommand]
    private async Task DeleteAsync()
    {
        if (Tag is not null)
        {
            try
            {
                await tagFacade.DeleteAsync(Tag.Id);
                MessengerService.Send(new TagDeleteMessage());
            }
            catch (InvalidOperationException)
            {
                await alertService.DisplayAsync("Big Bad", "Error occured");
            }
        }
    }

    public async void Receive(TagEditMessage message)
    {
        if (message.TagId == Tag?.Id)
        {
            await LoadDataAsync();
        }
    }
}
