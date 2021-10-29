using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductWIthFiltersForCountSpecification:BaseSpecification<Product>
    {
        public ProductWIthFiltersForCountSpecification(ProductSpecParams productparams):base(x=>
         (string.IsNullOrEmpty(productparams.Search)||x.Name.ToLower().Contains(productparams.Search)) &&
        (!productparams.brandId.HasValue||x.ProductBrandId==productparams.brandId)&&(!productparams.typeId.HasValue||x.ProductTypeId==productparams.typeId)
        )
        {
            
        }
    }
}