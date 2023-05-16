using CommunityToolkit.Maui.Views;
using ICSProj.App.ViewModels;

namespace ICSProj.App.Views.User;

public partial class UserListView
{
    public UserListView(UserListViewModel viewModel): base(viewModel)
    {
        InitializeComponent();
    }

    public void DisplayPopup(object sender, EventArgs e)
    {
        var userCreationPopup = new UserCreationPopupView();
        this.ShowPopupAsync(userCreationPopup);
    }
}

