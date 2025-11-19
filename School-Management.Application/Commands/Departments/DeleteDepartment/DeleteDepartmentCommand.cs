using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management.Application.Commands.Departments.DeleteDepartment
{
    public record DeleteDepartmentCommand(Guid Id) : IRequest<bool>;
   
}
