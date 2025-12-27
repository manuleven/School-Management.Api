using MediatR;
using School_Management.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management.Application.Commands.Subjects.CreateSubject
{
    public record CreateSubjectCommand(string Name, string Code, string Description,string? CreatedBy= null) :  IRequest<SubjectDto>;
    
}
