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
    public class VentaController : BaseControllerApi
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VentaController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<VentaDto>>> Get()
        {
            var venta = await _unitOfWork.Ventas.GetAllAsync();
            return _mapper.Map<List<VentaDto>>(venta);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VentaDto>> Get(int id)
        {
            var venta = await _unitOfWork.Ventas.GetByIdAsync(id);
            if (venta == null) return NotFound();
            return _mapper.Map<VentaDto>(venta);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Venta>> Post(VentaDto ventaDto)
        {
            var venta = _mapper.Map<Venta>(ventaDto);
            _unitOfWork.Ventas.Add(venta);
            await _unitOfWork.SaveAsync();
            if (venta == null) return BadRequest();
            ventaDto.Id = venta.Id;
            return CreatedAtAction(nameof(Post), new { id = ventaDto.Id }, ventaDto);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<VentaDto>> Put(int id, [FromBody] VentaDto ventaDto)
        {
            if (ventaDto == null) return NotFound();
            if (ventaDto.Id == 0) ventaDto.Id = id;
            if (ventaDto.Id != id) return BadRequest();
            var venta = await _unitOfWork.Ventas.GetByIdAsync(id);
            _mapper.Map(ventaDto, venta);
            _unitOfWork.Ventas.Update(venta);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<VentaDto>(venta);
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var venta = await _unitOfWork.Ventas.GetByIdAsync(id);
            if (venta == null) return NotFound();
            _unitOfWork.Ventas.Remove(venta);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}