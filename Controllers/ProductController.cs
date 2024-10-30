using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecomm_nike.Dtos.Product;
using ecomm_nike.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace ecomm_nike.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var products = _context.Products.ToList().Select(p => p.ToProductDto());

            return Ok(products);
        }
        [HttpGet("{id}")]
        public IActionResult GetProductById([FromRoute] Guid id)
        {
            var product = _context.Products.Find(id);

            if (product == null)
            {
                return NotFound();
            }
            return Ok(product.ToProductDto());
        }

        [HttpPost]

        public IActionResult CreateProduct([FromBody] CreateProductRequestDto productDto)
        {
            var productModel = productDto.ToProductFromCreateProductDto();
            _context.Products.Add(productModel);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetProductById), new { id = productModel.Id }, productModel.ToProductDto());
        }

    }
}