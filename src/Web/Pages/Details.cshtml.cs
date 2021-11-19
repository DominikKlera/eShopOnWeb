using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.eShopWeb.Web.Services;

namespace Microsoft.eShopWeb.Web.Pages;

public class DetailsModel : PageModel
{
    private readonly ICatalogViewModelService _catalogViewModelService;

    public DetailsModel(ICatalogViewModelService catalogViewModelService)
    {
        _catalogViewModelService = catalogViewModelService;
    }
    public CatalogItemDetailsViewModel CatalogModel = new CatalogItemDetailsViewModel();


    public async Task OnGet(int productId)
    {
        CatalogModel = await _catalogViewModelService.GetCatalog(productId);
    }
}

public class CatalogItemDetailsViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string PictureUri { get; set; }
    public decimal Price { get; set; }
    public string Descrition { get; set; }
}
