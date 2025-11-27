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
    public class GetAllStudentsHandler(IUnitOfWork repository) : IRequestHandler<GetAllStudentsQuery, IEnumerable<StudentDto>>
    {
        public async Task<IEnumerable<StudentDto>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
        {
            var students = await repository.Students.GetAllAsync(cancellationToken);
            if (students == null || students.Count() == 0)
                throw new ArgumentException("No student found");

            return students.Select(student => new StudentDto
            {
                Id = student.Id,
                FullName = student.FullName.GetFullName(),
                State = student.State,
                Address = student.Address,
                Age = student.Age,
                PhoneNumber = student.PhoneNumber,
                DateOfBirth = student.DateOfBirth,
                DateOfEnrollment = student.DateOfEnrollment,
                Department = student.Department.DepartmentName

            }).ToList();
        }
    
    }
}
