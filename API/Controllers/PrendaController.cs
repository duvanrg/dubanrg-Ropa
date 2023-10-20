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
    public class PrendaController : BaseControllerApi
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PrendaController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<PrendaDto>>> Get()
        {
            var prenda = await _unitOfWork.Prendas.GetAllAsync();
            return _mapper.Map<List<PrendaDto>>(prenda);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PrendaDto>> Get(int id)
        {
            var prenda = await _unitOfWork.Prendas.GetByIdAsync(id);
            if (prenda == null) return NotFound();
            return _mapper.Map<PrendaDto>(prenda);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Prenda>> Post(PrendaDto prendaDto)
        {
            var prenda = _mapper.Map<Prenda>(prendaDto);
            _unitOfWork.Prendas.Add(prenda);
            await _unitOfWork.SaveAsync();
            if (prenda == null) return BadRequest();
            prendaDto.Id = prenda.Id;
            return CreatedAtAction(nameof(Post), new { id = prendaDto.Id }, prendaDto);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PrendaDto>> Put(int id, [FromBody] PrendaDto prendaDto)
        {
            if (prendaDto == null) return NotFound();
            if (prendaDto.Id == 0) prendaDto.Id = id;
            if (prendaDto.Id != id) return BadRequest();
            var prenda = await _unitOfWork.Prendas.GetByIdAsync(id);
            _mapper.Map(prendaDto, prenda);
            _unitOfWork.Prendas.Update(prenda);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<PrendaDto>(prenda);
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var prenda = await _unitOfWork.Prendas.GetByIdAsync(id);
            if (prenda == null) return NotFound();
            _unitOfWork.Prendas.Remove(prenda);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}