using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Amazon;
using Amazon.EventBridge;
using Amazon.EventBridge.Model;
using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;
using AWS.Messaging.Publishers.SQS;
using ecomm_nike.Dtos.Product;
using ecomm_nike.Mappers;
using ecomm_nike.Models;
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

        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequestDto productDto)
        {
            var productModel = productDto.ToProductFromCreateProductDto();
            _context.Products.Add(productModel);
            await _context.SaveChangesAsync();
            var accesskey = Environment.GetEnvironmentVariable("accesskey");
            var secretkey = Environment.GetEnvironmentVariable("secretkey");
            var queueurl = Environment.GetEnvironmentVariable("queueurl");
            var awsCredentials = new BasicAWSCredentials(accesskey, secretkey);
            var client = new AmazonEventBridgeClient(awsCredentials, RegionEndpoint.EUNorth1);
            var sqsclient = new AmazonSQSClient(awsCredentials, RegionEndpoint.EUNorth1);

            var request = new SendMessageRequest()
            {
                QueueUrl = queueurl,
                MessageBody = JsonSerializer.Serialize(new ProductAddedEvent
                {
                    Id = productModel.Id,
                    Name = productModel.Name,
                    Category = productModel.Category,
                    EntryTime = new DateTime()
                }),
                //DelaySeconds = 10
            };

            await sqsclient.SendMessageAsync(request);


            await client.PutEventsAsync(new PutEventsRequest
            {
                Entries = new List<PutEventsRequestEntry>{
                    new PutEventsRequestEntry{
                        DetailType = "ProductCreated",
                        EventBusName = "ecomm-store",
                        Source = "ecomm-store/moveproductsintoqueue",
                        Detail = JsonSerializer.Serialize( new ProductCreated {
                            Id = productModel.Id,
                            Name = productModel.Name,
                        })
                    }
                }
            });

            return CreatedAtAction(nameof(GetProductById), new { id = productModel.Id }, productModel.ToProductDto());
        }

    }
}