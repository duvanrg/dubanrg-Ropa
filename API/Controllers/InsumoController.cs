using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{
    public class InsumoController : BaseControllerApi
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public InsumoController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<InsumoDto>>> Get()
        {
            var insumo = await _unitOfWork.Insumos.GetAllAsync();
            return _mapper.Map<List<InsumoDto>>(insumo);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<InsumoDto>> Get(int id)
        {
            var insumo = await _unitOfWork.Insumos.GetByIdAsync(id);
            if (insumo == null) return NotFound();
            return _mapper.Map<InsumoDto>(insumo);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Insumo>> Post(InsumoDto insumoDto)
        {
            var insumo = _mapper.Map<Insumo>(insumoDto);
            _unitOfWork.Insumos.Add(insumo);
            await _unitOfWork.SaveAsync();
            if (insumo == null) return BadRequest();
            insumoDto.Id = insumo.Id;
            return CreatedAtAction(nameof(Post), new { id = insumoDto.Id }, insumoDto);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<InsumoDto>> Put(int id, [FromBody] InsumoDto insumoDto)
        {
            if (insumoDto == null) return NotFound();
            if (insumoDto.Id == 0) insumoDto.Id = id;
            if (insumoDto.Id != id) return BadRequest();
            var insumo = await _unitOfWork.Insumos.GetByIdAsync(id);
            _mapper.Map(insumoDto, insumo);
            _unitOfWork.Insumos.Update(insumo);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<InsumoDto>(insumo);
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var insumo = await _unitOfWork.Insumos.GetByIdAsync(id);
            if (insumo == null) return NotFound();
            _unitOfWork.Insumos.Remove(insumo);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}