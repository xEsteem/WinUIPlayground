using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.WinUI;
using CommunityToolkit.Common.Collections;
using CommunityToolkit.Diagnostics;
using PexelsDotNetSDK.Api;
using PexelsDotNetSDK.Models;

namespace WinUIPlayground.ViewModels;

public partial class ImageSearchViewModel : ObservableRecipient
{
    // TODO: turn into a service
    private readonly PexelsClient _imageSearchClient;
    private readonly ImageSearchResultSource _imageSearchResultSource;

    [ObservableProperty]
    private string? _searchTerm;
    
    public IncrementalLoadingCollection<ImageSearchResultSource, ImageViewModel> Images
    {
        get;
    }

    public ImageSearchViewModel()
    {
        _imageSearchClient = new PexelsClient("TODO: PUT THIS IN SETTINGS?");
        _imageSearchResultSource = new ImageSearchResultSource(string.Empty, _imageSearchClient);
        Images = new IncrementalLoadingCollection<ImageSearchResultSource, ImageViewModel>(_imageSearchResultSource, 80);
    }

    partial void OnSearchTermChanged(string? value)
    {
        _imageSearchResultSource.SearchTerm = value;
        Images.RefreshAsync();
    }
}

public class ImageSearchResultSource : IIncrementalSource<ImageViewModel>
{
    private readonly PexelsClient _imageSearchClient;

    public ImageSearchResultSource(string searchTerm, PexelsClient imageSearchClient)
    {
        Guard.IsNotNull(imageSearchClient);

        _imageSearchClient = imageSearchClient;
        SearchTerm = searchTerm;
    }

    public string? SearchTerm
    {
        get;
        set;
    }

    public async Task<IEnumerable<ImageViewModel>> GetPagedItemsAsync(int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(SearchTerm))
        {
            return Enumerable.Empty<ImageViewModel>();
        }

        var images = await this._imageSearchClient.SearchPhotosAsync(this.SearchTerm, page:pageIndex + 1, pageSize: pageSize);
        return images.photos.Select(x => new ImageViewModel(x));
    }
}


public class ImageViewModel : ObservableObject
{
    private readonly Photo _searchResult;

    public ImageViewModel(Photo searchResult)
    {
        _searchResult = searchResult;
    }

    public string Photographer => _searchResult.photographer;

    public string Alt => _searchResult.alt;

    public string Uri => _searchResult.source.original;

    public string ThumbnailUri => _searchResult.source.medium;
}
