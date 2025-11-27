using MediatR;
using School_Management.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management.Application.Commands.Classrooms.UpdateClassroom
{
    public record UpdateClassroomCommand(Guid Id, string Name,string Department, int Capacity, Guid DepartmentId, string modifiedBy= null) : IRequest<ClassroomDto>
    {
    }
}
