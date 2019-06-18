using Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BasicCoreWebApp.Controllers
{
    [Route("api/[controller]")]
    public class StudentsController : Controller
    {
        private readonly IMediator mediator;
        public StudentsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("{id}", Name = "GetStudent")]
        public async Task<ActionResult> Get(int id)
        {
            var student = await this.mediator.Send(new GetStudent(id));
            if (student != null)
            {
                return Ok(student);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Post([FromBody] CreateStudent request)
        {
            if (ModelState.IsValid)
            {
                var student = await this.mediator.Send(request);
                return CreatedAtRoute("GetStudent", new { id = student.Id }, student);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
