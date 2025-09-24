using Microsoft.AspNetCore.Mvc;

namespace Remoting.Controllers 
{
    [ApiController] // This attribute indicates that the class is an API controller
    [Route("api/[controller]")] // This sets the base route for the controller to "api/calculator"
    public class CalculatorController : ControllerBase // Inherit from ControllerBase for API controllers
    {
        [HttpGet("add")] // This action responds to GET requests at "api/calculator/add"
        public ActionResult<int> Add(int a, int b) => Ok(a + b); // Returns the sum of a and b

        [HttpGet("subtract")] // This action responds to GET requests at "api/calculator/subtract"
        public ActionResult<int> Subtract(int a, int b) => Ok(a - b); // Returns the difference of a and b
    }
}