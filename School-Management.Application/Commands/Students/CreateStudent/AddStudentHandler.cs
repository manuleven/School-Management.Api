using MediatR;
using School_Management.Application.DTO;
using School_Management.Application.Interfaces;
using School_Management.Domain.Entities;
using SchoolManagement.Domain.ValueObjects;

namespace School_Management.Application.Commands.Students.CreateStudent
{
    public class AddStudentHandler(IUnitOfWork repository) : IRequestHandler<AddStudentCommand, StudentDto>
    {
        public async Task<StudentDto> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {

            var fullName = FullName.Create(request.FirstName, request.LastName);

            var student = Student.Create(
                fullName,
                request.DateOfBirth,
                request.PhoneNumber,
                request.State,
                request.Address,
                request.Age,
                request.DepartmentId
                , request.createdBy
            );

            await repository.Students.AddAsync(student, cancellationToken);
            await repository.SaveChangesAsync(cancellationToken);
            var dept = await repository.Departments.GetByIdAsync(request.DepartmentId, cancellationToken);

            return new StudentDto
            {
                FullName = student.FullName.GetFullName(),
                DateOfBirth = student.DateOfBirth,
                PhoneNumber = student.PhoneNumber,
                State = student.State,
                Address = student.Address,
                Age = student.Age,
                DateOfEnrollment = student.DateOfEnrollment,
                Department = dept.DepartmentName,
                CreatedBy = student.CreatedBy,


            };
        }

       
    }
}
