using System.Net;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.WinUI;
using CommunityToolkit.Common.Collections;
using CommunityToolkit.Diagnostics;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using PexelsDotNetSDK.Api;
using PexelsDotNetSDK.Models;
using WinUIPlayground.Contracts.Services;
using System;
using CommunityToolkit.WinUI.UI;

namespace WinUIPlayground.ViewModels;

public partial class ImageSearchViewModel : ObservableRecipient
{
    private readonly ImageSearchResultSource _imageSearchResultSource;

    [ObservableProperty]
    private string? _searchTerm;

    [ObservableProperty]
    private bool _validApiKey;

    public IncrementalLoadingCollection<ImageSearchResultSource, ImageViewModel> Images
    {
        get;
    }

    public ImageSearchViewModel(IPexelsImageSearchService pexelsImageSearchService)
    {
        _imageSearchResultSource = new ImageSearchResultSource(string.Empty, pexelsImageSearchService.Client);
        Images = new IncrementalLoadingCollection<ImageSearchResultSource, ImageViewModel>(_imageSearchResultSource, 80, onError: OnImageLoadError);
    }

    private void OnImageLoadError(Exception obj)
    {
        if (obj is ArgumentNullException)
        {
            // TODO: Point towards setting API key
        }

        if (obj.InnerException is ErrorResponse)
        {
            // TODO: Point towards updating API key
        }

        this.ValidApiKey = false;
    }

    partial void OnSearchTermChanged(string? value)
    {
        _imageSearchResultSource.SearchTerm = value;
        Images.RefreshAsync();
    }
}

public class ImageSearchResultSource : IIncrementalSource<ImageViewModel>
{
    private readonly PexelsClient? _imageSearchClient;

    public ImageSearchResultSource(string searchTerm, PexelsClient? imageSearchClient)
    {
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
        Guard.IsNotNull(this._imageSearchClient);

        if (string.IsNullOrWhiteSpace(SearchTerm))
        {
            return Enumerable.Empty<ImageViewModel>();
        }

        var images = await this._imageSearchClient.SearchPhotosAsync(this.SearchTerm, page: pageIndex + 1, pageSize: pageSize);
        return images.photos.Select(x => new ImageViewModel(x));
    }
}

public partial class ImageViewModel : ObservableObject
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

    public int Width => this._searchResult.width;

    public int Height => this._searchResult.height;
}
