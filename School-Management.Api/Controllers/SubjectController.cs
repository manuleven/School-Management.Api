
using MediatR;
using Microsoft.AspNetCore.Mvc;
using School_Management.Application.Commands.Subjects.CreateSubject;
using School_Management.Application.Commands.Subjects.UpdateSubject;
using School_Management.Application.DTO;
using School_Management.Application.Queries.Subject.GetSAllubjects;
using School_Management.Application.Queries.Subject.GetSubjectById;

namespace School_Management.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController(IMediator mediator) : ControllerBase
    {
        [HttpPost("create-subject")]
        public async Task<IActionResult> CreateSubject([FromBody] CreateSubjectCommand command, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpPut("update-subject")]

        public async Task<IActionResult> UpdateSubject([FromBody] UpdateSubjectCommand command, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpGet("get-all-subjects")]

        public async Task <IActionResult> GetAllSubjects(CancellationToken cancellationToken)
        {
            var check = await mediator.Send(new GetAllSubjectsQuery(), cancellationToken);
            if (check.Any())
            {
                return Ok(check);
            }

            return NotFound("no record found");
        }

        [HttpGet("get-subject-by-id")]
        public async Task<IActionResult> GetSubjectById(Guid id, CancellationToken cancellationToken)
        {
            var check = await mediator.Send(new GetSubjectByIdQuery(id), cancellationToken);
           
            if (check != null)
            {
                return Ok(check);
            }
            return NotFound("Subject with Id not found");
        }

        [HttpDelete("delete-subject")]
        public async Task<IActionResult> DeleteSubject(Guid id, CancellationToken cancellationToken)
        {
           
            var result = await mediator.Send(new GetSubjectByIdQuery(id), cancellationToken);
            if (result == null)
                return NotFound($"Subject with {id} not found");

            return NoContent();
        }
    }
}
