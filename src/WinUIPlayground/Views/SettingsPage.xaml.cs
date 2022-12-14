using CommunityToolkit.Diagnostics;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using WinUIPlayground.ViewModels;

namespace WinUIPlayground.Views;

// TODO: Set the URL for your privacy policy by updating SettingsPage_PrivacyTermsLink.NavigateUri in Resources.resw.
public sealed partial class SettingsPage : Page
{
    public SettingsViewModel ViewModel
    {
        get;
    }

    public SettingsPage()
    {
        ViewModel = App.GetService<SettingsViewModel>();
        InitializeComponent();
    }

    private void OnShowApiKeyTeachingTipClicked(object sender, RoutedEventArgs e)
    {
        ApiKeyTeachingTip.IsOpen = true;
    }
}
