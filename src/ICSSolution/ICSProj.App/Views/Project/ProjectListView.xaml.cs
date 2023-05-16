using CommunityToolkit.Maui.Views;
using ICSProj.App.ViewModels;

namespace ICSProj.App.Views.Project;

public partial class ProjectListView
{
    public ProjectListView(ProjectListViewModel viewModel): base(viewModel)
    {
        InitializeComponent();
    }

    public void DisplayPopup(object sender, EventArgs e)
    {
        var projectCreationPopup = new ProjectCreationPopupView();
        this.ShowPopupAsync(projectCreationPopup);
    }
}

