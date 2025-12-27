using MediatR;
using School_Management.Application.DTO;
using School_Management.Application.Interfaces;
using School_Management.Domain.Entities;

namespace School_Management.Application.Queries.Students.GetAllStudent
{
    public class GetAllStudentsHandler(IUnitOfWork repository) : IRequestHandler<GetAllStudentsQuery, IEnumerable<StudentDto>>
    {
        public async Task<IEnumerable<StudentDto>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
        {
            var students = await repository.Students.GetAllAsync(cancellationToken);
            if (students == null || !students.Any())
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
                Classroom = student.Classroom.Name,
                Subjects = student.Classroom.Department != null
                    ? student.Classroom.Department.SubjectTaken.Select(s => s.Value).ToList()
                    : student.Classroom.Subjects.Select(s => s.Name.Value).ToList(),
                DateOfEnrollment = student.DateOfEnrollment,
                Department = student.Classroom?.Department?.DepartmentName
            }).ToList();
        }
    }
}
