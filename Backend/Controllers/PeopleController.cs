using Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController: ControllerBase {

        private IPeopleService _peopleService;

        // Inyección de dependencias / Dependences injection
        public PeopleController ([FromKeyedServices("people2Service")]IPeopleService peopleService) {
            _peopleService = peopleService;
        }

        [HttpGet("all")]
        public List<People>? getPeople () => Repository.People;

        [HttpGet("{id}")]
        public ActionResult<People?> get (int id) {
            var people = Repository.People?.FirstOrDefault(p => p.Id == id);
            
            if (people == null) {
                return NotFound();
            }

            return Ok(people);
        }

        [HttpGet("search/{search}")]
        public List<People>? get (string search) =>
            Repository.People?.Where(p => p.Name.ToUpper()
            .Contains(search.ToUpper())).ToList();

        [HttpPost]
        public IActionResult add (People people) { 
            if (!_peopleService.validate(people)) {
                return BadRequest();
            }

            Repository.People?.Add(people);

            return NoContent();
        }

    }

    public class Repository {

        public static List<People>? People = new List<People>() {
            new People() {
                Id = 1, Name = "Hugo", Birthdate = new DateTime(1990, 12, 3)
            },
            new People() {
                Id = 2, Name = "Paco", Birthdate = new DateTime(1992, 11, 3)
            },
            new People() {
                Id = 3, Name = "Luis", Birthdate = new DateTime(1985, 1, 8)
            },
            new People() { 
                Id = 4, Name = "Marisol", Birthdate = new DateTime(1997, 1, 30)
            }
        };
    
    }

    public class People { 
    
        public int Id { get; set; }
        
        public string? Name { get; set; }

        public DateTime Birthdate { get; set; }
    
    }

}
