using CommunityToolkit.Maui.Views;

namespace ICSProj.App.Views.User;

public partial class UserCreationPopupView : Popup
{
    public UserCreationPopupView()
    {
        InitializeComponent();
    }

    private void ClosePopup(object sender, EventArgs e)
    {
        Close();
    }
}

