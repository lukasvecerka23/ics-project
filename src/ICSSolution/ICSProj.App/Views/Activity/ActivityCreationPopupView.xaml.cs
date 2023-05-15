using CommunityToolkit.Maui.Views;

namespace ICSProj.App.Views.Activity;

public partial class ActivityCreationPopupView : Popup
{
    public ActivityCreationPopupView()
    {
        InitializeComponent();
    }

    private void ClosePopup(object sender, EventArgs e)
    {
        Close();
    }
}

