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
    public class CargoController : BaseControllerApi
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CargoController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<CargoDto>>> Get()
        {
            var cargo = await _unitOfWork.Cargos.GetAllAsync();
            return _mapper.Map<List<CargoDto>>(cargo);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CargoDto>> Get(int id)
        {
            var cargo = await _unitOfWork.Cargos.GetByIdAsync(id);
            if (cargo == null) return NotFound();
            return _mapper.Map<CargoDto>(cargo);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Cargo>> Post(CargoDto cargoDto)
        {
            var cargo = _mapper.Map<Cargo>(cargoDto);
            _unitOfWork.Cargos.Add(cargo);
            await _unitOfWork.SaveAsync();
            if (cargo == null) return BadRequest();
            cargoDto.Id = cargo.Id;
            return CreatedAtAction(nameof(Post), new { id = cargoDto.Id }, cargoDto);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CargoDto>> Put(int id, [FromBody] CargoDto cargoDto)
        {
            if (cargoDto == null) return NotFound();
            if (cargoDto.Id == 0) cargoDto.Id = id;
            if (cargoDto.Id != id) return BadRequest();
            var cargo = await _unitOfWork.Cargos.GetByIdAsync(id);
            _mapper.Map(cargoDto, cargo);
            _unitOfWork.Cargos.Update(cargo);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<CargoDto>(cargo);
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var cargo = await _unitOfWork.Cargos.GetByIdAsync(id);
            if (cargo == null) return NotFound();
            _unitOfWork.Cargos.Remove(cargo);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}