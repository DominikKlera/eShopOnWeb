using Ardalis.Specification;
using Microsoft.eShopWeb.ApplicationCore.Entities;

namespace Microsoft.eShopWeb.ApplicationCore.Specifications;

public class CatalogFilterPaginatedSpecification : Specification<CatalogItem>
{
    public CatalogFilterPaginatedSpecification(int skip, int take, int? brandId, int? typeId, string text)
        : base()
    {
        if (take == 0)
        {
            take = int.MaxValue;
        }
        Query
            .Where(i => (!brandId.HasValue || i.CatalogBrandId == brandId) &&
            (!typeId.HasValue || i.CatalogTypeId == typeId))
            .Skip(skip).Take(take);

        if (!string.IsNullOrEmpty(text))
        {
            Query.Where(x => x.Name.Contains(text)
                    || x.Description.Contains(text)
                    || x.CatalogType.Type.Contains(text)
                    || x.CatalogBrand.Brand.Contains(text));
        }
    }
}
