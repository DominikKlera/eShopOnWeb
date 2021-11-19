using Ardalis.Specification;
using Microsoft.eShopWeb.ApplicationCore.Entities;

namespace Microsoft.eShopWeb.ApplicationCore.Specifications;

public class CatalogFilterSpecification : Specification<CatalogItem>
{
    public CatalogFilterSpecification(int? brandId, int? typeId, string text)
    {
        Query.Where(i => (!brandId.HasValue || i.CatalogBrandId == brandId) &&
            (!typeId.HasValue || i.CatalogTypeId == typeId));

        if (!string.IsNullOrEmpty(text))
        {
            Query.Where(x => x.Name.Contains(text)
            || x.Description.Contains(text)
            || x.CatalogType.Type.Contains(text)
            || x.CatalogBrand.Brand.Contains(text));
        }
    }
}
