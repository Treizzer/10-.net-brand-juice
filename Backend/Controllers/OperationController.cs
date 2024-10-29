using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class OperationController: ControllerBase {

        // Already works
        /*
        [HttpGet]
        public decimal add (decimal a, decimal b) {
            return a + b;
        }
        */

        [HttpGet] // Get something
        public ActionResult<decimal> get ([FromQuery] decimal a, [FromQuery] decimal b) {
            return Ok(a + b);
        }

        [HttpPost]
        // public decimal add (decimal a, decimal b, Numbers numbers) {
        public decimal add (Numbers numbers, [FromHeader] string host,
            [FromHeader(Name = "Content-Length")] string contentLength,
            [FromHeader(Name = "X-Some")] string some) 
        {
            /*
            Console.WriteLine(host);
            Console.WriteLine(contentLength);
            Console.WriteLine(some);
            */
            return numbers.A - numbers.B;
        }

        [HttpPut]
        public decimal edit (decimal a, decimal b) {
            return a * b;
        }

        [HttpDelete]
        public decimal delete (decimal a, decimal b) {
            return a / b;
        }

    }

    public class Numbers { 
    
        public decimal A { get; set; }
        
        public decimal B { get; set; }

    }

}
