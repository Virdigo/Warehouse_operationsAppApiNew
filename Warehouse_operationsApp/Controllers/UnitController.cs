using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Warehouse_operationsApp.Dto;
using Warehouse_operationsApp.Models;
using Warehouse_operationsApp.Repository;
using Warehouse_operationsApp.Repository.Interfaces;

namespace Warehouse_operationsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitController : Controller
    {
        private readonly IUnitRepository _unitRepository;
        private readonly IMapper _mapper;

        public UnitController(IUnitRepository unitRepository, IMapper mapper)
        {
            _unitRepository = unitRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Unit>))]
        public IActionResult GetUnitsList()
        {
            var unit = _mapper.Map<List<UnitDto>>(_unitRepository.GetUnitsList());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(unit);
        }

        [HttpGet("{UnitId}")]
        [ProducesResponseType(200, Type = typeof(Suppliers))]
        [ProducesResponseType(400)]

        public IActionResult GetSuppliersByID(int UnitId)
        {
            if (!_unitRepository.UnitExists(UnitId))
                return NotFound();

            var supplier = _mapper.Map<UnitDto>(_unitRepository.GetUnitsById(UnitId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(supplier);
        }

        [HttpGet("Unit/{product}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Unit>))]
        [ProducesResponseType(400)]

        public IActionResult GetUnitsByProduct(int product)
        {
            var Unitsss = _mapper.Map<List<UnitDto>>(_unitRepository.GetUnitsByProduct(product));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(Unitsss);
        }

        [HttpPost("POST")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateUnit([FromBody] UnitDto Unit_create)
        {
            if (Unit_create == null)
                return BadRequest(ModelState);

            var UnitCreat = _unitRepository.GetUnitsList()
                .Where(c => c.Name.Trim().ToUpper() == Unit_create.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (UnitCreat != null)
            {
                ModelState.AddModelError("", "Unit already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var UnitMap = _mapper.Map<Unit>(Unit_create);

            if (!_unitRepository.CreateUnit(UnitMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("PUT/{id_unit}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUnit(int id_unit, [FromBody] UnitDto Unit_update)
        {
            if (Unit_update == null)
                return BadRequest(ModelState);

            if (id_unit != Unit_update.id_unit)
                return BadRequest(ModelState);

            if (!_unitRepository.UnitExists(id_unit))
                return BadRequest(new { message = "Error: Invalid Id" });

            if (!ModelState.IsValid)
                return BadRequest();

            var UnitsMap = _mapper.Map<Unit>(Unit_update);

            if (!_unitRepository.UpdateUnit(UnitsMap))
            {
                ModelState.AddModelError("", "Something went wrong updating Units");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("DELETE/{id_unit}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteUnit(int id_unit)
        {
            if (!_unitRepository.UnitExists(id_unit))
            {
                return BadRequest(new { message = "Error: Invalid Id" });
            }

            var id_unit_delete = _unitRepository.GetUnitsById(id_unit);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_unitRepository.DeleteUnit(id_unit_delete))
            {
                ModelState.AddModelError("", "Something went wrong deleting suppliers");
            }

            return NoContent();
        }
    }
}
