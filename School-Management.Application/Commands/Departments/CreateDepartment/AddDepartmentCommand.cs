using MediatR;
using School_Management.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management.Application.Commands.Departments.CreateDepartment
{
    public record AddDepartmentCommand(string DepartmentCode, string DepartmentName, string CreatedBy) : IRequest<DepartmentDto>;
}