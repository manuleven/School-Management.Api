using MediatR;
using School_Management.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management.Application.Queries.Subject.GetSubjectById
{
    public record GetSubjectByIdQuery(Guid Id) : IRequest<SubjectDto>;
 
}
