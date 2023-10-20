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
    public class OrdenController : BaseControllerApi
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrdenController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<OrdenDto>>> Get()
        {
            var orden = await _unitOfWork.Ordenes.GetAllAsync();
            return _mapper.Map<List<OrdenDto>>(orden);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrdenDto>> Get(int id)
        {
            var orden = await _unitOfWork.Ordenes.GetByIdAsync(id);
            if (orden == null) return NotFound();
            return _mapper.Map<OrdenDto>(orden);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Orden>> Post(OrdenDto ordenDto)
        {
            var orden = _mapper.Map<Orden>(ordenDto);
            _unitOfWork.Ordenes.Add(orden);
            await _unitOfWork.SaveAsync();
            if (orden == null) return BadRequest();
            ordenDto.Id = orden.Id;
            return CreatedAtAction(nameof(Post), new { id = ordenDto.Id }, ordenDto);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OrdenDto>> Put(int id, [FromBody] OrdenDto ordenDto)
        {
            if (ordenDto == null) return NotFound();
            if (ordenDto.Id == 0) ordenDto.Id = id;
            if (ordenDto.Id != id) return BadRequest();
            var orden = await _unitOfWork.Ordenes.GetByIdAsync(id);
            _mapper.Map(ordenDto, orden);
            _unitOfWork.Ordenes.Update(orden);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<OrdenDto>(orden);
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var orden = await _unitOfWork.Ordenes.GetByIdAsync(id);
            if (orden == null) return NotFound();
            _unitOfWork.Ordenes.Remove(orden);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}