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
    public class FormaPagoController : BaseControllerApi
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FormaPagoController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<FormaPagoDto>>> Get()
        {
            var formaPago = await _unitOfWork.FormasPagos.GetAllAsync();
            return _mapper.Map<List<FormaPagoDto>>(formaPago);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FormaPagoDto>> Get(int id)
        {
            var formaPago = await _unitOfWork.FormasPagos.GetByIdAsync(id);
            if (formaPago == null) return NotFound();
            return _mapper.Map<FormaPagoDto>(formaPago);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<FormaPago>> Post(FormaPagoDto formaPagoDto)
        {
            var formaPago = _mapper.Map<FormaPago>(formaPagoDto);
            _unitOfWork.FormasPagos.Add(formaPago);
            await _unitOfWork.SaveAsync();
            if (formaPago == null) return BadRequest();
            formaPagoDto.Id = formaPago.Id;
            return CreatedAtAction(nameof(Post), new { id = formaPagoDto.Id }, formaPagoDto);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<FormaPagoDto>> Put(int id, [FromBody] FormaPagoDto formaPagoDto)
        {
            if (formaPagoDto == null) return NotFound();
            if (formaPagoDto.Id == 0) formaPagoDto.Id = id;
            if (formaPagoDto.Id != id) return BadRequest();
            var formaPago = await _unitOfWork.FormasPagos.GetByIdAsync(id);
            _mapper.Map(formaPagoDto, formaPago);
            _unitOfWork.FormasPagos.Update(formaPago);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<FormaPagoDto>(formaPago);
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var formaPago = await _unitOfWork.FormasPagos.GetByIdAsync(id);
            if (formaPago == null) return NotFound();
            _unitOfWork.FormasPagos.Remove(formaPago);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}