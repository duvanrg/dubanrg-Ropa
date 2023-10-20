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
    public class TipoProteccionController : BaseControllerApi
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TipoProteccionController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<TipoProteccionDto>>> Get()
        {
            var tipoProteccion = await _unitOfWork.TiposProtecciones.GetAllAsync();
            return _mapper.Map<List<TipoProteccionDto>>(tipoProteccion);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TipoProteccionDto>> Get(int id)
        {
            var tipoProteccion = await _unitOfWork.TiposProtecciones.GetByIdAsync(id);
            if (tipoProteccion == null) return NotFound();
            return _mapper.Map<TipoProteccionDto>(tipoProteccion);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TipoProteccion>> Post(TipoProteccionDto tipoProteccionDto)
        {
            var tipoProteccion = _mapper.Map<TipoProteccion>(tipoProteccionDto);
            _unitOfWork.TiposProtecciones.Add(tipoProteccion);
            await _unitOfWork.SaveAsync();
            if (tipoProteccion == null) return BadRequest();
            tipoProteccionDto.Id = tipoProteccion.Id;
            return CreatedAtAction(nameof(Post), new { id = tipoProteccionDto.Id }, tipoProteccionDto);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TipoProteccionDto>> Put(int id, [FromBody] TipoProteccionDto tipoProteccionDto)
        {
            if (tipoProteccionDto == null) return NotFound();
            if (tipoProteccionDto.Id == 0) tipoProteccionDto.Id = id;
            if (tipoProteccionDto.Id != id) return BadRequest();
            var tipoProteccion = await _unitOfWork.TiposProtecciones.GetByIdAsync(id);
            _mapper.Map(tipoProteccionDto, tipoProteccion);
            _unitOfWork.TiposProtecciones.Update(tipoProteccion);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<TipoProteccionDto>(tipoProteccion);
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var tipoProteccion = await _unitOfWork.TiposProtecciones.GetByIdAsync(id);
            if (tipoProteccion == null) return NotFound();
            _unitOfWork.TiposProtecciones.Remove(tipoProteccion);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}