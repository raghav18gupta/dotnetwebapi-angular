using API.Dtos;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    public class ProductsGenericController : BaseApiController
    {
        private IGenericRepository<Product> _productRepository;
        private IGenericRepository<ProductType> _productTypeRepository;
        private IGenericRepository<ProductBrand> _productBrandRepository;
        private readonly IMapper _mapper;

        public ProductsGenericController(
            IGenericRepository<Product> productRepository,
            IGenericRepository<ProductType> productTypeRepository,
            IGenericRepository<ProductBrand> productBrandRepository,
            IMapper mapper
        )
        {
            _productRepository = productRepository;
            _productTypeRepository = productTypeRepository;
            _productBrandRepository = productBrandRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery]ProductSpecificationParameters productParameters)
        {
            var specification = new ProductsWithTypesAndBrandsSpecification(productParameters);

            var product = await _productRepository.ListAsync(specification);

            var countSpec = new ProductsWithTypesAndBrandsSpecification(productParameters);
            var totalItems = await _productRepository.CountAsync(specification);
            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(product);

            return Ok(new Pagination<ProductToReturnDto>(productParameters.PageIndex, productParameters.PageSize, totalItems, data));
        }

        [HttpGet("{i}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int i)
        {
            var specification = new ProductsWithTypesAndBrandsSpecification(i);
            var product = await _productRepository.GetEntityWithSpecification(specification);

            if (product == null)
                return NotFound(new ApiResponse((int)HttpStatusCode.NotFound));

            return _mapper.Map<Product, ProductToReturnDto>(product);
        }


        [HttpGet("Brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            return Ok(await _productBrandRepository.ListAllAsync());
        }
        [HttpGet("Types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            return Ok(await _productTypeRepository.ListAllAsync());
        }
    }
}
