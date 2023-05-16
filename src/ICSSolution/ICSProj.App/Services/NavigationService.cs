using ICSProj.App.Models;
using CommunityToolkit.Maui.Views;
using ICSProj.App.ViewModels;
using ICSProj.App.Views.Activity;
using ICSProj.App.Views.Project;
using ICSProj.App.Views.User;
using ICSProj.App.Views.Tag;

namespace ICSProj.App.Services;

public class NavigationService: INavigationService
{
    public Guid CurrentUserId { get; set; } = Guid.Empty;
    public IEnumerable<RouteModel> Routes { get; } = new List<RouteModel>
    {
        new("//users", typeof(UserListView), typeof(UserListViewModel)),
        new("//user/detail", typeof(UserProfileView), typeof(UserProfileViewModel)),

        new("//activities", typeof(ActivityListView), typeof(ActivityListViewModel)),
        new("//activities/detail", typeof(ActivityDetailView), typeof(ActivityDetailViewModel)),
        new("//activities/detail/edit", typeof(ActivityEditView), typeof(ActivityEditViewModel)),

        new("//projects", typeof(ProjectListView), typeof(ProjectListViewModel)),
        new("//projects/detail", typeof(ProjectDetailView), typeof(ProjectDetailViewModel)),
        new("//projects/detail/activity", typeof(ActivityDetailView), typeof(ActivityDetailViewModel)),

        new("//tags", typeof(TagListView), typeof(TagListViewModel)),
        new("//tags/detail", typeof(TagDetailView), typeof(TagDetailViewModel))
    };

    public async Task GoToAsync<TViewModel>()
        where TViewModel : IViewModel
    {
        var route = GetRouteByViewModel<TViewModel>();
        await Shell.Current.GoToAsync(route);
    }
    public async Task GoToAsync<TViewModel>(IDictionary<string, object> parameters)
        where TViewModel : IViewModel
    {
        var route = GetRouteByViewModel<TViewModel>();
        await Shell.Current.GoToAsync(route, parameters);
    }

    public async Task GoToAsync(string route)
        => await Shell.Current.GoToAsync(route);

    public async Task GoToAsync(string route, IDictionary<string, object> parameters)
        => await Shell.Current.GoToAsync(route, parameters);

    private string GetRouteByViewModel<TViewModel>()
        where TViewModel : IViewModel
        => Routes.First(route => route.ViewModelType == typeof(TViewModel)).Route;

    public bool SendBackButtonPressed()
        => Shell.Current.SendBackButtonPressed();

    public async Task ShowPopupAsync(Popup showPopup)
    {
        await Shell.Current.ShowPopupAsync(showPopup);
    }
}
