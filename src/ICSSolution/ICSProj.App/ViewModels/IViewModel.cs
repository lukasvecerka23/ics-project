namespace ICSProj.App.ViewModels;

public interface IViewModel
{
    Task OnAppearingAsync();
    Task OnDisappearingAsync();
}
