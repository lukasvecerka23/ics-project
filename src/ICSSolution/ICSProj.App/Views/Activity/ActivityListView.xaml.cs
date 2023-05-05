using CommunityToolkit.Maui.Views;
using ICSProj.App.Services;
using ICSProj.App.ViewModels;
using ICSProj.App.Views.Popups;

namespace ICSProj.App.Views.Activity;

public partial class ActivityListView
{
    public ActivityListView(ActivityListViewModel viewModel): base(viewModel)
    {
        InitializeComponent();
    }

    // public void DisplayPopup(object sender, EventArgs e)
    // {
    //     var menuPopup = new MenuPopupView(BindingContext);
    //     this.ShowPopupAsync(menuPopup);
    // }
}

