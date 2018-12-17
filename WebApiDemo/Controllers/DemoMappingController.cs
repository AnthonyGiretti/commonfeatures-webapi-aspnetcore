using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiDemo.Enum;
using WebApiDemo.Models;

namespace WebApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoMappingController : ControllerBase
    {
        private IMapper _mapper;

        public DemoMappingController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet("getuser")]
        public ActionResult<User> Get()
        {
            var entity = new UserEntity
            {
                Gender = Gender.Mister,
                FirstName = "Anthony",
                LastName = "Giretti",
                Id = "123456789"
            };

            return Ok(_mapper.Map<User>(entity));
        }
    }
}