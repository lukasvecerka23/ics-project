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

    private Button lastClickedButton;

    private void SetButtonHighlight(object sender, EventArgs args)
    {
        if (lastClickedButton != null)
        {
            lastClickedButton.BorderWidth = 0;
        }

        var button = (Button)sender;
        button.BorderColor = Color.FromArgb("#CF12D8");
        button.BorderWidth = 3;

        lastClickedButton = button;
    }
}
