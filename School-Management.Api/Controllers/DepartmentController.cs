using MediatR;
using Microsoft.AspNetCore.Mvc;
using School_Management.Application.Commands.AssignSubjectsToClassroom;
using School_Management.Application.Commands.Departments.CreateDepartment;
using School_Management.Application.Commands.Departments.DeleteDepartment;
using School_Management.Application.Commands.Departments.UpdateDepartment;
using School_Management.Application.DTO;
using School_Management.Application.Queries.Departments.GetAllDepartments;
using School_Management.Application.Queries.Departments.GetDepartmentById;

namespace School_Management.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController(IMediator mediator) : ControllerBase
    {
        [HttpPost("create-department")]
        public async Task <IActionResult> CreateDepartment([FromBody]AddDepartmentCommand command)
        {
            var dept = await mediator.Send(command);
            return Ok(dept);
        }

        //[HttpPost("assign-subject")]
        //public async Task <IActionResult> AssignSubject([FromBody]AssignSubjectToClassroomCommand command)
        //{
        //    var subjects = await mediator.Send(command);
        //    return Ok(subjects);
        //}

        [HttpGet("department-by-id")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetDepartmentByIdQuery(id);
            var result = await mediator.Send(query, cancellationToken);

            if(result == null)
            {
                return NotFound($"Department not found with id: {id}");
            }

            return Ok(result);
        }

        [HttpGet("all-departments")]

        public async Task<IActionResult> GetAllDepartments(CancellationToken cancellationToken)
        {
            var query = new GetAllDepartmentsQuery();
            var result = await mediator.Send(query, cancellationToken);
            if (result.Count() == 0)
            {
                return NotFound("No departments found");
            }
            return Ok(result);
        }

        [HttpPut("update-department")]
        public async Task<IActionResult> UpdateDepartment([FromBody] UpdateDepartmentCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);                                                                                        
        }

        [HttpDelete("delete-department")]

        public async Task <IActionResult> DeleteDepartment(Guid id)
        {
            var result = await mediator.Send(new DeleteDepartmentCommand(id));

            if (!result)
                return NotFound($"department not found {id}");

            return NoContent();


        }
    }
}
