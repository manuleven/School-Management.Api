using MediatR;
using School_Management.Application.DTO;
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
    public class GetAllTeachersHandler(IUnitOfWork repository) : IRequestHandler<GetAllTeachersQuery, List
        <TeacherDto>>
    {

        public async Task<List<TeacherDto>> Handle(GetAllTeachersQuery request, CancellationToken cancellationToken)
        {
            var teachers = await repository.Teachers.GetAllAsync(cancellationToken);
            if (teachers == null || teachers.Count() == 0)
                throw new ArgumentException("No teachers found");

            return teachers.Select(teacher => new TeacherDto
            {
                Id = teacher.Id,
                FullName = teacher.FullName.GetFullName(),
                PhoneNumber = teacher.PhoneNumber,
                Department = teacher.Department?.DepartmentName,
                HireDate = teacher.HireDate,
                Email = teacher.Email,
                Subject = teacher.Subject.Name.Value,
                TeacherClassrooms = teacher.Classrooms.Select(c => c.Name).ToList(),
                IsActive = teacher.IsActive,
            }).ToList();
        }
    }
}
