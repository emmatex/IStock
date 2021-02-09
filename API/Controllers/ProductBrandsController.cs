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
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductBrandsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<ProductBrand> _repository;

        public ProductBrandsController(IGenericRepository<ProductBrand> repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductBrandDto>>> GetProductBands([FromQuery] SpecParams specParams)
        {
            var spec = new ProductBrandSpecification(specParams);
            var countSpec = new ProductBandCountSpecificication(specParams);
            var totalItems = await _repository.CountAsync(countSpec);
            var bands = await _repository.ListAsync(spec);
            var data = _mapper.Map<IReadOnlyList<ProductBrand>, IReadOnlyList<ProductBrandDto>>(bands);
            return Ok(new Pagination<ProductBrandDto>(specParams.PageIndex, specParams.PageSize, totalItems, data));
        }

        [HttpGet("{id}", Name = "GetProductBand")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductBrandDto>> GetProductBand(string id)
        {
            var spec = new ProductBrandSpecification(id);
            var band = await _repository.GetEntityWithSpec(spec);
            if (band == null) return NotFound(new ApiResponse(404));
            return _mapper.Map<ProductBrand, ProductBrandDto>(band);
        }

        [HttpPost(Name = "CreateProductBand")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProductBrandDto>> CreateProductBand(CreateUpdateBrandDto createDto)
        {
            if (_repository.IsExist(x => x.Name.ToLower() == createDto.Name.ToLower()))
                return BadRequest(new ApiResponse(400, $"{createDto.Name} already exists"));

            var brand = _mapper.Map<ProductBrand>(createDto);
            brand.Id = Guid.NewGuid().ToString();
            brand.CreatedBy = HttpContext.User.RetrieveEmailFromPrincipal();
            await _repository.Add(brand);

            if (await _unitOfWork.Complete() <= 0)
                return BadRequest(new ApiResponse(400, $"An error occured while trying to insert"));

            return CreatedAtRoute("GetProductBand", new { productId = brand.Id },
                   _mapper.Map<ProductBrandDto>(brand));
        }

        [HttpPut("{id}", Name = "UpdateProductBand")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProductBrandDto>> UpdateProductBand(string id, CreateUpdateBrandDto updateDto)
        {
            var band = await _repository.GetByIdAsync(id);
            if (band == null) return NotFound(new ApiResponse(404));

            _mapper.Map(updateDto, band);
            _repository.Update(band);

            if (await _unitOfWork.Complete() <= 0)
                return BadRequest(new ApiResponse(400, $"An error occured while trying to update"));

            return CreatedAtRoute("GetProductBand", new { bandId = band.Id },
               _mapper.Map<ProductBrandDto>(band));
        }

        [HttpDelete("{productId}", Name = "DeleteProduct")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteVehicle(string productId)
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

