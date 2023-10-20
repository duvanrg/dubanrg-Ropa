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
    public class MunicipioController : BaseControllerApi
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MunicipioController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<MunicipioDto>>> Get()
        {
            var municipio = await _unitOfWork.Municipios.GetAllAsync();
            return _mapper.Map<List<MunicipioDto>>(municipio);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MunicipioDto>> Get(int id)
        {
            var municipio = await _unitOfWork.Municipios.GetByIdAsync(id);
            if (municipio == null) return NotFound();
            return _mapper.Map<MunicipioDto>(municipio);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Municipio>> Post(MunicipioDto municipioDto)
        {
            var municipio = _mapper.Map<Municipio>(municipioDto);
            _unitOfWork.Municipios.Add(municipio);
            await _unitOfWork.SaveAsync();
            if (municipio == null) return BadRequest();
            municipioDto.Id = municipio.Id;
            return CreatedAtAction(nameof(Post), new { id = municipioDto.Id }, municipioDto);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MunicipioDto>> Put(int id, [FromBody] MunicipioDto municipioDto)
        {
            if (municipioDto == null) return NotFound();
            if (municipioDto.Id == 0) municipioDto.Id = id;
            if (municipioDto.Id != id) return BadRequest();
            var municipio = await _unitOfWork.Municipios.GetByIdAsync(id);
            _mapper.Map(municipioDto, municipio);
            _unitOfWork.Municipios.Update(municipio);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<MunicipioDto>(municipio);
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var municipio = await _unitOfWork.Municipios.GetByIdAsync(id);
            if (municipio == null) return NotFound();
            _unitOfWork.Municipios.Remove(municipio);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}