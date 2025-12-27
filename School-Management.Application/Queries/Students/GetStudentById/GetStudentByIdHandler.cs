using MediatR;
using School_Management.Application.DTO;
using School_Management.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace School_Management.Application.Queries.Students.GetStudentById
{
    public class GetStudentByIdHandler(IUnitOfWork repository) : IRequestHandler<GetStudentByIdQuery, StudentDto>
    {
        public async Task<StudentDto> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            var student = await repository.Students.GetByIdAsync(request.Id, cancellationToken);
            if (student == null)
                throw new Exception("Student not found");
          
            var studentDto = new StudentDto
            {
                Id = student.Id,
                FullName = student.FullName.GetFullName(),
                DateOfEnrollment = student.DateOfEnrollment,
                DateOfBirth = student.DateOfBirth,
                PhoneNumber = student.PhoneNumber,
                Address = student.Address,
                State = student.State,
                Classroom = student.Classroom.Name,
                Age = student.Age,
                Subjects = student.Department?.SubjectTaken?.Select(s => s.Value).ToList() ?? student.Classroom?.Subjects?.Select(s => s.Name.Value).ToList(),
                Department = student.Classroom?.Department?.DepartmentName
            };
            return studentDto;
        }
    
    }
}
