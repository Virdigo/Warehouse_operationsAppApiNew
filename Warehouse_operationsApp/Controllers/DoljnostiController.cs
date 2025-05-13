﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Warehouse_operationsApp.Data;
using Warehouse_operationsApp.Dto;
using Warehouse_operationsApp.Models;
using Warehouse_operationsApp.Repository.Interfaces;

namespace Warehouse_operationsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoljnostiController : Controller
    {
        private readonly IDoljnostiRepository _doljnostiRepository;
        private readonly IMapper _mapper;

        public DoljnostiController(IDoljnostiRepository doljnostiRepository, IMapper mapper)
        {
            _doljnostiRepository = doljnostiRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Doljnosti>))]
        public IActionResult GetDoljnostiList()
        {
            var doljnosti = _mapper.Map<List<DoljnostiDto>>(_doljnostiRepository.GetDoljnostisList());

            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            return Ok(doljnosti);
        }

        [HttpGet("{doljnosId}")]
        [ProducesResponseType(200, Type = typeof(Doljnosti))]
        [ProducesResponseType(400)]

        public IActionResult GetDoljnostiByID(int doljnosId)
        {
            if (!_doljnostiRepository.DoljnostiExists(doljnosId))
                return NotFound();

            var doljnosti = _mapper.Map<DoljnostiDto>(_doljnostiRepository.GetDoljnostiById(doljnosId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(doljnosti);
        }

        [HttpGet("{doljnosId}/FIO")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400)]

        public IActionResult GetDoljnostiid_users(int doljnosId)
        {
            if(!_doljnostiRepository.DoljnostiExists(doljnosId))
                return NotFound();

            var FIO = _doljnostiRepository.GetDoljnostiById(doljnosId);

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(FIO);
        }

        [HttpPost("POST")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateDoljnosti([FromBody] DoljnostiDto doljnosti_create)
        {
            if (doljnosti_create == null)
                return BadRequest(ModelState);

            var doljnosti = _doljnostiRepository.GetDoljnostisList()
                .Where(c => c.Post.Trim().ToUpper() == doljnosti_create.Post.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (doljnosti != null)
            {
                ModelState.AddModelError("", "doljnosti already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var DoljnostisMap = _mapper.Map<Doljnosti>(doljnosti_create);

            if (!_doljnostiRepository.CreateDoljnosti(DoljnostisMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("PUT/{id_doljnosti}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateDoljnosti(int id_doljnosti, [FromBody] DoljnostiDto doljnosti_update)
        {
            if (doljnosti_update == null)
                return BadRequest(ModelState);

            if (id_doljnosti != doljnosti_update.id_doljnosti)
                return BadRequest(ModelState);

            if (!_doljnostiRepository.DoljnostiExists(id_doljnosti))
                return BadRequest(new { message = "Error: Invalid Id" });

            if (!ModelState.IsValid)
                return BadRequest();

            var DoljnostiMap = _mapper.Map<Doljnosti>(doljnosti_update);

            if (!_doljnostiRepository.UpdateDoljnosti(DoljnostiMap))
            {
                ModelState.AddModelError("", "Something went wrong updating category");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("DELETE/{id_doljnosti}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteDoljnosti(int id_doljnosti)
        {
            if (!_doljnostiRepository.DoljnostiExists(id_doljnosti))
            {
                return BadRequest(new { message = "Error: Invalid Id" });
            }

            var Delete_Doljnosti = _doljnostiRepository.GetDoljnostiById(id_doljnosti);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_doljnostiRepository.DeleteDoljnosti(Delete_Doljnosti))
            {
                ModelState.AddModelError("", "Something went wrong deleting category");
            }

            return NoContent();
        }
    }
}
