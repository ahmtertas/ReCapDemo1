using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController : Controller
    {
        IRentalService _rentalServices;

        public RentalsController(IRentalService rentalServices)
        {
            _rentalServices = rentalServices;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _rentalServices.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _rentalServices.GetAllByCarId(id);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Data);

        }

        [HttpGet("getbyprimerid")]
        public IActionResult GetByPrimeId(int id)
        {
            var result = _rentalServices.GetById(id);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Data);

        }

        [HttpPost("add")]
        public IActionResult Add(Rental rental)
        {
            var result = _rentalServices.Add(rental);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
