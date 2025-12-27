using MediatR;
using School_Management.Application.Interfaces;
using School_Management.Domain.Entities;
using SchoolManagement.Application.DTO;

namespace School_Management.Application.Commands.Teachers.UpdateTeacher
{
    public class UpdateTeacherHandler(IUnitOfWork repository) : IRequestHandler<UpdateTeacherCommand, TeacherDto>
    {
        public async Task<TeacherDto> Handle(UpdateTeacherCommand request, CancellationToken cancellationToken)
        {
            var teacher = await repository.Teachers.GetByIdAsync(request.Id, cancellationToken);


            if (teacher == null)
                throw new Exception("teacher not found");


            var newDepartment = request.DepartmentId.HasValue
     ? await repository.Departments.GetByIdAsync(request.DepartmentId.Value, cancellationToken)
         ?? throw new Exception("Department not found")
     : null;

            var newSubject = await repository.Subjects.GetSubjectById(request.SubjectId, cancellationToken);
            if (newSubject == null)
                throw new ArgumentNullException("Subject with id not found");

            var newClassroom = await repository.Classrooms.GetByIdsAsync(request.ClassroomIds, cancellationToken);
            var classroomnames = newClassroom.Select(c => c.Name).ToList();


            teacher.UpdateFullName(request.FirstName, request.LastName);
            teacher.UpdateEmail(request.Email);
            teacher.ChangePhoneNumber(request.PhoneNumber);
            teacher.ChangeDepartment(newDepartment?.Id);
            teacher.ChangeSubject(newSubject.Id);
            teacher.UpdateClassrooms(newClassroom);




            await repository.Teachers.UpdateAsync(teacher, cancellationToken);
            await repository.SaveChangesAsync(cancellationToken);

            return new TeacherDto
            {
                Id = teacher.Id,
                FullName = teacher.FullName.GetFullName(),
                Email = teacher.Email,
                PhoneNumber = teacher.PhoneNumber,
                Department = newDepartment?.DepartmentName,
                Subject = newSubject.Name.Value,
                TeacherClassrooms = newClassroom.Select(c => c.Name).ToList(),
                HireDate = teacher.HireDate,
                IsActive = teacher.IsActive
            };

        }
    }
}
