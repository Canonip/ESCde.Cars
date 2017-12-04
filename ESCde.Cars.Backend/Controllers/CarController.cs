using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ESCde.Cars.Backend.Model;
using Newtonsoft.Json;
using ESCde.Cars.Model;

namespace ESCde.Cars.Backend.Controllers
{
    [Route("api/[controller]")]
    public class CarController : Controller
    {
        // GET api/car
        [HttpGet]
        public IActionResult Get()
        {
            return new OkObjectResult(Database.Instance.Cars);
        }

        // GET api/car/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var source = Database.Instance.Cars;
            var query = from car
                        in source
                        where car.Guid == id
                        select car;
            var foundCar = query.FirstOrDefault();

            if (foundCar == null) return NotFound();

            return new ObjectResult(foundCar);
        }

        // POST api/car
        [HttpPost]
        public IActionResult CreateNewCar([FromBody]Car newCar)
        {
            if (newCar == null) return BadRequest();
            Database.Instance.Cars.Add(newCar);
            return CreatedAtRoute(new { id = newCar.Guid }, newCar);

        }

        // PUT api/car/5
        [HttpPut("{id}")]
        public IActionResult UpdateCar(Guid id, [FromBody]Car car)
        {
            //Put needs complete object, not just deltas
            if (car == null || car.Guid != id || car.Vendor == null || car.Model == null || car.Color == null)
            {
                return BadRequest();
            }

            var updatedCar = Database.Instance.Cars.FirstOrDefault(t => t.Guid == id);
            if (updatedCar == null)
            {
                return NotFound();
            }

            updatedCar.Color = car.Color;
            updatedCar.Model = car.Model;
            updatedCar.Vendor = car.Vendor;
            updatedCar.YearOfManufacture = car.YearOfManufacture;

            return new NoContentResult();
        }
        // PATCH api/car/5
        [HttpPatch("{id}")]
        public IActionResult UpdateCarDeltas(Guid id, [FromBody]Car car)
        {
            if (car == null)
            {
                return BadRequest();
            }

            var updatedCar = Database.Instance.Cars.FirstOrDefault(t => t.Guid == id);
            if (updatedCar == null)
            {
                return NotFound();
            }

            updatedCar.Color = car.Color ?? updatedCar.Color;
            updatedCar.Model = car.Model ?? updatedCar.Model;
            updatedCar.Vendor = car.Vendor ?? updatedCar.Vendor;
            updatedCar.YearOfManufacture = car.YearOfManufacture != 0 ? car.YearOfManufacture : updatedCar.YearOfManufacture;

            return new OkObjectResult(updatedCar);
        }

        // DELETE api/car/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var car = Database.Instance.Cars.FirstOrDefault(t => t.Guid == id);
            if (car == null)
            {
                return NotFound();
            }

            Database.Instance.Cars.Remove(car);
            return new NoContentResult();
        }
    }
}
