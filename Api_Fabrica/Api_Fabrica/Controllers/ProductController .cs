using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api_Fabrica.Model;
using Api_Fabrica.Services.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace Api_Fabrica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private ILogger _logger;
        private IProductService _service;

        public ProductController(IProductService service, ILogger<ProductController> logger)
        {
            _service = service;
            _logger = logger;
        }


        [HttpGet]
        public ObjectResult Get()
        {
            try
            {
                var Products = _service.GetProducts();
                return Ok(Products);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet("{id}")]
        public ObjectResult Get(int id)
        {
            try
            {
                var Product = _service.GetProductByiD(id);
                return Ok(Product);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ObjectResult Post([FromBody] ProductEntity entity)
        {
            try
            {
                var newEntity = _service.AddProduct(entity);
                return Ok(newEntity);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ObjectResult Put(int id, [FromBody] ProductEntity entity)
        {
            try
            {
                var Product = _service.UpdateProduct(id,entity);
                return Ok(Product);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public ObjectResult Delete(int id)
        {
            try
            {
                var response = _service.DeleteProduct(id);
                return Ok(response);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
