using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Data;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Interfaces;
using Core.Specifications;
using API.Dtos;
using AutoMapper;
using API.Helpers;

namespace API.Controllers
{
   
    public class ProductsController:BaseApiController
    { 
       // private readonly IProductRepository _repo;
        private readonly IGenericRepository<Product> _productsRepo;

        private readonly IGenericRepository<ProductBrand> _productBrandRepo;

        private readonly IGenericRepository<ProductType> _productTypeRepo;

        
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productsRepo,IGenericRepository<ProductBrand> productBrandRepo,
        IGenericRepository<ProductType> productTypeRepo,IMapper mapper)
        {
            _mapper = mapper;
            _productsRepo=productsRepo;
            _productBrandRepo=productBrandRepo;
            _productTypeRepo=productTypeRepo;

        }

        [HttpGet]
       public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery] ProductSpecParams productparams){
            var spec = new ProductsWithTypesAndBrandsSpecification(productparams);
            var countspec =  new ProductWIthFiltersForCountSpecification(productparams);
            var totalItems = await _productsRepo.CountAsync(countspec);
        var products = await _productsRepo.ListAsync(spec);
        var data=_mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductToReturnDto>>(products);
        return Ok(new Pagination<ProductToReturnDto>(productparams.PageIndex,productparams.PageSize,totalItems,data));
        }

         [HttpGet("{id}")]
          public async Task<ActionResult<List<ProductToReturnDto>>> GetProduct(int id){ 
               var spec = new ProductsWithTypesAndBrandsSpecification(id);
               var product = await _productsRepo.GetEntityWithSpec(spec);
               return Ok(_mapper.Map<Product,ProductToReturnDto>(product));
//             return Ok(await _productsRepo.GetEntityWithSpec(spec));  
         }

         [HttpGet("brands")]
          public async Task<ActionResult<List<Product>>> GetProductBrands(){ 
             return Ok(await _productBrandRepo.ListAllAsync());  
         }

         [HttpGet("types")]
          public async Task<ActionResult<List<Product>>> GetProductTypes(){ 
             return Ok(await _productTypeRepo.ListAllAsync());  
         }
    }
}