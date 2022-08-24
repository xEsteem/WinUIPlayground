using System.Numerics;
using CommunityToolkit.Diagnostics;
using Microsoft.UI.Composition;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using WinUIPlayground.ViewModels;

namespace WinUIPlayground.Views;

public sealed partial class ImageSearchPage : Page
{
    public ImageSearchViewModel ViewModel
    {
        get;
    }

    public ImageSearchPage()
    {
        ViewModel = App.GetService<ImageSearchViewModel>();
        DataContext = ViewModel;

        InitializeComponent();
    }

    private void OnImageThumbnailClick(object sender, ItemClickEventArgs e)
    {
        throw new NotImplementedException();
    }
}
