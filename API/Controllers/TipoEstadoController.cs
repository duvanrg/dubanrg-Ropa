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
    public class TipoEstadoController : BaseControllerApi
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TipoEstadoController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<TipoEstadoDto>>> Get()
        {
            var tipoEstado = await _unitOfWork.TiposEstados.GetAllAsync();
            return _mapper.Map<List<TipoEstadoDto>>(tipoEstado);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TipoEstadoDto>> Get(int id)
        {
            var tipoEstado = await _unitOfWork.TiposEstados.GetByIdAsync(id);
            if (tipoEstado == null) return NotFound();
            return _mapper.Map<TipoEstadoDto>(tipoEstado);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TipoEstado>> Post(TipoEstadoDto tipoEstadoDto)
        {
            var tipoEstado = _mapper.Map<TipoEstado>(tipoEstadoDto);
            _unitOfWork.TiposEstados.Add(tipoEstado);
            await _unitOfWork.SaveAsync();
            if (tipoEstado == null) return BadRequest();
            tipoEstadoDto.Id = tipoEstado.Id;
            return CreatedAtAction(nameof(Post), new { id = tipoEstadoDto.Id }, tipoEstadoDto);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TipoEstadoDto>> Put(int id, [FromBody] TipoEstadoDto tipoEstadoDto)
        {
            if (tipoEstadoDto == null) return NotFound();
            if (tipoEstadoDto.Id == 0) tipoEstadoDto.Id = id;
            if (tipoEstadoDto.Id != id) return BadRequest();
            var tipoEstado = await _unitOfWork.TiposEstados.GetByIdAsync(id);
            _mapper.Map(tipoEstadoDto, tipoEstado);
            _unitOfWork.TiposEstados.Update(tipoEstado);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<TipoEstadoDto>(tipoEstado);
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var tipoEstado = await _unitOfWork.TiposEstados.GetByIdAsync(id);
            if (tipoEstado == null) return NotFound();
            _unitOfWork.TiposEstados.Remove(tipoEstado);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}