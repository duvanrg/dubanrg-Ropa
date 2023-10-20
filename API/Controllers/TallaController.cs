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
    public class TallaController : BaseControllerApi
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TallaController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<TallaDto>>> Get()
        {
            var talla = await _unitOfWork.Tallas.GetAllAsync();
            return _mapper.Map<List<TallaDto>>(talla);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TallaDto>> Get(int id)
        {
            var talla = await _unitOfWork.Tallas.GetByIdAsync(id);
            if (talla == null) return NotFound();
            return _mapper.Map<TallaDto>(talla);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Talla>> Post(TallaDto tallaDto)
        {
            var talla = _mapper.Map<Talla>(tallaDto);
            _unitOfWork.Tallas.Add(talla);
            await _unitOfWork.SaveAsync();
            if (talla == null) return BadRequest();
            tallaDto.Id = talla.Id;
            return CreatedAtAction(nameof(Post), new { id = tallaDto.Id }, tallaDto);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TallaDto>> Put(int id, [FromBody] TallaDto tallaDto)
        {
            if (tallaDto == null) return NotFound();
            if (tallaDto.Id == 0) tallaDto.Id = id;
            if (tallaDto.Id != id) return BadRequest();
            var talla = await _unitOfWork.Tallas.GetByIdAsync(id);
            _mapper.Map(tallaDto, talla);
            _unitOfWork.Tallas.Update(talla);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<TallaDto>(talla);
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var talla = await _unitOfWork.Tallas.GetByIdAsync(id);
            if (talla == null) return NotFound();
            _unitOfWork.Tallas.Remove(talla);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}