using MediatR;
using School_Management.Application.Interfaces;
using SchoolManagement.Application.DTO;

namespace School_Management.Application.Commands.Teachers.UpdateTeacher
{
    public class UpdateTeacherHandler(IUnitOfWork repository) : IRequestHandler<UpdateTeacherCommand, TeacherDto>
    {
        public async Task<TeacherDto> Handle(UpdateTeacherCommand request, CancellationToken cancellationToken)
        {
            var teacher = await repository.Teachers.GetByIdAsync(request.Id, cancellationToken);
            var department = await repository.Departments.GetByIdAsync(request.DepartmentId, cancellationToken);

            if (teacher == null)
                throw new Exception("teacher not found");
           teacher.UpdateFullName(request.FirstName, request.LastName);
           teacher.UpdateEmail(request.Email);
            teacher.ChangePhoneNumber(request.PhoneNumber);
            department.UpdateDepartmentName(request.Department);

            await repository.Teachers.UpdateAsync(teacher, cancellationToken);
            await repository.SaveChangesAsync(cancellationToken);

            return new TeacherDto
            {
                Id = teacher.Id,
                FullName = teacher.FullName.GetFullName(),
                Email = teacher.Email,
                PhoneNumber = teacher.PhoneNumber,
                Department = department.DepartmentName,
                HireDate = teacher.HireDate,
                IsActive = teacher.IsActive
            };

        }
    }
}
