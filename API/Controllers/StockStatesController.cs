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
    public class StockStatesController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<StockState> _repository;

        public StockStatesController(IGenericRepository<StockState> repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet(Name = "GetStockStates")]
        public async Task<ActionResult<Pagination<StockStateDto>>> GetStockStates([FromQuery] SpecParams specParams)
        {
            var spec = new StockStateSpecification(specParams);
            var countSpec = new StockStateCountSpecificication(specParams);
            var totalItems = await _repository.CountAsync(countSpec);
            var stockstates = await _repository.ListAsync(spec);
            var data = _mapper.Map<IReadOnlyList<StockState>, IReadOnlyList<StockStateDto>>(stockstates);
            return Ok(new Pagination<StockStateDto>(specParams.PageIndex, specParams.PageSize, totalItems, data));
        }

        [HttpGet("{stockstateId}", Name = "GetStockState")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StockStateDto>> GetStockState(string stockstateId)
        {
            var spec = new StockStateSpecification(stockstateId);
            var stockstate = await _repository.GetEntityWithSpec(spec);
            if (stockstate == null) return NotFound(new ApiResponse(404));
            return _mapper.Map<StockState, StockStateDto>(stockstate);
        }

        [HttpPost(Name = "CreateProductType")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<StockStateDto>> CreateStockState(CreateUpdateStockStateDto createDto)
        {
            if (_repository.IsExist(x => x.Name.ToLower() == createDto.Name.ToLower()))
                return BadRequest(new ApiResponse(400, $"{createDto.Name} already exists"));

            var stockstate = _mapper.Map<StockState>(createDto);
            stockstate.Id = Guid.NewGuid().ToString();
            stockstate.CreatedBy = HttpContext.User.RetrieveEmailFromPrincipal();
            await _repository.Add(stockstate);

            if (await _unitOfWork.Complete() <= 0)
                return BadRequest(new ApiResponse(400, $"An error occured while trying to insert"));

            return CreatedAtRoute("GetStockState", new { productId = stockstate.Id },
                   _mapper.Map<StockStateDto>(stockstate));
        }

        [HttpPut("{stockstateId}", Name = "UpdateStockState")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<StockStateDto>> UpdateStockState(string stockstateId, CreateUpdateStockStateDto updateDto)
        {
            var stockstate = await _repository.GetByIdAsync(stockstateId);
            if (stockstate == null) return NotFound(new ApiResponse(404));

            _mapper.Map(updateDto, stockstate);
            _repository.Update(stockstate);

            if (await _unitOfWork.Complete() <= 0)
                return BadRequest(new ApiResponse(400, $"An error occured while trying to update"));

            return CreatedAtRoute("GetStockState", new { bandId = stockstate.Id },
               _mapper.Map<StockStateDto>(stockstate));
        }

        [HttpDelete("{stockstateId}", Name = "DeleteStockState")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteStockState(string stockstateId)
        {
            var product = await _repository.GetByIdAsync(stockstateId);
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
