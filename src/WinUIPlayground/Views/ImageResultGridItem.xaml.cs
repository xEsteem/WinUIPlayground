using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using WinUIPlayground.ViewModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUIPlayground.Views;
public sealed partial class ImageResultGridItem : UserControl
{
    public ImageResultGridItem()
    {
        this.InitializeComponent();
    }

    private void element_PointerEntered(object sender, PointerRoutedEventArgs e)
    {
        this.Image.Scale = new Vector3(1.05f, 1.05f, 1);
        this.VignetteTop.Opacity = 1d;
        this.VignetteBottom.Opacity = 1d;
        this.Alt.Opacity = 1d;
        this.Alt.Translation = new Vector3(0,15,0);
        this.Photographer.Opacity = 1d;
        this.Photographer.Translation = new Vector3(0,-15,0);
    }

    private void element_PointerExited(object sender, PointerRoutedEventArgs e)
    {
        this.Image.Scale = new Vector3(1.15f, 1.15f, 1);
        this.VignetteTop.Opacity = 0d;
        this.VignetteBottom.Opacity = 0d;
        this.Alt.Opacity = 0d;
        this.Alt.Translation = new Vector3(0,0,0);
        this.Photographer.Opacity = 0d;
        this.Photographer.Translation = new Vector3(0,0,0);
    }
}
