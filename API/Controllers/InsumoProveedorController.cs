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
    public class InsumoProveedorController : BaseControllerApi
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public InsumoProveedorController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<InsumoProveedorDto>>> Get()
        {
            var insumoProveedor = await _unitOfWork.InsumosProveedores.GetAllAsync();
            return _mapper.Map<List<InsumoProveedorDto>>(insumoProveedor);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<InsumoProveedorDto>> Get(int id)
        {
            var insumoProveedor = await _unitOfWork.InsumosProveedores.GetByIdAsync(id);
            if (insumoProveedor == null) return NotFound();
            return _mapper.Map<InsumoProveedorDto>(insumoProveedor);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<InsumoProveedor>> Post(InsumoProveedorDto insumoProveedorDto)
        {
            var insumoProveedor = _mapper.Map<InsumoProveedor>(insumoProveedorDto);
            _unitOfWork.InsumosProveedores.Add(insumoProveedor);
            await _unitOfWork.SaveAsync();
            if (insumoProveedor == null) return BadRequest();
            insumoProveedorDto.Id = insumoProveedor.Id;
            return CreatedAtAction(nameof(Post), new { id = insumoProveedorDto.Id }, insumoProveedorDto);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<InsumoProveedorDto>> Put(int id, [FromBody] InsumoProveedorDto insumoProveedorDto)
        {
            if (insumoProveedorDto == null) return NotFound();
            if (insumoProveedorDto.Id == 0) insumoProveedorDto.Id = id;
            if (insumoProveedorDto.Id != id) return BadRequest();
            var insumoProveedor = await _unitOfWork.InsumosProveedores.GetByIdAsync(id);
            _mapper.Map(insumoProveedorDto, insumoProveedor);
            _unitOfWork.InsumosProveedores.Update(insumoProveedor);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<InsumoProveedorDto>(insumoProveedor);
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var insumoProveedor = await _unitOfWork.InsumosProveedores.GetByIdAsync(id);
            if (insumoProveedor == null) return NotFound();
            _unitOfWork.InsumosProveedores.Remove(insumoProveedor);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}