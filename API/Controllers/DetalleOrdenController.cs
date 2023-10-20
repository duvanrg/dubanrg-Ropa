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
    public class DetalleOrdenController : BaseControllerApi
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DetalleOrdenController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<DetalleOrdenDto>>> Get()
        {
            var detalleOrden = await _unitOfWork.DetallesOrdenes.GetAllAsync();
            return _mapper.Map<List<DetalleOrdenDto>>(detalleOrden);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DetalleOrdenDto>> Get(int id)
        {
            var detalleOrden = await _unitOfWork.DetallesOrdenes.GetByIdAsync(id);
            if (detalleOrden == null) return NotFound();
            return _mapper.Map<DetalleOrdenDto>(detalleOrden);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DetalleOrden>> Post(DetalleOrdenDto detalleOrdenDto)
        {
            var detalleOrden = _mapper.Map<DetalleOrden>(detalleOrdenDto);
            _unitOfWork.DetallesOrdenes.Add(detalleOrden);
            await _unitOfWork.SaveAsync();
            if (detalleOrden == null) return BadRequest();
            detalleOrdenDto.Id = detalleOrden.Id;
            return CreatedAtAction(nameof(Post), new { id = detalleOrdenDto.Id }, detalleOrdenDto);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DetalleOrdenDto>> Put(int id, [FromBody] DetalleOrdenDto detalleOrdenDto)
        {
            if (detalleOrdenDto == null) return NotFound();
            if (detalleOrdenDto.Id == 0) detalleOrdenDto.Id = id;
            if (detalleOrdenDto.Id != id) return BadRequest();
            var detalleOrden = await _unitOfWork.DetallesOrdenes.GetByIdAsync(id);
            _mapper.Map(detalleOrdenDto, detalleOrden);
            _unitOfWork.DetallesOrdenes.Update(detalleOrden);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<DetalleOrdenDto>(detalleOrden);
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var detalleOrden = await _unitOfWork.DetallesOrdenes.GetByIdAsync(id);
            if (detalleOrden == null) return NotFound();
            _unitOfWork.DetallesOrdenes.Remove(detalleOrden);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}