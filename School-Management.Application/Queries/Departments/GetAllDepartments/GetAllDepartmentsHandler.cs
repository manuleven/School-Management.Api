using MediatR;
using School_Management.Application.DTO;
using School_Management.Application.Interfaces;
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
            }).ToList();
        }
    }
}
