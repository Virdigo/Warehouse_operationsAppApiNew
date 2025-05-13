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
    public class Information_about_documentsController : Controller
    {
        private readonly IInformation_about_documentsRepository _information_About_DocumentsRepository;
        private readonly IMapper _mapper;

        public Information_about_documentsController(IInformation_about_documentsRepository information_about_documentsRepository, IMapper mapper)
        {
            _information_About_DocumentsRepository = information_about_documentsRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Information_about_documents>))]
        public IActionResult GetInformation_about_documentsList()
        {
            var documents = _information_About_DocumentsRepository.GetInformation_About_DocumentssList();
            var result = _mapper.Map<List<Information_about_documentsApi>>(documents);

            return Ok(result);
        }

        [HttpGet("{id_inf}")]
        [ProducesResponseType(200, Type = typeof(Information_about_documents))]
        [ProducesResponseType(400)]

        public IActionResult GetInformation_about_documentsByID(int id_inf)
        {
            if (!_information_About_DocumentsRepository.Information_about_documentsExists(id_inf))
                return NotFound();

            var document = _information_About_DocumentsRepository.GetInformation_About_DocumentssById(id_inf);
            var result = _mapper.Map<Information_about_documentsApi>(document);

            return Ok(result);
        }

        [HttpGet("Information_about_documents/{product}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Information_about_documents>))]
        [ProducesResponseType(400)]

        public IActionResult GetInformation_About_DocumentssByProduct(int product)
        {
            var producttos = _mapper.Map<List<Information_about_documentsDto>>(_information_About_DocumentsRepository.GetInformation_About_DocumentssById(product));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(producttos);
        }


        [HttpPost("POST")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateInformation_about_documents([FromQuery] int ProductID, [FromQuery] int id_doc, [FromQuery] int id_suppliers, [FromBody] Information_about_documentsDto Information_about_documents_create)
        {
            if (Information_about_documents_create == null)
                return BadRequest(ModelState);

            var Information_about_documents = _information_About_DocumentsRepository.GetInformation_About_DocumentssList()
                .Where(c => c.Quanity.ToString().Trim().ToUpper() == Information_about_documents_create.Quanity.ToString().TrimEnd().ToUpper())
                .FirstOrDefault();

            if (Information_about_documents != null)
            {
                ModelState.AddModelError("", "Information_about_documents already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var Information_about_documentsMap = _mapper.Map<Information_about_documents>(Information_about_documents_create);

            if (!_information_About_DocumentsRepository.CreateInformation_about_documents(ProductID, id_doc, id_suppliers, Information_about_documentsMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("PUT/{id_inf_doc}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateInformation_about_documents(int id_inf_doc, [FromQuery] int ProductID, [FromQuery] int id_doc, [FromQuery] int id_suppliers,
            [FromBody] Information_about_documentsDto Information_about_documents_update)
        {
            if (Information_about_documents_update == null)
                return BadRequest(ModelState);

            if (id_inf_doc != Information_about_documents_update.id_inf_doc)
                return BadRequest(ModelState);

            if (!_information_About_DocumentsRepository.Information_about_documentsExists(id_inf_doc))
                return BadRequest(new { message = "Error: Invalid Id" });

            if (!ModelState.IsValid)
                return BadRequest();

            var Information_aboutMap = _mapper.Map<Information_about_documents>(Information_about_documents_update);

            if (!_information_About_DocumentsRepository.UpdateInformation_about_documents(ProductID, id_doc, id_suppliers, Information_aboutMap))
            {
                ModelState.AddModelError("", "Something went wrong updating Information");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("DELETE/{id_inf_doc}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteInformation_about_documents(int id_inf_doc)
        {
            if (!_information_About_DocumentsRepository.Information_about_documentsExists(id_inf_doc))
            {
                return BadRequest(new { message = "Error: Invalid Id" });
            }

            var DeleteInformation_about_documents = _information_About_DocumentsRepository.GetInformation_About_DocumentssById(id_inf_doc);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_information_About_DocumentsRepository.DeleteInformation_about_documents(DeleteInformation_about_documents))
            {
                ModelState.AddModelError("", "Something went wrong deleting Information_About_Documentss");
            }

            return NoContent();
        }
    }
}
