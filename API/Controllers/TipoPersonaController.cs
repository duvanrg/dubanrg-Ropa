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
    public class TipoPersonaController : BaseControllerApi
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TipoPersonaController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<TipoPersonaDto>>> Get()
        {
            var tipoPersona = await _unitOfWork.TiposPersonas.GetAllAsync();
            return _mapper.Map<List<TipoPersonaDto>>(tipoPersona);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TipoPersonaDto>> Get(int id)
        {
            var tipoPersona = await _unitOfWork.TiposPersonas.GetByIdAsync(id);
            if (tipoPersona == null) return NotFound();
            return _mapper.Map<TipoPersonaDto>(tipoPersona);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TipoPersona>> Post(TipoPersonaDto tipoPersonaDto)
        {
            var tipoPersona = _mapper.Map<TipoPersona>(tipoPersonaDto);
            _unitOfWork.TiposPersonas.Add(tipoPersona);
            await _unitOfWork.SaveAsync();
            if (tipoPersona == null) return BadRequest();
            tipoPersonaDto.Id = tipoPersona.Id;
            return CreatedAtAction(nameof(Post), new { id = tipoPersonaDto.Id }, tipoPersonaDto);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TipoPersonaDto>> Put(int id, [FromBody] TipoPersonaDto tipoPersonaDto)
        {
            if (tipoPersonaDto == null) return NotFound();
            if (tipoPersonaDto.Id == 0) tipoPersonaDto.Id = id;
            if (tipoPersonaDto.Id != id) return BadRequest();
            var tipoPersona = await _unitOfWork.TiposPersonas.GetByIdAsync(id);
            _mapper.Map(tipoPersonaDto, tipoPersona);
            _unitOfWork.TiposPersonas.Update(tipoPersona);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<TipoPersonaDto>(tipoPersona);
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var tipoPersona = await _unitOfWork.TiposPersonas.GetByIdAsync(id);
            if (tipoPersona == null) return NotFound();
            _unitOfWork.TiposPersonas.Remove(tipoPersona);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}