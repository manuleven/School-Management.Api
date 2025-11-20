
using MediatR;
using Microsoft.AspNetCore.Mvc;
using School_Management.Application.Commands.Students.CreateStudent;
using School_Management.Application.Commands.Students.DeleteStudent;
using School_Management.Application.Commands.Students.UpdateStudent;
using School_Management.Application.DTO;
using School_Management.Application.Queries.Students.GetAllStudent;
using School_Management.Application.Queries.Students.GetStudentById;

namespace School_Management.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController(IMediator mediator) : ControllerBase
    {
        [HttpPost("create-student")]
        public async Task<ActionResult> CreateStudent([FromBody]AddStudentCommand studentCommand)
        {
            var result = await mediator.Send(studentCommand);
            return Ok(studentCommand);
        }

        [HttpGet("get-student")]
        public async Task<ActionResult<StudentDto>> GetStudent(Guid Id,  CancellationToken cancellationToken)
        {
            var query = new GetStudentByIdQuery(Id);

            var result = await mediator.Send(query, cancellationToken);

            if(result == null)
                return NotFound($"Student with {Id} not found");

            return Ok(result);

        }
        [HttpGet("get-all")]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetAllStudent(CancellationToken cancellationToken)
        {
            var query = new GetAllStudentsQuery();

            var result = await mediator.Send(query, cancellationToken);

            if(result.Count() == 0)
                return NotFound($"No student not found");

            return Ok(result);

        }

        [HttpPut("update-student")]

        public async Task<ActionResult> UpdateStudent([FromBody] UpdateStudentCommand updateStudent) 
        {
            var result = await mediator.Send(updateStudent);
            return Ok(result);
        }

        [HttpDelete("delete-student")]
        public async Task<ActionResult> DeleteStudent(Guid Id, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new DeleteStudentCommand(Id), cancellationToken);
            if(!result)
                return NotFound($"Student with {Id} not found");

            return Ok($"Student with {Id} deleted successfully");
        }
    }
}
