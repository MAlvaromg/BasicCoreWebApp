using BasicCoreWebApp.Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BasicCoreWebApp.Controllers
{
    [Route("api/[controller]")]
    public class StudentsController:Controller
    {
        private readonly IMediator mediator;
        public StudentsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet(Name ="GetStudent")]
        public ActionResult<IEnumerable<string>> Get(int id)
        {
            return new string[] { "created", id.ToString() };
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
                return BadRequest();
            }
        }
    }
}
