using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RestExercise1.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestExercise1.Controllers
{
    
    //[EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;

        public PersonsController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }


        // GET: api/<PersonsController>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        [HttpGet]
        public IActionResult GetAll(
            [FromQuery] string? nameFilter,
            [FromQuery] int? minAge,
            [FromQuery] int? maxAge,
            [FromQuery] int? minHeight, // Ændret til double? (eller int?)
            [FromQuery] int? maxHeight) // Ændret til double? (eller int?)
        {
            try
            {
                IEnumerable<Person> items = _personRepository.GetAllPersons(nameFilter, minAge, maxAge, minHeight, maxHeight);
                

                
                if (items == null || items.Count()==0)
                {
                    return NoContent(); // Returnerer statuskode 204
                }

                return Ok(items); // Returnerer statuskode 200 med listen
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Returnerer statuskode 400
            }
        }

        // GET api/<PersonsController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")] 
        public ActionResult<Person> Get(int id)
        {
            
                var item = _personRepository.GetPersonById(id);
                if (item == null) return NotFound("No such item, id: " + id);
                
                return Ok(item);
            
            

        }

        // POST api/<PersonsController>
        //[DisableCors]
        
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public ActionResult<Person> Post([FromBody] Person newItem)
        {
            try
            {
                _personRepository.AddPerson(newItem);
                return Created($"api/items/{newItem.Id}", newItem);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // PUT api/<PersonsController>/5
        //[DisableCors]
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public Person Put(int id, [FromBody] Person value)
        {
            return _personRepository.UpdatePerson(id, value);
        }

        // DELETE api/<PersonsController>/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _personRepository.RemovePerson(id);
           
        }
    }
}
