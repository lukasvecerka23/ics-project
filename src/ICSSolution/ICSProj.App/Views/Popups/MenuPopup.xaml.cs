using CommunityToolkit.Maui.Views;

namespace ICSProj.App.Views.Popups;

public partial class MenuPopupView : Popup
{
    public MenuPopupView()
    {
        InitializeComponent();
    }

    private void ClosePopup(object sender, EventArgs e)
    {
        Close();
    }
}
