using MediatR;
using School_Management.Application.DTO;
using School_Management.Application.Interfaces;
using School_Management.Domain.Entities;

namespace School_Management.Application.Commands.Classrooms.CreateClassroom
{
    public class CreateClassroomHandler(IUnitOfWork repo) : IRequestHandler<CreateClassroomCommand, ClassroomDto>
    {
        public async Task<ClassroomDto> Handle(CreateClassroomCommand command, CancellationToken cancellationToken)
        {
            var check = await repo.Classrooms.ClassroomExistsAsync(command.Name, cancellationToken);
            if (check)
            {
                throw new ApplicationException("Classroom with the same name already exists");
            }

            
            var classroom = Classroom.Create(
                command.Name,
                command.Capacity,
                command.DepartmentId,
                command.createdBy
            );

            await repo.Classrooms.AddAsync(classroom, cancellationToken);
            await repo.SaveChangesAsync(cancellationToken);

            string? deptName = null;
                 List<string> subjects = new();
            if (command.DepartmentId.HasValue)
            {
                var dept = await repo.Departments.GetByIdAsync(command.DepartmentId.Value, cancellationToken);



                if (dept != null)
                {
                     subjects = dept.SubjectTaken.Select(s => s.Value).ToList() ?? new List<string>();
                    deptName = dept?.DepartmentName;
                }
            }
            return new ClassroomDto
            {
                Name = classroom.Name,
                Capacity = classroom.Capacity,
                ClassId = classroom.Id,
                SubjectTaken = subjects,
                DepartmentName = deptName
            };

        }
    }
}
