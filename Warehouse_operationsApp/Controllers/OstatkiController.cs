using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Warehouse_operationsApp.Dto;
using Warehouse_operationsApp.Dto.ApiDto;
using Warehouse_operationsApp.Models;
using Warehouse_operationsApp.Repository;
using Warehouse_operationsApp.Repository.Interfaces;

namespace Warehouse_operationsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OstatkiController: Controller
    {
        private readonly IOstatkiRepository _ostatkiRepository;
        private readonly IMapper _mapper;

        public OstatkiController(IOstatkiRepository ostatkiRepository, IMapper mapper)
        {
            _ostatkiRepository = ostatkiRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Ostatki>))]
        public IActionResult GetOstatkisList()
        {
            var OstatkiList = _mapper.Map<List<OstatkiApi>>(_ostatkiRepository.GetOstatkisList());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(OstatkiList);
        }

        [HttpGet("{OstatkiId}")]
        [ProducesResponseType(200, Type = typeof(Ostatki))]
        [ProducesResponseType(400)]

        public IActionResult GetOstatkiById(int OstatkiId)
        {
            if (!_ostatkiRepository.OstatkiExists(OstatkiId))
                return NotFound();

            var OstatkiById = _mapper.Map<OstatkiApi>(_ostatkiRepository.GetOstatkiById(OstatkiId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(OstatkiById);
        }

        [HttpGet("Ostatki/{product}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Ostatki>))]
        [ProducesResponseType(400)]

        public IActionResult GetOstatkisOfProduct(int product)
        {
            var OstatkiProduct = _mapper.Map<List<OstatkiDto>>(_ostatkiRepository.GetOstatkiById(product));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(OstatkiProduct);
        }

        [HttpPost("POST")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateOstatki([FromQuery] int id_warehouses, [FromQuery] int ProductId,
             [FromBody] OstatkiDto Ostatki_create)
        {
            if (Ostatki_create == null)
                return BadRequest(ModelState);

            var Ostatki = _ostatkiRepository.GetOstatkisList()
                .Where(c => c.Quantity_Ostatki.ToString().Trim().ToUpper() == Ostatki_create.Quantity_Ostatki.ToString().TrimEnd().ToUpper())
                .FirstOrDefault();

            if (Ostatki != null)
            {
                ModelState.AddModelError("", "Ostatki already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var OstatkiMap = _mapper.Map<Ostatki>(Ostatki_create);

            if (!_ostatkiRepository.CreateOstatki(id_warehouses, ProductId, OstatkiMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("PUT/{id_Ostatki}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateOstatki(int id_Ostatki, [FromQuery] int id_warehouses, [FromQuery] int ProductId,
            [FromBody] OstatkiDto Ostatki_update)
        {
            if (Ostatki_update == null)
                return BadRequest(ModelState);

            if (id_Ostatki != Ostatki_update.id_Ostatki)
                return BadRequest(ModelState);

            if (!_ostatkiRepository.OstatkiExists(id_Ostatki))
                return BadRequest(new { message = "Error: Invalid Id" });

            if (!ModelState.IsValid)
                return BadRequest();

            var OstatkiaboutMap = _mapper.Map<Ostatki>(Ostatki_update);

            if (!_ostatkiRepository.UpdateOstatki(id_warehouses, ProductId, OstatkiaboutMap))
            {
                ModelState.AddModelError("", "Something went wrong updating Ostatki");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("DELETE/{id_Ostatki}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteOstatki(int id_Ostatki)
        {
            if (!_ostatkiRepository.OstatkiExists(id_Ostatki))
            {
                return BadRequest(new { message = "Error: Invalid Id" });
            }

            var DeleteOstatki = _ostatkiRepository.GetOstatkiById(id_Ostatki);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_ostatkiRepository.DeleteOstatki(DeleteOstatki))
            {
                ModelState.AddModelError("", "Something went wrong deleting category");
            }

            return NoContent();
        }
    }
}
