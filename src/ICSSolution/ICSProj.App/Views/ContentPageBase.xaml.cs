using ICSProj.App.ViewModels;

namespace ICSProj.App.Views;

public partial class ContentPageBase
{
    protected IViewModel ViewModel { get; }

    public ContentPageBase(IViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = ViewModel = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await ViewModel.OnAppearingAsync();
    }

    protected override async void OnDisappearing()
    {
        base.OnDisappearing();
        await ViewModel.OnDisappearingAsync();
    }
}

