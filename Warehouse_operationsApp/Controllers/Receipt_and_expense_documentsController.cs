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
    public class Receipt_and_expense_documentsController : Controller
    {
        private readonly IReceipt_and_expense_documentsRepository _receipt_And_Expense_DocumentsRepository;
        private readonly IMapper _mapper;

        public Receipt_and_expense_documentsController(IReceipt_and_expense_documentsRepository receipt_and_expense_documentsRepository, IMapper mapper)
        {
            _receipt_And_Expense_DocumentsRepository = receipt_and_expense_documentsRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Receipt_and_expense_documents>))]
        public IActionResult GetReceipt_and_expense_documentsList()
        {
            var Receipt_and_expense_doc = _mapper.Map<List<Receipt_and_expense_documentsApi>>(
        _receipt_And_Expense_DocumentsRepository.GetReceipt_and_expense_documentsList()
    );

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(Receipt_and_expense_doc);
        }

        [HttpGet("{GetReceipt_and_expense_documentsId}")]
        [ProducesResponseType(200, Type = typeof(Receipt_and_expense_documents))]
        [ProducesResponseType(400)]

        public IActionResult GetReceipt_and_expense_documentsByID(int GetReceipt_and_expense_documentsId)
        {
            if (!_receipt_And_Expense_DocumentsRepository.Receipt_and_expense_documentsExists(GetReceipt_and_expense_documentsId))
                return NotFound();

            var receiptAndExpenseDoc = _mapper.Map<Receipt_and_expense_documentsDto>(_receipt_And_Expense_DocumentsRepository.GetReceipt_and_expense_documentsById(GetReceipt_and_expense_documentsId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(receiptAndExpenseDoc);
        }
        [HttpGet("{GetReceipt_and_expense_documentsId}/Information_about_documents")]
        [ProducesResponseType(200, Type = typeof(Information_about_documents))]
        [ProducesResponseType(400)]

        public IActionResult GetInformation_about_documentsByReceipt_and_expense_documents(int GetReceipt_and_expense_documentsId)
        {
            if (!_receipt_And_Expense_DocumentsRepository.Receipt_and_expense_documentsExists(GetReceipt_and_expense_documentsId))
                return NotFound();

            var inf = _mapper.Map<List<Information_about_documentsDto>>(_receipt_And_Expense_DocumentsRepository.Receipt_and_expense_documentsExists(GetReceipt_and_expense_documentsId));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(inf);
        }

        [HttpPost("POST")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateReceipt_and_expense_documents([FromQuery] int id_users, [FromBody] Receipt_and_expense_documentsDto Receipt_and_expense_documents_create)
        {
            if (Receipt_and_expense_documents_create == null)
                return BadRequest(ModelState);

            var Receipt_and_expense_documents = _receipt_And_Expense_DocumentsRepository.GetReceipt_and_expense_documentsList()
                .Where(c => c.date.ToString().Trim().ToUpper() == Receipt_and_expense_documents_create.date.ToString().TrimEnd().ToUpper())
                .FirstOrDefault();

            if (Receipt_and_expense_documents != null)
            {
                ModelState.AddModelError("", "Receipt_and_expense_documents already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var Receipt_and_expense_documentsMap = _mapper.Map<Receipt_and_expense_documents>(Receipt_and_expense_documents_create);

            if (!_receipt_And_Expense_DocumentsRepository.CreateReceipt_and_expense_documents(id_users, Receipt_and_expense_documentsMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("PUT/{id_doc}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateReceipt_and_expense_documents(int id_doc, [FromQuery] int id_users,
            [FromBody] Receipt_and_expense_documentsDto Receipt_and_expense_documents_update)
        {
            if (Receipt_and_expense_documents_update == null)
                return BadRequest(ModelState);

            if (id_doc != Receipt_and_expense_documents_update.id_doc)
                return BadRequest(ModelState);

            if (!_receipt_And_Expense_DocumentsRepository.Receipt_and_expense_documentsExists(id_doc))
                return BadRequest(new { message = "Error: Invalid Id" });

            if (!ModelState.IsValid)
                return BadRequest();

            var UpdateReceipt_and_expense_documents_aboutMap = _mapper.Map<Receipt_and_expense_documents>(Receipt_and_expense_documents_update);

            if (!_receipt_And_Expense_DocumentsRepository.UpdateReceipt_and_expense_documents(id_users, UpdateReceipt_and_expense_documents_aboutMap))
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
        public IActionResult DeleteReceipt_and_expense_documents(int id_inf_doc)
        {
            if (!_receipt_And_Expense_DocumentsRepository.Receipt_and_expense_documentsExists(id_inf_doc))
            {
                return BadRequest(new { message = "Error: Invalid Id" });
            }

            var DeleteReceipt_and = _receipt_And_Expense_DocumentsRepository.GetReceipt_and_expense_documentsById(id_inf_doc);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_receipt_And_Expense_DocumentsRepository.DeleteReceipt_and_expense_documents(DeleteReceipt_and))
            {
                ModelState.AddModelError("", "Something went wrong deleting category");
            }

            return NoContent();
        }
    }
}
