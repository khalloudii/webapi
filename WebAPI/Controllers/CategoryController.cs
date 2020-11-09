using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitofwork;

        private readonly IMapper _mapper;

        public CategoryController(IUnitOfWork unitofwork, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
        }

        //[HttpGet("create")]
        //public async Task<ActionResult<List<ProductType>>> GetProductTypes()
        //{
        //    var types = await _unitofwork.ProductTypeRepository.GetList();
        //    return types.ToList();
        //}
    }
}