using PexelsDotNetSDK.Api;

namespace WinUIPlayground.Contracts.Services;

public interface IPexelsImageSearchService
{
    string ApiKey
    {
        get;
    }

    PexelsClient? Client
    {
        get;
    }

    Task InitializeAsync();

    Task SetApiKeyAsync(string value);
}