using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management.Application.Commands.Departments.UpdateDepartment
{
    public record UpdateDepartmentCommand(Guid Id, string DepartmentCode,string DepartmentName, string ModifiedBy) : IRequest<bool>;
   
}
