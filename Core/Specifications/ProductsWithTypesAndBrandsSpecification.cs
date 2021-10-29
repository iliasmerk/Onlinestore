using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification:BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification(ProductSpecParams productparams)
        :base(x=>
        (string.IsNullOrEmpty(productparams.Search)||x.Name.ToLower().Contains(productparams.Search)) &&
        (!productparams.brandId.HasValue||x.ProductBrandId==productparams.brandId)&&(!productparams.typeId.HasValue||x.ProductTypeId==productparams.typeId)
        )
        {
            AddInclude(x=>x.ProductType);
            AddInclude(x=>x.ProductBrand);
            AddOrderBy(x=>x.Name);
            ApplyPaging(productparams.PageSize * (productparams.PageIndex-1),productparams.PageSize);

            if (!string.IsNullOrEmpty(productparams.sort))
            {
                switch (productparams.sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(n => n.Name);
                        break;
                }
            
        }
        }

        public ProductsWithTypesAndBrandsSpecification(int id) : base(x=>x.Id==id)
        {
            AddInclude(x=>x.ProductType);
            AddInclude(x=>x.ProductBrand);
        }
    }
}