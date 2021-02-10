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
    public class ProductTypesController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<ProductType> _repository;

        public ProductTypesController(IGenericRepository<ProductType> repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet(Name = "GetProductTypes")]
        public async Task<ActionResult<Pagination<ProductTypeDto>>> GetProductTypes([FromQuery] SpecParams specParams)
        {
            var spec = new ProductTypeSpecification(specParams);
            var countSpec = new ProductTypeCountSpecificication(specParams);
            var totalItems = await _repository.CountAsync(countSpec);
            var bands = await _repository.ListAsync(spec);
            var data = _mapper.Map<IReadOnlyList<ProductType>, IReadOnlyList<ProductTypeDto>>(bands);
            return Ok(new Pagination<ProductTypeDto>(specParams.PageIndex, specParams.PageSize, totalItems, data));
        }

        [HttpGet("{typeId}", Name = "GetProductType")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductTypeDto>> GetProductType(string typeId)
        {
            var spec = new ProductTypeSpecification(typeId);
            var type = await _repository.GetEntityWithSpec(spec);
            if (type == null) return NotFound(new ApiResponse(404));
            return _mapper.Map<ProductType, ProductTypeDto>(type);
        }

        [HttpPost(Name = "CreateProductType")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProductTypeDto>> CreateProductType(CreateUpdateTypeDto createDto)
        {
            if (_repository.IsExist(x => x.Name.ToLower() == createDto.Name.ToLower()))
                return BadRequest(new ApiResponse(400, $"{createDto.Name} already exists"));

            var type = _mapper.Map<ProductType>(createDto);
            type.Id = Guid.NewGuid().ToString();
            type.CreatedBy = HttpContext.User.RetrieveEmailFromPrincipal();
            await _repository.Add(type);

            if (await _unitOfWork.Complete() <= 0)
                return BadRequest(new ApiResponse(400, $"An error occured while trying to insert"));

            return CreatedAtRoute("GetProductType", new { productId = type.Id },
                   _mapper.Map<ProductTypeDto>(type));
        }

        [HttpPut("{typeId}", Name = "UpdateProductType")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProductTypeDto>> UpdateProductType(string typeId, CreateUpdateTypeDto updateDto)
        {
            var type = await _repository.GetByIdAsync(typeId);
            if (type == null) return NotFound(new ApiResponse(404));

            _mapper.Map(updateDto, type);
            _repository.Update(type);

            if (await _unitOfWork.Complete() <= 0)
                return BadRequest(new ApiResponse(400, $"An error occured while trying to update"));

            return CreatedAtRoute("GetProductType", new { bandId = type.Id },
               _mapper.Map<ProductTypeDto>(type));
        }

        [HttpDelete("{productId}", Name = "DeleteProductType")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteProductType(string typeId)
        {
            var product = await _repository.GetByIdAsync(typeId);
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
