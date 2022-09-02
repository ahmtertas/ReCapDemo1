using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        IUserService _userServices;
        public UsersController(IUserService userServices)
        {
            _userServices = userServices;
        }

        [HttpGet("getall")]
        public IActionResult Get()
        {
            var result = _userServices.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id )
        {
            var result = _userServices.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(User user)
        {
            var result = _userServices.Add(user);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
