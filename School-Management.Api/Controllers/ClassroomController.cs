
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using School_Management.Application.Commands.AssignSubjectsToClassroom;
using School_Management.Application.Commands.AssignTeacherToClassroom;
using School_Management.Application.Commands.Classrooms.CreateClassroom;
using School_Management.Application.Commands.Classrooms.DeleteClassroom;
using School_Management.Application.Commands.Classrooms.UpdateClassroom;
using School_Management.Application.DTO;
using School_Management.Application.Queries.Classrooms.GetAllClassrooms;
using School_Management.Application.Queries.Classrooms.GetClassroomById;
using System.Threading;

namespace School_Management.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassroomController(IMediator mediator) : ControllerBase
    {
        [HttpPost("create-classroom")]
        public async Task<IActionResult> Create([FromBody] CreateClassroomCommand command, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);
            return Ok(result);
        }



        [HttpPut("update-classroom")]
        public async Task<IActionResult> UpdateClassroom([FromBody] UpdateClassroomCommand command, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpPost("assign-subject-to-classroom")]

        public async Task <IActionResult> AssignSubjectToClassroon([FromBody] AssignSubjectToClassroomCommand command, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpGet("get-all-classroom")]
        public async Task<IActionResult> GetAllClassroom(CancellationToken cancellationToken)
        {
            var query = new GetAllClassroomsQuery();

            var result = await mediator.Send(query, cancellationToken);

            if (result.Count() < 1)
            {

                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost("assign-teacher")]
        public async Task<IActionResult> AssignTeacherToClassroom(
      
       [FromBody] AssignTeacherToClassroomCommand request,
       CancellationToken cancellationToken)
        {
            var result = await mediator.Send(
                new AssignTeacherToClassroomCommand(request.ClassroomIds, request.TeacherId),
                cancellationToken
            );

            if (!result)
                return NotFound("Teacher or Classroom not found");

            return Ok("Teacher successfully assigned to classroom");
        }

        [HttpGet("get-classroom-by-id{id:guid}")]
        public async Task<IActionResult> GetClassroomById(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetClassroomByIdQuery(id);
            if (query == null)
            {
                return BadRequest();
            }
            var result = await mediator.Send(query, cancellationToken);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpDelete("delete-classroom{id:guid}")]
        public async Task<IActionResult> DeleteClassroom(Guid id, CancellationToken cancellationToken)
        {
            var command = await mediator.Send(new DeleteClassroomCommand(id));
            if (command == null)
            {
                return BadRequest($"department not found {id}");
            }

            return NoContent();
        }

    }
}
