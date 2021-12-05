﻿using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsGenericController : Controller
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
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts()
        {
            var specification = new ProductsWithTypesAndBrandsSpecification();

            var product = await _productRepository.ListAsync(specification);
            return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(product));
        }

        [HttpGet("{i}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int i)
        {
            var specification = new ProductsWithTypesAndBrandsSpecification(i);
            var product = await _productRepository.GetEntityWithSpecification(specification);
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
