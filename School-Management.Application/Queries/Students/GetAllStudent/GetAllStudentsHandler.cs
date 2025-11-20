using MediatR;
using School_Management.Application.DTO;
using School_Management.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management.Application.Queries.Students.GetAllStudent
{
    public class GetAllStudentsHandler(IUnitOfWork repository) : IRequestHandler<GetAllStudentsQuery, List<StudentDto>>
    {
        public async Task<List<StudentDto>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
        {
            var students = await repository.Students.GetAllAsync(cancellationToken);
            var studentDtos = new List<StudentDto>();
            foreach (var student in students)
            {
                var department = await repository.Departments.GetByIdAsync(student.DepartmentId, cancellationToken);
                var studentDto = new StudentDto
                {
                    Id = student.Id,
                    FullName = student.FullName.GetFullName(),
                    DateOfBirth = student.DateOfBirth,
                    PhoneNumber = student.PhoneNumber,
                    State = student.State,
                    Address = student.Address,
                    Age = student.Age,
                    DateOfEnrollment = student.DateOfEnrollment,
                    Department = department?.DepartmentName,
                    CreatedBy = student.CreatedBy
                };
                studentDtos.Add(studentDto);
            }
            return studentDtos;
        }
    
    }
}
