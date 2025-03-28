﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.eShopWeb.Web.Extensions;
using Microsoft.eShopWeb.Web.Pages;
using Microsoft.eShopWeb.Web.ViewModels;
using Microsoft.Extensions.Caching.Memory;

namespace Microsoft.eShopWeb.Web.Services;

public class CachedCatalogViewModelService : ICatalogViewModelService
{
    private readonly IMemoryCache _cache;
    private readonly CatalogViewModelService _catalogViewModelService;

    public CachedCatalogViewModelService(IMemoryCache cache,
        CatalogViewModelService catalogViewModelService)
    {
        _cache = cache;
        _catalogViewModelService = catalogViewModelService;
    }

    public async Task<IEnumerable<SelectListItem>> GetBrands()
    {
        return await _cache.GetOrCreateAsync(CacheHelpers.GenerateBrandsCacheKey(), async entry =>
                {
                    entry.SlidingExpiration = CacheHelpers.DefaultCacheDuration;
                    return await _catalogViewModelService.GetBrands();
                });
    }

    public Task<CatalogItemDetailsViewModel> GetCatalog(int productId)
    {
        return _catalogViewModelService.GetCatalog(productId);
    }

    public async Task<CatalogIndexViewModel> GetCatalogItems(int pageIndex, int itemsPage, int? brandId, int? typeId, string text)
    {
        text = text?.Trim().ToLower() ?? "";
        var cacheKey = CacheHelpers.GenerateCatalogItemCacheKey(pageIndex, Constants.ITEMS_PER_PAGE, brandId, typeId, text);

        return await _cache.GetOrCreateAsync(cacheKey, async entry =>
        {
            entry.SlidingExpiration = CacheHelpers.DefaultCacheDuration;
            return await _catalogViewModelService.GetCatalogItems(pageIndex, itemsPage, brandId, typeId, text);
        });
    }

    public async Task<IEnumerable<SelectListItem>> GetTypes()
    {
        return await _cache.GetOrCreateAsync(CacheHelpers.GenerateTypesCacheKey(), async entry =>
        {
            entry.SlidingExpiration = CacheHelpers.DefaultCacheDuration;
            return await _catalogViewModelService.GetTypes();
        });
    }
}
