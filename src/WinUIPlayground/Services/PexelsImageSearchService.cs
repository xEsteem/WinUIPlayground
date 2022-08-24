using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls.Primitives;
using PexelsDotNetSDK.Api;
using WinUIPlayground.Contracts.Services;

namespace WinUIPlayground.Services;

public class PexelsImageSearchService : IPexelsImageSearchService
{
    private const string SettingsKey = "PexelsApiKey";

    private readonly ILocalSettingsService _localSettingsService;

    public PexelsImageSearchService(ILocalSettingsService localSettingsService)
    {
        _localSettingsService = localSettingsService;

        ApiKey = string.Empty;
    }

    public string ApiKey
    {
        get;
        private set;
    }

    public PexelsClient? Client
    {
        get;
        private set;
    }

    public async Task InitializeAsync()
    {
        ApiKey = await LoadApiKeyFromSettingsAsync();
        UpdateClient();
        await Task.CompletedTask;
    }

    public async Task SetApiKeyAsync(string value)
    {
        ApiKey = value;

        UpdateClient();
        await SaveApiKeyInSettingsAsync(value);
        await Task.CompletedTask;
    }

    private void UpdateClient()
    {
        if (string.IsNullOrWhiteSpace(this.ApiKey))
        {
            Client = null;
            return;
        }

        Client = new PexelsClient(this.ApiKey);
    }

    private async Task SaveApiKeyInSettingsAsync(string value)
    {
        await _localSettingsService.SaveSettingAsync(SettingsKey, value);
    }

    private async Task<string> LoadApiKeyFromSettingsAsync()
    {
        return await _localSettingsService.ReadSettingAsync<string>(SettingsKey) ?? string.Empty;
    }
}