using CommunityToolkit.Maui.Views;
using ICSProj.App.Services;
using ICSProj.App.ViewModels;
using ICSProj.App.ViewModels;

namespace ICSProj.App.Views.User;

public partial class UserProfileView
{
    private readonly UserProfileViewModel _viewModel;

	public UserProfileView(UserProfileViewModel viewModel): base(viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
	}

    async void OnClickedButton(object sender, EventArgs e)
    {
        await _viewModel.GetBackToUserList();
    }
}
