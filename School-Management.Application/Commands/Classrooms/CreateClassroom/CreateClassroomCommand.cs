using MediatR;
using School_Management.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management.Application.Commands.Classrooms.CreateClassroom
{
    public record CreateClassroomCommand(string Name, int Capacity, Guid DepartmentId, string? createdBy = null) : IRequest<ClassroomDto>;
   
}
