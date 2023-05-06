using CommunityToolkit.Maui.Views;
using ICSProj.App.Services;
using ICSProj.App.ViewModels;
using ICSProj.App.ViewModels;

namespace ICSProj.App.Views.User;

public partial class UserProfileView
{
	public UserProfileView(UserProfileViewModel viewModel): base(viewModel)
	{
		InitializeComponent();
	}
}
