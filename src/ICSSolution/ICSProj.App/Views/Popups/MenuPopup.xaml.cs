using CommunityToolkit.Maui.Views;
using ICSProj.App.Services;
using ICSProj.App.ViewModels;

namespace ICSProj.App.Views.Popups;

public partial class MenuPopupView : Popup
{
    public MenuPopupView()
    {

        InitializeComponent();

        BindingContext = new MenuPopupViewModel(new NavigationService());
    }

    private void ClosePopup(object sender, EventArgs e)
    {
        Close();
    }
}
