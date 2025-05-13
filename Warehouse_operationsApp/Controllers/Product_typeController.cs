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
    public class Product_typeController : Controller
    {
        private readonly IProduct_typeRepository _product_TypeRepository;
        private readonly IMapper _mapper;

        public Product_typeController(IProduct_typeRepository product_typeRepository, IMapper mapper)
        {
            _product_TypeRepository = product_typeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Product_type>))]
        public IActionResult GetProduct_typeList()
        {
            var ProductTypes = _mapper.Map<List<Product_typeDto>>(_product_TypeRepository.GetProductTypesList());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(ProductTypes);
        }

        [HttpGet("{product_TypeId}")]
        [ProducesResponseType(200, Type = typeof(Product_type))]
        [ProducesResponseType(400)]

        public IActionResult GetProductTypeByID(int product_TypeId)
        {
            if (!_product_TypeRepository.Product_typeExists(product_TypeId))
                return NotFound();

            var product_Type = _mapper.Map<Product_typeDto>(_product_TypeRepository.GetProductTypeById(product_TypeId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(product_Type);
        }

        [HttpGet("product/{product_TypeId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Product_type>))]
        [ProducesResponseType(400)]

        public IActionResult GetProductsByProduct_type(int product_TypeId)
        {
            var products = _product_TypeRepository.GetProductsByProduct_type(product_TypeId);
            var productDtos = _mapper.Map<List<Product_typeDto>>(products);

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(productDtos);
        }

        [HttpPost("POST")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateProduct_type([FromBody] Product_typeDto Product_type_create)
        {
            if (Product_type_create == null)
                return BadRequest(ModelState);

            var Product_type = _product_TypeRepository.GetProductTypesList()
                .Where(c => c.Name.Trim().ToUpper() == Product_type_create.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (Product_type != null)
            {
                ModelState.AddModelError("", "Product_type already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var Product_typeMap = _mapper.Map<Product_type>(Product_type_create);

            if (!_product_TypeRepository.CreateProduct_type(Product_typeMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("PUT/{id_product_type}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateProduct_type(int id_product_type, [FromBody] Product_typeDto Product_type_update)
        {
            if (Product_type_update == null)
                return BadRequest(ModelState);

            if (id_product_type != Product_type_update.id_product_type)
                return BadRequest(ModelState);

            if (!_product_TypeRepository.Product_typeExists(id_product_type))
                return BadRequest(new { message = "Error: Invalid Id" });

            if (!ModelState.IsValid)
                return BadRequest();

            var Product_typeMap = _mapper.Map<Product_type>(Product_type_update);

            if (!_product_TypeRepository.UpdateProduct_type(Product_typeMap))
            {
                ModelState.AddModelError("", "Something went wrong updating category");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("DELETE/{id_product_type}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteProduct_type(int id_product_type)
        {
            if (!_product_TypeRepository.Product_typeExists(id_product_type))
            {
                return BadRequest(new { message = "Error: Invalid Id" });
            }

            var DeleteProduct_type = _product_TypeRepository.GetProductTypeById(id_product_type);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_product_TypeRepository.DeleteProduct_type(DeleteProduct_type))
            {
                ModelState.AddModelError("", "Something went wrong deleting Product_type");
            }

            return NoContent();
        }
    }
}
