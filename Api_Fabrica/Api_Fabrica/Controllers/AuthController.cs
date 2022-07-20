using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api_Fabrica.Authorization;
using Api_Fabrica.Dto;
using Api_Fabrica.Model;
using Api_Fabrica.Services.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace Api_Fabrica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthController : ControllerBase
    {

        private ILogger _logger;
        private IUserService _service;

        public AuthController(IUserService service, ILogger<UserController> logger)
        {
            _service = service;
            _logger = logger;
        }


        

        [HttpPost]
        [AllowAnonymous]
        public ObjectResult Login([FromBody] AuthDTO authDTO)
        {
            try
            {
                var response = _service.Login(authDTO);
                return Ok(response);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        
        }
    }

