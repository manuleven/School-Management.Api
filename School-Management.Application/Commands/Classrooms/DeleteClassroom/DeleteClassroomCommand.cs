using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management.Application.Commands.Classrooms.DeleteClassroom
{
    public record DeleteClassroomCommand(Guid Id) : IRequest<bool>;
    
    
}
