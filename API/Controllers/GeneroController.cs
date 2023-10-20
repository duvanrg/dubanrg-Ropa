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
    public class GeneroController : BaseControllerApi
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GeneroController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<GeneroDto>>> Get()
        {
            var genero = await _unitOfWork.Generos.GetAllAsync();
            return _mapper.Map<List<GeneroDto>>(genero);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GeneroDto>> Get(int id)
        {
            var genero = await _unitOfWork.Generos.GetByIdAsync(id);
            if (genero == null) return NotFound();
            return _mapper.Map<GeneroDto>(genero);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Genero>> Post(GeneroDto generoDto)
        {
            var genero = _mapper.Map<Genero>(generoDto);
            _unitOfWork.Generos.Add(genero);
            await _unitOfWork.SaveAsync();
            if (genero == null) return BadRequest();
            generoDto.Id = genero.Id;
            return CreatedAtAction(nameof(Post), new { id = generoDto.Id }, generoDto);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GeneroDto>> Put(int id, [FromBody] GeneroDto generoDto)
        {
            if (generoDto == null) return NotFound();
            if (generoDto.Id == 0) generoDto.Id = id;
            if (generoDto.Id != id) return BadRequest();
            var genero = await _unitOfWork.Generos.GetByIdAsync(id);
            _mapper.Map(generoDto, genero);
            _unitOfWork.Generos.Update(genero);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<GeneroDto>(genero);
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var genero = await _unitOfWork.Generos.GetByIdAsync(id);
            if (genero == null) return NotFound();
            _unitOfWork.Generos.Remove(genero);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}