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
    public class DetalleVentaController : BaseControllerApi
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DetalleVentaController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<DetalleVentaDto>>> Get()
        {
            var detalleVenta = await _unitOfWork.DetallesVentas.GetAllAsync();
            return _mapper.Map<List<DetalleVentaDto>>(detalleVenta);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DetalleVentaDto>> Get(int id)
        {
            var detalleVenta = await _unitOfWork.DetallesVentas.GetByIdAsync(id);
            if (detalleVenta == null) return NotFound();
            return _mapper.Map<DetalleVentaDto>(detalleVenta);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DetalleVenta>> Post(DetalleVentaDto detalleVentaDto)
        {
            var detalleVenta = _mapper.Map<DetalleVenta>(detalleVentaDto);
            _unitOfWork.DetallesVentas.Add(detalleVenta);
            await _unitOfWork.SaveAsync();
            if (detalleVenta == null) return BadRequest();
            detalleVentaDto.Id = detalleVenta.Id;
            return CreatedAtAction(nameof(Post), new { id = detalleVentaDto.Id }, detalleVentaDto);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DetalleVentaDto>> Put(int id, [FromBody] DetalleVentaDto detalleVentaDto)
        {
            if (detalleVentaDto == null) return NotFound();
            if (detalleVentaDto.Id == 0) detalleVentaDto.Id = id;
            if (detalleVentaDto.Id != id) return BadRequest();
            var detalleVenta = await _unitOfWork.DetallesVentas.GetByIdAsync(id);
            _mapper.Map(detalleVentaDto, detalleVenta);
            _unitOfWork.DetallesVentas.Update(detalleVenta);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<DetalleVentaDto>(detalleVenta);
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var detalleVenta = await _unitOfWork.DetallesVentas.GetByIdAsync(id);
            if (detalleVenta == null) return NotFound();
            _unitOfWork.DetallesVentas.Remove(detalleVenta);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}