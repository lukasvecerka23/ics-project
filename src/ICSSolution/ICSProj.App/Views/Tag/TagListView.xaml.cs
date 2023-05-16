using CommunityToolkit.Maui.Views;
using ICSProj.App.ViewModels;

namespace ICSProj.App.Views.Tag;

public partial class TagListView
{
    public TagListView(TagListViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
    }

    public void DisplayPopup(object sender, EventArgs e)
    {
        var tagCreationPopup = new TagCreationPopupView();
        this.ShowPopupAsync(tagCreationPopup);
    }
}
