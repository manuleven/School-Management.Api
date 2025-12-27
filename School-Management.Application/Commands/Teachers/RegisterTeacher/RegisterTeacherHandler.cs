using MediatR;
using School_Management.Application.Interfaces;
using School_Management.Domain.Entities;
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
                request.SubjectId,
                 request.DepartmentId,
                request.CreatedBy

                );


            Department? dept = null;

            if (request.DepartmentId is Guid deptId)
            {
                dept = await teacherRepository.Departments.GetByIdAsync(deptId, cancellationToken);
                if (dept is null)
                    throw new Exception("Department with id not found");
            }

            var subject = await teacherRepository.Subjects.GetSubjectById(request.SubjectId, cancellationToken);
                if(subject == null)
                throw new ArgumentNullException("Subject with id not found");

          


            await teacherRepository.Teachers.AddAsync(teacher, cancellationToken);
            await teacherRepository.SaveChangesAsync(cancellationToken);
            

            return new TeacherDto
            {
                Id = teacher.Id,
                FullName = teacher.FullName.GetFullName(),
                PhoneNumber = teacher.PhoneNumber,
                Department= dept?.DepartmentName,
                Subject= subject.Name.Value,
                HireDate = teacher.HireDate,
                IsActive = teacher.IsActive,

            };
        }
    }
}
