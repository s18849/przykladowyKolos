using Microsoft.AspNetCore.Mvc;
using przykladowyKolos.DTOs.Requests;
using przykladowyKolos.DTOs.Responses;
using przykladowyKolos.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace przykladowyKolos.Controllers
{
    [ApiController]
    [Route("api/animals")]
    public class AnimalControler : ControllerBase
    {
        private IAnimalDbService _service;

        public AnimalControler(IAnimalDbService service)
        {
            _service = service;
        }
        [HttpGet]
        public IActionResult GetAnimals(string orderBy)
       
        {
           
            List<GetAnimalResponse> list;
            try
            {
                list = _service.GetAnimal(orderBy);

                return Ok(list);

            }
            catch (SqlException exc)
            {
                return BadRequest("Nie ma takiego pola w bazie");
            }
            
        }

        [HttpPost]
        public IActionResult  AddAnimals(AddAnimalRequest animal) {
            try
            {
                _service.AddAnimal(animal);
            }
            catch (Exception)
            {
                return NotFound("Zadanie nie zawiera wszystkich danych");

            }

            return Ok();
        }
        
    }
}
