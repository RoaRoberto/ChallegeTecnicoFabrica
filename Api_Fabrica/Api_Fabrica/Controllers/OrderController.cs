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
    public class OrderController : ControllerBase
    {

        private ILogger _logger;
        private IOrderService _service;

        public OrderController(IOrderService service, ILogger<OrderController> logger)
        {
            _service = service;
            _logger = logger;
        }


        [HttpGet]
        public ObjectResult Get()
        {
            try
            {
                var Orders = _service.GetOrders();
                return Ok(Orders);
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
                var Order = _service.GetOrderByiD(id);
                return Ok(Order);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ObjectResult Post([FromBody] OrderEntity entity)
        {
            try
            {
                var newEntity = _service.AddOrder(entity);
                return Ok(newEntity);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ObjectResult Put(int id, [FromBody] OrderEntity entity)
        {
            try
            {
                var Order = _service.UpdateOrder(id,entity);
                return Ok(Order);
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
                var response = _service.DeleteOrder(id);
                return Ok(response);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
