using MediatR;
using School_Management.Application.Interfaces;

namespace School_Management.Application.Commands.AssignTeacherToClassroom
{
    public class AssignTeacherToClassroomHandler(IUnitOfWork repository) : IRequestHandler<AssignTeacherToClassroomCommand, bool>
    {
        public async Task<bool> Handle(AssignTeacherToClassroomCommand command, CancellationToken cancellationToken)
        {

            var teacher = await repository.Teachers.GetByIdAsync(command.TeacherId, cancellationToken);
            if (teacher == null)
                return false;

            var classrooms = await repository.Classrooms.GetByIdsAsync(command.ClassroomIds, cancellationToken);

            if (classrooms.Count != command.ClassroomIds.Count)
                throw new Exception("Some classroom Ids are invalid");
            // 3️⃣ Validate department compatibility
            foreach (var classroom in classrooms)
            {
                // If classroom has a department, check it matches teacher's department
                if (classroom.DepartmentId.HasValue && classroom.DepartmentId != teacher.DepartmentId)
                {
                    throw new Exception($"Teacher cannot teach classroom '{classroom.Name}' from another department");
                }


                teacher.UpdateClassrooms(classrooms);
                await repository.Teachers.UpdateAsync(teacher, cancellationToken);
                await repository.SaveChangesAsync(cancellationToken);

              
            }
            return true;
        }
    }
}
