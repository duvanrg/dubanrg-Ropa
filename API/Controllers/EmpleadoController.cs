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
    public class EmpleadoController : BaseControllerApi
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmpleadoController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<EmpleadoDto>>> Get()
        {
            var empleado = await _unitOfWork.Empleados.GetAllAsync();
            return _mapper.Map<List<EmpleadoDto>>(empleado);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EmpleadoDto>> Get(int id)
        {
            var empleado = await _unitOfWork.Empleados.GetByIdAsync(id);
            if (empleado == null) return NotFound();
            return _mapper.Map<EmpleadoDto>(empleado);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Empleado>> Post(EmpleadoDto empleadoDto)
        {
            var empleado = _mapper.Map<Empleado>(empleadoDto);
            _unitOfWork.Empleados.Add(empleado);
            await _unitOfWork.SaveAsync();
            if (empleado == null) return BadRequest();
            empleadoDto.Id = empleado.Id;
            return CreatedAtAction(nameof(Post), new { id = empleadoDto.Id }, empleadoDto);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EmpleadoDto>> Put(int id, [FromBody] EmpleadoDto empleadoDto)
        {
            if (empleadoDto == null) return NotFound();
            if (empleadoDto.Id == 0) empleadoDto.Id = id;
            if (empleadoDto.Id != id) return BadRequest();
            var empleado = await _unitOfWork.Empleados.GetByIdAsync(id);
            _mapper.Map(empleadoDto, empleado);
            _unitOfWork.Empleados.Update(empleado);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<EmpleadoDto>(empleado);
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var empleado = await _unitOfWork.Empleados.GetByIdAsync(id);
            if (empleado == null) return NotFound();
            _unitOfWork.Empleados.Remove(empleado);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}