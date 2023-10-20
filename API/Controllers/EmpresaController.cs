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
    public class EmpresaController : BaseControllerApi
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmpresaController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<EmpresaDto>>> Get()
        {
            var empresa = await _unitOfWork.Empresas.GetAllAsync();
            return _mapper.Map<List<EmpresaDto>>(empresa);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EmpresaDto>> Get(int id)
        {
            var empresa = await _unitOfWork.Empresas.GetByIdAsync(id);
            if (empresa == null) return NotFound();
            return _mapper.Map<EmpresaDto>(empresa);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Empresa>> Post(EmpresaDto empresaDto)
        {
            var empresa = _mapper.Map<Empresa>(empresaDto);
            _unitOfWork.Empresas.Add(empresa);
            await _unitOfWork.SaveAsync();
            if (empresa == null) return BadRequest();
            empresaDto.Id = empresa.Id;
            return CreatedAtAction(nameof(Post), new { id = empresaDto.Id }, empresaDto);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EmpresaDto>> Put(int id, [FromBody] EmpresaDto empresaDto)
        {
            if (empresaDto == null) return NotFound();
            if (empresaDto.Id == 0) empresaDto.Id = id;
            if (empresaDto.Id != id) return BadRequest();
            var empresa = await _unitOfWork.Empresas.GetByIdAsync(id);
            _mapper.Map(empresaDto, empresa);
            _unitOfWork.Empresas.Update(empresa);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<EmpresaDto>(empresa);
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var empresa = await _unitOfWork.Empresas.GetByIdAsync(id);
            if (empresa == null) return NotFound();
            _unitOfWork.Empresas.Remove(empresa);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}