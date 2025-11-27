using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management.Application.Commands.Subjects.UpdateSubject
{
    public record UpdateSubjectCommand(Guid Id, string Name, string Code, string Description, Guid DepartmentId, string? ModifiedBy = null) : IRequest<bool>;

}
