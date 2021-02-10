using API.Dtos;
using API.Errors;
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
    public class StockReceivesController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<StockReceive> _repository;

        public StockReceivesController(IGenericRepository<StockReceive> repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet(Name = "GetStockReceived")]
        public async Task<ActionResult<Pagination<StockReceiveDto>>> Get([FromQuery] SpecParams specParams)
        {
            var spec = new StockReceiveSpecification(specParams);
            var countSpec = new StockReceiveCountSpecificication(specParams);
            var totalItems = await _repository.CountAsync(countSpec);
            var results = await _repository.ListAsync(spec);
            var data = _mapper.Map<IReadOnlyList<StockReceive>, IReadOnlyList<StockReceiveDto>>(results);
            return Ok(new Pagination<StockReceiveDto>(specParams.PageIndex, specParams.PageSize, totalItems, data));
        }

        [HttpGet("{id}", Name = "GetStockReceive")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SingleStockReceiveDto>> Get(string id)
        {
            var spec = new StockReceiveSpecification(id);
            var result = await _repository.GetEntityWithSpec(spec);
            if (result == null) return NotFound(new ApiResponse(404));
            return _mapper.Map<StockReceive, SingleStockReceiveDto>(result);
        }
    }
}
