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
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Product>))]
        public IActionResult GetProductsList()
        {
            var ProductList = _mapper.Map<List<ProductAPI>>(_productRepository.GetProductsList());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(ProductList);
        }

        [HttpGet("{ProductId}")]
        [ProducesResponseType(200, Type = typeof(Product))]
        [ProducesResponseType(400)]

        public IActionResult GetProductById(int ProductId)
        {
            if (!_productRepository.ProductExists(ProductId))
                return NotFound();

            var ProductById = _mapper.Map<ProductAPI>(_productRepository.GetProductById(ProductId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(ProductById);
        }

        [HttpGet("Ostatki/{product}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Product>))]
        [ProducesResponseType(400)]

        public IActionResult GetInformation_about_documentsByProduct(int id_inf)
        {
            var InfProduct = _mapper.Map<List<ProductsDto>>(_productRepository.GetInformation_about_documentsByProduct(id_inf));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(InfProduct);
        }

        [HttpPost("POST")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateProduct([FromQuery] int id_product_type, [FromQuery] int id_unit, [FromBody] ProductsDto Product_create)
        {
            if (Product_create == null)
                return BadRequest(ModelState);

            var Product = _productRepository.GetProductsList()
                .Where(c => c.Name.Trim().ToUpper() == Product_create.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (Product != null)
            {
                ModelState.AddModelError("", "Product already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ProductMap = _mapper.Map<Product>(Product_create);

            if (!_productRepository.CreateProduct(id_product_type, id_unit, ProductMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("PUT/{id_Product}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateProduct(int id_Product, [FromQuery] int id_product_type, [FromQuery] int id_unit, [FromBody] ProductsDto Product_update)
        {
            if (Product_update == null)
                return BadRequest(ModelState);

            if (id_Product != Product_update.id_Product)
                return BadRequest(ModelState);

            if (!_productRepository.ProductExists(id_Product))
                return BadRequest(new { message = "Error: Invalid Id" });

            if (!ModelState.IsValid)
                return BadRequest();

            var Product_aboutMap = _mapper.Map<Product>(Product_update);

            if (!_productRepository.UpdateProduct(id_product_type, id_unit, Product_aboutMap))
            {
                ModelState.AddModelError("", "Something went wrong updating Information");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("DELETE/{id_Product}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteProduct(int id_Product)
        {
            if (!_productRepository.ProductExists(id_Product))
            {
                return BadRequest(new { message = "Error: Invalid Id" });
            }

            var DeleteDeleteProduct = _productRepository.GetProductById(id_Product);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_productRepository.DeleteProduct(DeleteDeleteProduct))
            {
                ModelState.AddModelError("", "Something went wrong deleting Product");
            }

            return NoContent();
        }
    }
}