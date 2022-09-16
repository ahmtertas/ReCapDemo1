using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : Controller
    {
        ICarService _carServices;
        public CarsController(ICarService carService)
        {
            _carServices = carService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result =_carServices.GetAll();
            if (result.Success)
            {
                return Ok(result);
            } 
            return BadRequest(result);
        }
        [HttpGet("getbyid")]
        public IActionResult GetById(int id )
        {
            var result = _carServices.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("Add")]
        public IActionResult Add(Car car)
        {
            var result = _carServices.Add(car);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getdetailsdto")]
        public IActionResult GetDetailsDto()
        {
            Thread.Sleep(5000);

            var result = _carServices.GetCarDetails();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }
    }
}
