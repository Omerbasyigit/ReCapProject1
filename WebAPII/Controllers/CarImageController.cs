using Business.Abstract;
using Business.Concrete;
using Entity.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace WebAPII.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarImagesController : ControllerBase
    {
        ICarImageService _carImageService;
        
        public CarImagesController(ICarImageService carImageService)
        {
            _carImageService = carImageService;
        }


        [HttpPost("add")]
        public IActionResult Add([FromForm] IFormFile file, [FromForm] int carId)
        {

            file = Request.Form.Files.FirstOrDefault();
            var result = _carImageService.Add(file, new CarImage { CarId=carId});

            if (result.success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("addmultiple")]
        public IActionResult AddMultiple([FromForm] IFormFile[] images, [FromForm] int carId)
        {
            var result =_carImageService.AddMultiple(images, new CarImage { CarId = carId });

            if (result.success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


            [HttpPost("delete")]
        public IActionResult Delete( CarImage carImage)
        {
            var result = _carImageService.Delete(carImage);

            if (result.success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update([FromForm] IFormFile file, [FromForm] CarImage carImage)
        {
            file = Request.Form.Files.FirstOrDefault();
            var result = _carImageService.Update(file, carImage);

            if (result.success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _carImageService.GetAll();

            if (result.success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getbycarid")]
        public IActionResult GetByCarId(int carId)
        {
            var result = _carImageService.GetByCarId(carId);

            if (result.success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

      
    }
}
