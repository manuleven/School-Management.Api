using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management.Application.Commands.AssignSubjectsToClassroom
{
    public record AssignSubjectToClassroomCommand(Guid ClassroomId,  List<Guid>SubjectId) : IRequest<bool>;
    
}
