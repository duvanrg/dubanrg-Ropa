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
    public class InventarioTallaController : BaseControllerApi
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public InventarioTallaController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<InventarioTallaDto>>> Get()
        {
            var inventarioTalla = await _unitOfWork.InventariosTallas.GetAllAsync();
            return _mapper.Map<List<InventarioTallaDto>>(inventarioTalla);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<InventarioTallaDto>> Get(int id)
        {
            var inventarioTalla = await _unitOfWork.InventariosTallas.GetByIdAsync(id);
            if (inventarioTalla == null) return NotFound();
            return _mapper.Map<InventarioTallaDto>(inventarioTalla);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<InventarioTalla>> Post(InventarioTallaDto inventarioTallaDto)
        {
            var inventarioTalla = _mapper.Map<InventarioTalla>(inventarioTallaDto);
            _unitOfWork.InventariosTallas.Add(inventarioTalla);
            await _unitOfWork.SaveAsync();
            if (inventarioTalla == null) return BadRequest();
            inventarioTallaDto.Id = inventarioTalla.Id;
            return CreatedAtAction(nameof(Post), new { id = inventarioTallaDto.Id }, inventarioTallaDto);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<InventarioTallaDto>> Put(int id, [FromBody] InventarioTallaDto inventarioTallaDto)
        {
            if (inventarioTallaDto == null) return NotFound();
            if (inventarioTallaDto.Id == 0) inventarioTallaDto.Id = id;
            if (inventarioTallaDto.Id != id) return BadRequest();
            var inventarioTalla = await _unitOfWork.InventariosTallas.GetByIdAsync(id);
            _mapper.Map(inventarioTallaDto, inventarioTalla);
            _unitOfWork.InventariosTallas.Update(inventarioTalla);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<InventarioTallaDto>(inventarioTalla);
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var inventarioTalla = await _unitOfWork.InventariosTallas.GetByIdAsync(id);
            if (inventarioTalla == null) return NotFound();
            _unitOfWork.InventariosTallas.Remove(inventarioTalla);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}