using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using CommunityToolkit.Diagnostics;
using WinUIPlayground.ViewModels;
using CommunityToolkit.WinUI.UI;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUIPlayground.Views;
/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class BlankPage1 : Page
{
    public BlankPage1()
    {   
        this.InitializeComponent();
    }

    private void OnSearchResultItemClick(object sender, ItemClickEventArgs e)
    {
        Guard.IsAssignableToType<ImageViewModel>(e.ClickedItem);
        Frame.Navigate(typeof(BlankPage2), e.ClickedItem, new SuppressNavigationTransitionInfo());
    }

    private async void OnScrollViewerScrolled(object? sender, ScrollViewerViewChangedEventArgs e)
    {
        var dataContext = (ImageSearchViewModel)DataContext;
        if (e.IsIntermediate || sender is not ScrollViewer scrollViewer)
        {
            return;
        }
        
        var distanceToEnd = scrollViewer.ExtentHeight - (scrollViewer.VerticalOffset + scrollViewer.ViewportHeight);
        if (distanceToEnd <= 2.0 * scrollViewer.ViewportHeight
            && dataContext.Images.HasMoreItems && !dataContext.Images.IsLoading)
        {
            await dataContext.Images.LoadMoreItemsAsync(0);
        }
    }

    private void OnGridViewLoaded(object sender, RoutedEventArgs e)
    {
        var scrollViewer = this.GridView.FindDescendant<ScrollViewer>();
        Guard.IsNotNull(scrollViewer);
        scrollViewer.ViewChanged += OnScrollViewerScrolled;
    }
}
