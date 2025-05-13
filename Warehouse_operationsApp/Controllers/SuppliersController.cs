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
    public class SuppliersController : Controller
    {
        private readonly ISuppliersRepository _suppliersRepository;
        private readonly IMapper _mapper;

        public SuppliersController(ISuppliersRepository suppliersRepository, IMapper mapper)
        {
            _suppliersRepository = suppliersRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Suppliers>))]
        public IActionResult GetSuppliersList()
        {
            var supplier = _mapper.Map<List<SuppliersDto>>(_suppliersRepository.GetSuppliersList());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(supplier);
        }

        [HttpGet("{suppliersId}")]
        [ProducesResponseType(200, Type = typeof(Suppliers))]
        [ProducesResponseType(400)]

        public IActionResult GetSuppliersByID(int suppliersId)
        {
            if (!_suppliersRepository.SuppliersExists(suppliersId))
                return NotFound();

            var supplier = _mapper.Map<SuppliersDto>(_suppliersRepository.GetSuppliersById(suppliersId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(supplier);
        }

        [HttpGet("/Information_about_documents/{id_inf_doc}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(Suppliers))]
        
        public IActionResult GetSuppliersOfInformation_about_documents(int id_inf_doc)
        {
            var Suppl = _mapper.Map<SuppliersDto>(
                _suppliersRepository.GetSuppliersByInformation_about_documents(id_inf_doc));

            if (!ModelState.IsValid) return BadRequest();

            return Ok(Suppl);
        }

        [HttpPost("POST")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateSuppliers([FromBody] SuppliersDto suppliers_create)
        {
            if (suppliers_create == null)
                return BadRequest(ModelState);

            var suppliersCreat = _suppliersRepository.GetSuppliersList()
                .Where(c => c.Name.Trim().ToUpper() == suppliers_create.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (suppliersCreat != null)
            {
                ModelState.AddModelError("", "suppliers already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var SuppliersMap = _mapper.Map<Suppliers>(suppliers_create);

            if (!_suppliersRepository.CreateSuppliers(SuppliersMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("PUT/{id_suppliers}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateSuppliers(int id_suppliers, [FromBody] SuppliersDto suppliers_update)
        {
            if (suppliers_update == null)
                return BadRequest(ModelState);

            if (id_suppliers != suppliers_update.id_suppliers)
                return BadRequest(ModelState);

            if (!_suppliersRepository.SuppliersExists(id_suppliers))
                return BadRequest(new { message = "Error: Invalid Id" });

            if (!ModelState.IsValid)
                return BadRequest();

            var SuppliersMap = _mapper.Map<Suppliers>(suppliers_update);

            if (!_suppliersRepository.UpdateSuppliers(SuppliersMap))
            {
                ModelState.AddModelError("", "Something went wrong updating Suppliers");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("DELETE/{id_suppliers}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteSuppliers(int id_suppliers)
        {
            if (!_suppliersRepository.SuppliersExists(id_suppliers))
            {
                return BadRequest(new { message = "Error: Invalid Id" });
            }

            var Suppliers_delete = _suppliersRepository.GetSuppliersById(id_suppliers);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_suppliersRepository.DeleteSuppliers(Suppliers_delete))
            {
                ModelState.AddModelError("", "Something went wrong deleting suppliers");
            }

            return NoContent();
        }
    }
}
