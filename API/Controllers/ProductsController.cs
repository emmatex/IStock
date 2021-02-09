using API.Dtos;
using API.Errors;
using API.Extensions;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;

        public ProductsController(IGenericRepository<Product> productsRepo, IGenericRepository<ProductBrand> productBrandRepo, IGenericRepository<ProductType> productTypeRepo, IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _productsRepo = productsRepo ?? throw new ArgumentNullException(nameof(productsRepo));
            _productTypeRepo = productTypeRepo ?? throw new ArgumentNullException(nameof(productTypeRepo));
            _productBrandRepo = productBrandRepo ?? throw new ArgumentNullException(nameof(productBrandRepo));
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductDto>>> GetProducts([FromQuery]
        ProductSpecParams productParams)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(productParams);
            var countSpec = new ProductWithFiltersForCountSpecificication(productParams);
            var totalItems = await _productsRepo.CountAsync(countSpec);
            var products = await _productsRepo.ListAsync(spec);
            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDto>>(products);
            return Ok(new Pagination<ProductDto>(productParams.PageIndex, productParams.PageSize, totalItems, data));
        }

        [HttpGet("{id}", Name = "GetProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDto>> GetProduct(string id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var product = await _productsRepo.GetEntityWithSpec(spec);
            if (product == null) return NotFound(new ApiResponse(404));
            return _mapper.Map<Product, ProductDto>(product);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            return Ok(await _productBrandRepo.ListAllAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            return Ok(await _productTypeRepo.ListAllAsync());
        }

        [HttpPost(Name = "CreateProduct")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProductDto>> CreateProduct(ProductToCreateDto createDto)
        {
            if (_productsRepo.IsExist(x => x.Name.ToLower() == createDto.Name.ToLower()))
                return BadRequest(new ApiResponse(400, $"{createDto.Name} already exists"));

            var product = _mapper.Map<Product>(createDto);
            product.Id = Guid.NewGuid().ToString();
            product.CreatedBy = HttpContext.User.RetrieveEmailFromPrincipal();
            await _productsRepo.Add(product);

            return CreatedAtRoute("GetProduct", new { productId = product.Id },
               _mapper.Map<ProductDto>(product));
        }

        [HttpPut("{productId}", Name = "UpdateProduct")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProductDto>> UpdateProduct(string productId, ProductToUpdateDto updateDto)
        {
            var product = await _productsRepo.GetByIdAsync(productId);
            if (product == null) return NotFound(new ApiResponse(404));

            _mapper.Map(updateDto, product);
            _productsRepo.Update(product);

            if (!await _productsRepo.SaveChangesAsync())
                return BadRequest(new ApiResponse(400, $"An error occured while trying to update"));

            return CreatedAtRoute("GetProduct", new { vehicleId = product.Id },
               _mapper.Map<ProductDto>(product));
        }

        [HttpDelete("{productId}", Name = "DeleteProduct")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteVehicle(string productId)
        {
            var product = await _productsRepo.GetByIdAsync(productId);
            if (product == null) return NotFound(new ApiResponse(404));
            _productsRepo.Delete(product);
            await _productsRepo.SaveChangesAsync();
            return NoContent();
        }

        [HttpOptions]
        public IActionResult GetProductOptions()
        {
            Response.Headers.Add("Allow", "GET,OPTIONS,POST,PUT,DELETE");
            return Ok();
        }

    }
}
