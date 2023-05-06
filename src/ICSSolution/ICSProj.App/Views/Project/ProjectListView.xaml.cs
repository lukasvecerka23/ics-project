using System.Diagnostics;
using CommunityToolkit.Maui.Views;
using ICSProj.App.ViewModels;

namespace ICSProj.App.Views.Project;

public partial class ProjectListView
{
    private readonly ProjectListViewModel _viewModel;
    public ProjectListView(ProjectListViewModel viewModel): base(viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
    }

    public void DisplayPopup(object sender, EventArgs e)
    {
        var projectCreationPopup = new ProjectCreationPopupView();
        this.ShowPopupAsync(projectCreationPopup);
    }

    async void OnToggled(object sender, ToggledEventArgs e)
    {
        // print to debug console
        if (e.Value)
        {
            //set ProjectList to User's Projects
            await _viewModel.ShowUserProjects();
        }
        else
        {
            //set ProjectList to All projects
            await _viewModel.ShowAllProjects();
        }
        Debug.WriteLine(e.ToString());
        Debug.WriteLine(e.Value);
    }
}

