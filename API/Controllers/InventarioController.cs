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
    public class InventarioController : BaseControllerApi
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public InventarioController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<InventarioDto>>> Get()
        {
            var inventario = await _unitOfWork.Inventarios.GetAllAsync();
            return _mapper.Map<List<InventarioDto>>(inventario);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<InventarioDto>> Get(int id)
        {
            var inventario = await _unitOfWork.Inventarios.GetByIdAsync(id);
            if (inventario == null) return NotFound();
            return _mapper.Map<InventarioDto>(inventario);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Inventario>> Post(InventarioDto inventarioDto)
        {
            var inventario = _mapper.Map<Inventario>(inventarioDto);
            _unitOfWork.Inventarios.Add(inventario);
            await _unitOfWork.SaveAsync();
            if (inventario == null) return BadRequest();
            inventarioDto.Id = inventario.Id;
            return CreatedAtAction(nameof(Post), new { id = inventarioDto.Id }, inventarioDto);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<InventarioDto>> Put(int id, [FromBody] InventarioDto inventarioDto)
        {
            if (inventarioDto == null) return NotFound();
            if (inventarioDto.Id == 0) inventarioDto.Id = id;
            if (inventarioDto.Id != id) return BadRequest();
            var inventario = await _unitOfWork.Inventarios.GetByIdAsync(id);
            _mapper.Map(inventarioDto, inventario);
            _unitOfWork.Inventarios.Update(inventario);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<InventarioDto>(inventario);
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var inventario = await _unitOfWork.Inventarios.GetByIdAsync(id);
            if (inventario == null) return NotFound();
            _unitOfWork.Inventarios.Remove(inventario);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}