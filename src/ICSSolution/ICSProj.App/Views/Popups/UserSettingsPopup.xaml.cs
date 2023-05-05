using CommunityToolkit.Maui.Views;
using ICSProj.App.Services;
using ICSProj.App.ViewModels;

namespace ICSProj.App.Views.Popups;

public partial class UserSettingsPopupView : Popup
{
    public UserSettingsPopupView()
    {

        InitializeComponent();

        BindingContext = new UserSettingsPopupViewModel(new NavigationService());
    }

    private void ClosePopup(object sender, EventArgs e)
    {
        Close();
    }
}
