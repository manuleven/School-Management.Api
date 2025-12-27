using MediatR;
using School_Management.Application.Commands.Classrooms.UpdateClassroom;
using School_Management.Application.DTO;
using School_Management.Application.Interfaces;
using School_Management.Domain.Entities;

public class UpdateClassroomHandler(IUnitOfWork repo)
    : IRequestHandler<UpdateClassroomCommand, ClassroomDto>
{
    public async Task<ClassroomDto> Handle(UpdateClassroomCommand command, CancellationToken cancellationToken)
    {
        var classroom = await repo.Classrooms.GetClassroomById(command.Id, cancellationToken)
            ?? throw new NullReferenceException("Classroom with Id not found");

        //Department? department = classroom.Department; // keep current department by default

        //if (command.DepartmentId.HasValue) // Only when user wants to change department
        //{
        //    department = await repo.Departments.GetByIdAsync(command.DepartmentId.Value, cancellationToken)
        //        ?? throw new NullReferenceException("New Department not found");


        //}

        string? departmentName = null;
        List<string?> subjectTaken = new List<string?>();

        if (classroom.DepartmentId.HasValue)
        {
            var dept = await repo.Departments.GetByIdAsync(classroom.DepartmentId.Value, cancellationToken);

            departmentName = dept.DepartmentName;
            subjectTaken = dept.SubjectTaken.Select(s => s.Value).ToList();

        }

        classroom.UpdateClassroom(command.Name, classroom.DepartmentId, command.modifiedBy);
        classroom.UpdateCapacity(command.Capacity, command.modifiedBy);

        await repo.Classrooms.UpdateAsync(classroom, cancellationToken);
        await repo.SaveChangesAsync(cancellationToken);

        return new ClassroomDto
        {
            ClassId = classroom.Id,
            Name = classroom.Name,
            Capacity = classroom.Capacity,
            DepartmentName = departmentName,
            SubjectTaken = subjectTaken
        };
    }
}
