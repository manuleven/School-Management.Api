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

namespace School_Management.Application.Queries.Teachers.GetTeacherById
{
    public class GetTeacherByIdHandler(IUnitOfWork repository) : IRequestHandler<GetTeacherByIdQuery, TeacherDto>
    {
        public async Task<TeacherDto> Handle(GetTeacherByIdQuery request,CancellationToken cancellationToken)
        {
            var teacher = await repository.Teachers.GetByIdAsync(request.Id, cancellationToken);
            if(teacher == null)
               return new TeacherDto();

            return new TeacherDto()
            {
                Id = teacher.Id,
                FullName = teacher.FullName.GetFullName(),
                Email = teacher.Email,
                PhoneNumber = teacher.PhoneNumber,
                Subject = teacher.Subject.Name.Value,
                TeacherClassrooms = teacher.Classrooms.Select(c => c.Name).ToList(),
                Department = teacher.Department?.DepartmentName,
                HireDate = teacher.HireDate,
                IsActive = teacher.IsActive,

            };
        }
    }
}
