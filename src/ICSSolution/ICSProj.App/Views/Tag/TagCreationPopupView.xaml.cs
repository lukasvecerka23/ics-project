using CommunityToolkit.Maui.Views;

namespace ICSProj.App.Views.Tag;

public partial class TagCreationPopupView : Popup
{
    public TagCreationPopupView()
    {
        InitializeComponent();
    }

    private void ClosePopup(object sender, EventArgs e)
    {
        Close();
    }
}

