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
    public class ClienteController : BaseControllerApi
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ClienteController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ClienteDto>>> Get()
        {
            var cliente = await _unitOfWork.Clientes.GetAllAsync();
            return _mapper.Map<List<ClienteDto>>(cliente);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ClienteDto>> Get(int id)
        {
            var cliente = await _unitOfWork.Clientes.GetByIdAsync(id);
            if (cliente == null) return NotFound();
            return _mapper.Map<ClienteDto>(cliente);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Cliente>> Post(ClienteDto clienteDto)
        {
            var cliente = _mapper.Map<Cliente>(clienteDto);
            _unitOfWork.Clientes.Add(cliente);
            await _unitOfWork.SaveAsync();
            if (cliente == null) return BadRequest();
            clienteDto.Id = cliente.Id;
            return CreatedAtAction(nameof(Post), new { id = clienteDto.Id }, clienteDto);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ClienteDto>> Put(int id, [FromBody] ClienteDto clienteDto)
        {
            if (clienteDto == null) return NotFound();
            if (clienteDto.Id == 0) clienteDto.Id = id;
            if (clienteDto.Id != id) return BadRequest();
            var cliente = await _unitOfWork.Clientes.GetByIdAsync(id);
            _mapper.Map(clienteDto, cliente);
            _unitOfWork.Clientes.Update(cliente);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<ClienteDto>(cliente);
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var cliente = await _unitOfWork.Clientes.GetByIdAsync(id);
            if (cliente == null) return NotFound();
            _unitOfWork.Clientes.Remove(cliente);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}