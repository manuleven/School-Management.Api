using MediatR;
using School_Management.Application.Interfaces;
using SchoolManagement.Application.DTO;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.ValueObjects;

namespace SchoolManagement.Application.Commands.Teachers.RegisterTeacher
{
    public class RegisterTeacherHandler(IUnitOfWork teacherRepository) : IRequestHandler<RegisterTeacherCommand, TeacherDto>
    {
        public async Task<TeacherDto> Handle(RegisterTeacherCommand request,CancellationToken cancellationToken)
        {
            var fullName = FullName.Create(request.FirstName, request.LastName);

            var teacher = Teacher.Create(
                fullName,
                request.Email,
                request.PhoneNumber,
                request.DepartmentId,
                request.CreatedBy

                );

            await teacherRepository.Teachers.AddAsync(teacher, cancellationToken);
            await teacherRepository.SaveChangesAsync(cancellationToken);
            var dept = await teacherRepository.Departments.GetByIdAsync(request.DepartmentId,cancellationToken);

            return new TeacherDto
            {
                Id = teacher.Id,
                FullName = teacher.FullName.GetFullName(),
                PhoneNumber = teacher.PhoneNumber,
                Department= dept.DepartmentName,
                HireDate = teacher.HireDate,
                IsActive = teacher.IsActive,

            };
        }
    }
}
