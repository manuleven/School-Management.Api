using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management.Application.Commands.AssignTeacherToClassroom
{
    public record AssignTeacherToClassroomCommand(List<Guid> ClassroomIds,
        Guid TeacherId) : IRequest<bool>;
   
}
