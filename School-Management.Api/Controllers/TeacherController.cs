
using MediatR;
using Microsoft.AspNetCore.Mvc;
using School_Management.Application.Commands.Teachers.DeleteTeacher;
using School_Management.Application.Commands.Teachers.UpdateTeacher;
using School_Management.Application.Queries.Teachers.GetAllTeachers;
using School_Management.Application.Queries.Teachers.GetTeacherById;
using SchoolManagement.Application.Commands.Teachers.RegisterTeacher;
using SchoolManagement.Application.DTO;


namespace School_Management.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController(IMediator mediator) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> RegisterTeacher([FromBody] RegisterTeacherCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("update")]

        public async Task<IActionResult> UpdateTeacher([FromBody] UpdateTeacherCommand request, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(request, cancellationToken);
            return Ok(result);
        }


        [HttpGet("get-by-id{id:guid}")]

        public async Task<IActionResult> GetTeacherById(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetTeacherByIdQuery(id);
            var result = await mediator.Send(query, cancellationToken);

            if (result == null)
            {

                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllTeachers(CancellationToken cancellationToken)
        {
            var query = new GetAllTeachersQuery();
            var result = await mediator.Send(query, cancellationToken);

            if (result.Count()< 0)
            { 

                return NotFound();
            }

            return Ok(result);
        }

        [HttpDelete("{id:guid}")]

        public async Task<IActionResult> DeleteTeacher(Guid id)
        {
            var result = await mediator.Send(new DeleteTeacherCommand(id));

            if(!result)
                return NotFound("$Teacher with ID {id} was not found");

            return NoContent();
        }
    } 
}
