using CommunityToolkit.Mvvm.ComponentModel;
using ICSProj.App.Services;

namespace ICSProj.App.ViewModels;

public class ViewModelBase: ObservableRecipient, IViewModel
{
    private bool _isRefreshRequired = true;

    protected readonly IMessengerService MessengerService;

    protected ViewModelBase(IMessengerService messengerService) : base(messengerService.Messenger)
    {
        MessengerService = messengerService;
        IsActive = true;
    }

    public async Task OnAppearingAsync()
    {
        if (_isRefreshRequired)
        {
            await LoadDataAsync();

            _isRefreshRequired = false;
        }
    }

    public async Task OnDisappearingAsync()
    {
        _isRefreshRequired = true;
        await Task.CompletedTask;
    }

    protected virtual Task LoadDataAsync()
    {
        return Task.CompletedTask;
    }
}
