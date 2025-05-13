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
    public class WarehousesController : Controller
    {
        private readonly IWarehousesRepository _warehousesRepository;
        private readonly IMapper _mapper;

        public WarehousesController(IWarehousesRepository warehousesRepository, IMapper mapper)
        {
            _warehousesRepository = warehousesRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Warehouses>))]
        public IActionResult GetWarehousesList()
        {
            var warehouses = _warehousesRepository.GetWarehousesList();
            var warehousesApi = _mapper.Map<List<WarehousesApi>>(warehouses);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(warehousesApi);
        }

        [HttpGet("{WarehousesId}")]
        [ProducesResponseType(200, Type = typeof(Warehouses))]
        [ProducesResponseType(400)]

        public IActionResult GetWarehousesById(int WarehousesId)
        {
            if (!_warehousesRepository.WarehousesExists(WarehousesId))
                return NotFound();

            var warehouse = _warehousesRepository.GetWarehousesById(WarehousesId);
            var warehouseDto = _mapper.Map<WarehousesApi>(warehouse);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(warehouseDto);
        }

        [HttpGet("Users/{Warehouses}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Warehouses>))]
        [ProducesResponseType(400)]

        public IActionResult GetUsersByWarehouses(int User_id)
        {
            var doljnostiByUser = _mapper.Map<List<WarehousesDto>>(_warehousesRepository.GetUsersByWarehouses(User_id));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(doljnostiByUser);
        }

        [HttpPost("POST")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateWarehouses([FromQuery] int id_users, [FromBody] WarehousesDto Warehouses_create)
        {
            if (Warehouses_create == null)
                return BadRequest(ModelState);

            var Warehousests = _warehousesRepository.GetWarehousesList()
                .Where(c => c.Name.Trim().ToUpper() == Warehouses_create.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (Warehousests != null)
            {
                ModelState.AddModelError("", "Warehouse already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var WarehouseMap = _mapper.Map<Warehouses>(Warehouses_create);

            if (!_warehousesRepository.CreateWarehouses(id_users, WarehouseMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("PUT/{id_warehouses}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateWarehouses(int id_warehouses, [FromQuery] int id_users, 
            [FromBody] WarehousesDto Warehouses_update)
        {
            if (Warehouses_update == null)
                return BadRequest(ModelState);

            if (id_warehouses != Warehouses_update.id_warehouses)
                return BadRequest(ModelState);

            if (!_warehousesRepository.WarehousesExists(id_warehouses))
                return BadRequest(new { message = "Error: Invalid Id" });

            if (!ModelState.IsValid)
                return BadRequest();

            var Warehouses_aboutMap = _mapper.Map<Warehouses>(Warehouses_update);

            if (!_warehousesRepository.UpdateWarehouses(id_users, Warehouses_aboutMap))
            {
                ModelState.AddModelError("", "Something went wrong updating Warehouse");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("DELETE/{id_warehouses}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteWarehouses(int id_warehouses)
        {
            if (!_warehousesRepository.WarehousesExists(id_warehouses))
            {
                return BadRequest(new { message = "Error: Invalid Id" });
            }

            var DeleteWarehouses = _warehousesRepository.GetWarehousesById(id_warehouses);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_warehousesRepository.DeleteWarehouses(DeleteWarehouses))
            {
                ModelState.AddModelError("", "Something went wrong deleting Warehouses");
            }

            return NoContent();
        }
    }
}
