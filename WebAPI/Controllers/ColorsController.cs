using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;


namespace WebAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ColorsController : Controller
    {
        IColorService _colorServices;
        public ColorsController(IColorService colorServices)
        {
            _colorServices = colorServices;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _colorServices.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _colorServices.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(Color color)
        {
            var result = _colorServices.Add(color);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}
