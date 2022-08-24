using System.Reflection;
using System.Windows.Input;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Microsoft.UI.Xaml;

using Windows.ApplicationModel;
using CommunityToolkit.Diagnostics;
using WinUIPlayground.Contracts.Services;
using WinUIPlayground.Helpers;
using WinUIPlayground.Services;

namespace WinUIPlayground.ViewModels;

public partial class SettingsViewModel : ObservableRecipient
{
    private readonly IThemeSelectorService _themeSelectorService;
    private readonly IPexelsImageSearchService _pexelsImageSearchService;

    [ObservableProperty]
    private ElementTheme _elementTheme;

    [ObservableProperty]
    private string _versionDescription;

    [ObservableProperty]
    private string _pexelsApiKey;

    public SettingsViewModel(IThemeSelectorService themeSelectorService, IPexelsImageSearchService pexelsImageSearchService)
    {
        Guard.IsNotNull(themeSelectorService);
        Guard.IsNotNull(pexelsImageSearchService);

        _themeSelectorService = themeSelectorService;
        _pexelsImageSearchService = pexelsImageSearchService;

        _elementTheme = _themeSelectorService.Theme;
        _pexelsApiKey = _pexelsImageSearchService.ApiKey;
        _versionDescription = GetVersionDescription();
    }

    partial void OnPexelsApiKeyChanged(string value)
    {
        _pexelsImageSearchService.SetApiKeyAsync(value);
    }

    [RelayCommand]
    private async Task SwitchTheme(ElementTheme targetTheme)
    {
        if (ElementTheme != targetTheme)
        {
            ElementTheme = targetTheme;
            await _themeSelectorService.SetThemeAsync(targetTheme);
        }
    }

    private static string GetVersionDescription()
    {
        Version version;

        if (RuntimeHelper.IsMSIX)
        {
            var packageVersion = Package.Current.Id.Version;

            version = new Version(packageVersion.Major, packageVersion.Minor, packageVersion.Build, packageVersion.Revision);
        }
        else
        {
            version = Assembly.GetExecutingAssembly().GetName().Version!;
        }

        return $"{"AppDisplayName".GetLocalized()} - {version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
    }
}
