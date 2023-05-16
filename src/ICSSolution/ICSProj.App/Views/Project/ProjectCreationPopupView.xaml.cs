using CommunityToolkit.Maui.Views;

namespace ICSProj.App.Views.Project;

public partial class ProjectCreationPopupView : Popup
{
    public ProjectCreationPopupView()
    {
        InitializeComponent();
    }

    private void ClosePopup(object sender, EventArgs e)
    {
        Close();
    }
}

