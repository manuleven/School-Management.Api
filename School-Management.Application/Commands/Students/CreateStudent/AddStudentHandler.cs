using MediatR;
using School_Management.Application.DTO;
using School_Management.Application.Interfaces;
using School_Management.Domain.Entities;
using SchoolManagement.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace School_Management.Application.Commands.Students.CreateStudent
{
    public class AddStudentHandler(IUnitOfWork repository) : IRequestHandler<AddStudentCommand, StudentDto>
    {
        public async Task<StudentDto> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {     

            var classroom = await repository.Classrooms.GetClassroomById(request.ClassroomId, cancellationToken);
            if (classroom == null)
                throw new ArgumentNullException("Classroom not found");

            Guid? departmentId = classroom.DepartmentId;

            if (classroom.Students.Count >= classroom.Capacity)
                throw new ValidationException("Classroom is full");
            var fullName = FullName.Create(request.FirstName, request.LastName);

            var student = Student.Create(
                fullName,
                request.DateOfBirth,
                request.PhoneNumber,
                request.State,
                request.Address,
                request.Age,
                request.ClassroomId,
               departmentId,
                request.createdBy
            );



            classroom.AddStudent(student, request.createdBy);

            await repository.Students.AddAsync(student, cancellationToken);
            await repository.SaveChangesAsync(cancellationToken);


            return new StudentDto
            {
                Id = student.Id,
                FullName = student.FullName.GetFullName(),
                DateOfBirth = student.DateOfBirth,
                PhoneNumber = student.PhoneNumber,
                State = student.State,
                Address = student.Address,
                Age = student.Age,
                DateOfEnrollment = student.DateOfEnrollment,
                Department = classroom.Department?.DepartmentName,
                Classroom = classroom.Name,
                Subjects = classroom.Department?.SubjectTaken?.Select(s => s.Value).ToList() ?? student.Classroom?.Subjects.Select(s => s.Name.Value).ToList(),
                CreatedBy = student.CreatedBy,


            };
        }

       
    }
}
