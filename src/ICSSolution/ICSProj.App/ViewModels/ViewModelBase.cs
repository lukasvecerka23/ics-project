using CommunityToolkit.Mvvm.ComponentModel;
using ICSProj.App.Services;

namespace ICSProj.App.ViewModels;

public class ViewModelBase: ObservableRecipient, IViewModel
{
    private bool _isRefreshRequired;

    protected readonly IMessengerService MessengerService;

    protected ViewModelBase(IMessengerService messengerService) : base(messengerService.Messenger)
    {
        MessengerService = messengerService;
    }

    public async Task OnAppearingAsync()
    {
        if (_isRefreshRequired)
        {
            await LoadDataAsync();

            _isRefreshRequired = false;
        }
    }

    protected virtual Task LoadDataAsync()
    {
        return Task.CompletedTask;
    }
}
