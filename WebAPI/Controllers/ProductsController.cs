using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;
using Data.Parameters;
using WebAPI.ViewModels;
using AutoMapper;
using Data.Entities;
using Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Errors;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        //private readonly IProductRepository _repo;
        private readonly IUnitOfWork _unitofwork;

        private readonly IMapper _mapper;

        public ProductsController(IUnitOfWork unitofwork, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnVM>>> GetProductsAsync(
            [FromQuery]ProductParams productParams)
        {
            var products = await _unitofwork.ProductRepository.GetProductsAsync(productParams);

            var totalItems = await _unitofwork.ProductRepository.CountAsync();

            var data = _mapper.Map<List<Product>, List<ProductToReturnVM>>(products);

            return Ok(new Pagination<ProductToReturnVM>(productParams.PageIndex,
                productParams.PageSize, totalItems, data));

        }

        [HttpGet("GetProducts")]
        public async Task<ActionResult<List<ProductToReturnVM>>> GetProducts()
        {
            var products = await _unitofwork.ProductRepository.GetProductsAsync();

            return _mapper.Map<List<Product>, List<ProductToReturnVM>>(products);


            //return Ok(products);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<List<ProductBrand>>> GetBrands()
        {
            var brands = await _unitofwork.Repository<ProductBrand>().GetList();
            return brands.ToList();
            //return _mapper.Map<List<Product>, List<ProductToReturnVM>>(products);
        }

        [HttpGet("productTypes")]
        public async Task<ActionResult<List<ProductType>>> GetProductTypes()
        {
            var types = await _unitofwork.Repository<ProductType>().GetList();
            return types.ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnVM>> GetProduct(int id)
        {
            //var product = await _repo.ProductRepository.GetProductByIdAsync(id);
            var product = await _unitofwork.ProductRepository.GetProductByIdAsync(id);

            if(product == null)
                return NotFound(new ApiResponse(404));

            return _mapper.Map<Product, ProductToReturnVM>(product);
            
            //return new ProductToReturnVM
            //{
            //    Id = product.Id
            //};

            //return Ok(product);
        }

        //[HttpGet("brands")]
        //public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        //{
        //    var productBrands = await _repo.ProductRepository.GetProductBrandsAsync();

        //    return Ok(productBrands);
        //}

        //[HttpGet("types")]
        //public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductTypes()
        //{
        //    var p = await _repo.ProductRepository.GetProductTypesAsync();

        //    return Ok(p);
        //}
    }
}