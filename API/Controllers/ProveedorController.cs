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
    public class ProveedorController : BaseControllerApi
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProveedorController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ProveedorDto>>> Get()
        {
            var proveedor = await _unitOfWork.Proveedores.GetAllAsync();
            return _mapper.Map<List<ProveedorDto>>(proveedor);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProveedorDto>> Get(int id)
        {
            var proveedor = await _unitOfWork.Proveedores.GetByIdAsync(id);
            if (proveedor == null) return NotFound();
            return _mapper.Map<ProveedorDto>(proveedor);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Proveedor>> Post(ProveedorDto proveedorDto)
        {
            var proveedor = _mapper.Map<Proveedor>(proveedorDto);
            _unitOfWork.Proveedores.Add(proveedor);
            await _unitOfWork.SaveAsync();
            if (proveedor == null) return BadRequest();
            proveedorDto.Id = proveedor.Id;
            return CreatedAtAction(nameof(Post), new { id = proveedorDto.Id }, proveedorDto);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProveedorDto>> Put(int id, [FromBody] ProveedorDto proveedorDto)
        {
            if (proveedorDto == null) return NotFound();
            if (proveedorDto.Id == 0) proveedorDto.Id = id;
            if (proveedorDto.Id != id) return BadRequest();
            var proveedor = await _unitOfWork.Proveedores.GetByIdAsync(id);
            _mapper.Map(proveedorDto, proveedor);
            _unitOfWork.Proveedores.Update(proveedor);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<ProveedorDto>(proveedor);
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var proveedor = await _unitOfWork.Proveedores.GetByIdAsync(id);
            if (proveedor == null) return NotFound();
            _unitOfWork.Proveedores.Remove(proveedor);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}