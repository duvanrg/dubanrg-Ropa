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
    public class InsumoPrendaController : BaseControllerApi
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public InsumoPrendaController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<InsumoPrendaDto>>> Get()
        {
            var insumoPrenda = await _unitOfWork.InsumosPrendas.GetAllAsync();
            return _mapper.Map<List<InsumoPrendaDto>>(insumoPrenda);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<InsumoPrendaDto>> Get(int id)
        {
            var insumoPrenda = await _unitOfWork.InsumosPrendas.GetByIdAsync(id);
            if (insumoPrenda == null) return NotFound();
            return _mapper.Map<InsumoPrendaDto>(insumoPrenda);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<InsumoPrenda>> Post(InsumoPrendaDto insumoPrendaDto)
        {
            var insumoPrenda = _mapper.Map<InsumoPrenda>(insumoPrendaDto);
            _unitOfWork.InsumosPrendas.Add(insumoPrenda);
            await _unitOfWork.SaveAsync();
            if (insumoPrenda == null) return BadRequest();
            insumoPrendaDto.Id = insumoPrenda.Id;
            return CreatedAtAction(nameof(Post), new { id = insumoPrendaDto.Id }, insumoPrendaDto);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<InsumoPrendaDto>> Put(int id, [FromBody] InsumoPrendaDto insumoPrendaDto)
        {
            if (insumoPrendaDto == null) return NotFound();
            if (insumoPrendaDto.Id == 0) insumoPrendaDto.Id = id;
            if (insumoPrendaDto.Id != id) return BadRequest();
            var insumoPrenda = await _unitOfWork.InsumosPrendas.GetByIdAsync(id);
            _mapper.Map(insumoPrendaDto, insumoPrenda);
            _unitOfWork.InsumosPrendas.Update(insumoPrenda);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<InsumoPrendaDto>(insumoPrenda);
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var insumoPrenda = await _unitOfWork.InsumosPrendas.GetByIdAsync(id);
            if (insumoPrenda == null) return NotFound();
            _unitOfWork.InsumosPrendas.Remove(insumoPrenda);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}