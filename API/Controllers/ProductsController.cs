﻿using API.Dtos;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Product> _repository;

        public ProductsController(IGenericRepository<Product> repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductDto>>> GetProducts([FromQuery] SpecParams specParams)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(specParams);
            var countSpec = new ProductWithFiltersForCountSpecificication(specParams);
            var totalItems = await _repository.CountAsync(countSpec);
            var products = await _repository.ListAsync(spec);
            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDto>>(products);
            return Ok(new Pagination<ProductDto>(specParams.PageIndex, specParams.PageSize, totalItems, data));
        }

        [HttpGet("{id}", Name = "GetProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDto>> GetProduct(string id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var product = await _repository.GetEntityWithSpec(spec);
            if (product == null) return NotFound(new ApiResponse(404));
            return _mapper.Map<Product, ProductDto>(product);
        }

        [HttpPost(Name = "CreateProduct")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProductDto>> CreateProduct(ProductToCreateDto createDto)
        {
            if (_repository.IsExist(x => x.Name.ToLower() == createDto.Name.ToLower()))
                return BadRequest(new ApiResponse(400, $"{createDto.Name} already exists"));

            var product = _mapper.Map<Product>(createDto);
            product.Id = Guid.NewGuid().ToString();
            product.CreatedBy = HttpContext.User.RetrieveEmailFromPrincipal();
            await _repository.Add(product);

            if (await _unitOfWork.Complete() <= 0)
                return BadRequest(new ApiResponse(400, $"An error occured while trying to insert"));

            return CreatedAtRoute("GetProduct", new { productId = product.Id },
                   _mapper.Map<ProductDto>(product));
        }

        [HttpPut("{productId}", Name = "UpdateProduct")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProductDto>> UpdateProduct(string productId, ProductToUpdateDto updateDto)
        {
            var product = await _repository.GetByIdAsync(productId);
            if (product == null) return NotFound(new ApiResponse(404));

            _mapper.Map(updateDto, product);
            _repository.Update(product);

            if (await _unitOfWork.Complete() <= 0)
                return BadRequest(new ApiResponse(400, $"An error occured while trying to update"));

            return CreatedAtRoute("GetProduct", new { productId = product.Id },
               _mapper.Map<ProductDto>(product));
        }

        [HttpDelete("{productId}", Name = "DeleteProduct")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteProduct(string productId)
        {
            var product = await _repository.GetByIdAsync(productId);
            if (product == null) return NotFound(new ApiResponse(404));
            _repository.Delete(product);

            if (await _unitOfWork.Complete() <= 0)
                return BadRequest(new ApiResponse(400, $"An error occured while trying to delete"));

            return NoContent();
        }

        [HttpOptions]
        public IActionResult GetOptions()
        {
            Response.Headers.Add("Allow", "GET,OPTIONS,POST,PUT,DELETE");
            return Ok();
        }

    }
}
