using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management.Application.Commands.Students.DeleteStudent
{
    public record DeleteStudentCommand(Guid Id)  : IRequest<bool>;
    
    

}
