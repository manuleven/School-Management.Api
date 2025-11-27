using MediatR;
using School_Management.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management.Application.Queries.Subject.GetSAllubjects
{
    public record GetAllSubjectsQuery(): IRequest<IEnumerable<SubjectDto>>;
    
}
