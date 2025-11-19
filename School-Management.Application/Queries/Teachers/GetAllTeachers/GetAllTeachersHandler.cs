using MediatR;
using School_Management.Application.Interfaces;
using SchoolManagement.Application.DTO;
using SchoolManagement.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management.Application.Queries.Teachers.GetAllTeachers
{
    public class GetAllTeachersHandler(IUnitOfWork repository) : IRequestHandler<GetAllTeachersQuery, IEnumerable<TeacherDto>>
    {

        public async Task<IEnumerable<TeacherDto>> Handle(GetAllTeachersQuery request, CancellationToken cancellationToken)
        {
            var teachers = await repository.Teachers.GetAllAsync(cancellationToken);
            return teachers.Select(teacher => new TeacherDto
            {
                Id = teacher.Id,
                FullName = teacher.FullName.GetFullName(),
                PhoneNumber = teacher.PhoneNumber,
                Department = teacher.Department.CreatedBy,
                HireDate = teacher.HireDate,
                IsActive = teacher.IsActive,
            }).ToList();
        }
    }
}
