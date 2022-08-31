using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class BrandsController : Controller
    {
        IBrandService _brandServices;

        public BrandsController(IBrandService brandServices)
        {
            _brandServices = brandServices;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var result = _brandServices.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _brandServices.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(Brand brand)
        {
            var result = _brandServices.Add(brand);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }
    }
}
