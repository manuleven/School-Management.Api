using MediatR;
using School_Management.Application.DTO;
using School_Management.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management.Application.Queries.Departments.GetDepartmentById
{
    public class GetDepartmentByIdHandler(IUnitOfWork repository) : IRequestHandler<GetDepartmentByIdQuery, DepartmentDto>
    {
        public async Task<DepartmentDto> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {

            var check = await repository.Departments.GetByIdAsync(request.Id, cancellationToken);

            if (check == null)
                throw new Exception("department not found");

            return new DepartmentDto
            {
                Id = check.Id,
                DepartmentCode = check.DepartmentCode,
                DepartmentName = check.DepartmentName,
                CreatedBy = check.CreatedBy,
                Subjects = check.SubjectTaken.Select(c => c.Value).ToList(),

            };

            


        }
    }
}
