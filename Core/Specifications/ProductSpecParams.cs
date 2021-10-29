using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class ProductSpecParams
    {
        private const int MaxPageSize=50;
        public int PageIndex {get;set;} =1;

        public int _pagesize = 6;

        private string _search;

        public int PageSize{
            get=>_pagesize;
            set=>_pagesize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public int? brandId {get;set;}

        public int? typeId{get;set;}

        public string sort{get;set;}

        public string Search{
            get=>_search;
            set=>_search = value.ToLower();
        }

    }
}