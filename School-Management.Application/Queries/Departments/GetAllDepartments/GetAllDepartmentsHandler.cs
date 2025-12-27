using MediatR;
using Microsoft.EntityFrameworkCore.Migrations;
using School_Management.Application.DTO;
using School_Management.Application.Interfaces;
using SchoolManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management.Application.Queries.Departments.GetAllDepartments
{
    public class GetAllDepartmentsHandler(IUnitOfWork repository) : IRequestHandler<GetAllDepartmentsQuery, IEnumerable<DepartmentDto>>
    {
        public async Task<IEnumerable<DepartmentDto>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
        {
            var check = await repository.Departments.GetAllAsync(cancellationToken);
            if (check == null)
                throw new ArgumentException();

            return check.Select(Departments => new DepartmentDto
            {
                DepartmentCode = Departments.DepartmentCode,
                DepartmentName = Departments.DepartmentName,
                Id = Departments.Id,
                Subjects = Departments.SubjectTaken.Select(c => c.Value).ToList(),
                CreatedBy = Departments.CreatedBy
            }).ToList();
        }
    }
}


//Add - Migration InitialCreate - Project School_Management.Persistence - StartupProject School_Management.Api
//Update-Database -Project School_Management.Persistence -StartupProject School_Management.Api

