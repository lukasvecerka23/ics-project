using ICSProj.App.ViewModels;
using ICSProj.App.Models;

namespace ICSProj.App.Services;

public interface INavigationService
{
    IEnumerable<RouteModel> Routes { get; }

    Task GoToAsync<TViewModel>(IDictionary<string, object?> parameters)
        where TViewModel : IViewModel;

    Task GoToAsync(string route);
    Task GoToAsync(string route, IDictionary<string, object?> parameters);

    Task GoToAsync<TViewModel>()
        where TViewModel : IViewModel;

    bool SendBackButtonPressed();
}
