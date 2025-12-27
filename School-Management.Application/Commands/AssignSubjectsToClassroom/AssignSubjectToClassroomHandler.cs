using MediatR;
using School_Management.Application.Interfaces;
using School_Management.Domain.Entities;

namespace School_Management.Application.Commands.AssignSubjectsToClassroom
{
    public class AssignSubjectToClassroomHandler(IUnitOfWork repository) : IRequestHandler<AssignSubjectToClassroomCommand, bool>
    {
        public async Task <bool> Handle(AssignSubjectToClassroomCommand request, CancellationToken cancellationToken)
        {
            var classroom = await repository.Classrooms.GetClassroomById(request.ClassroomId, cancellationToken);
            if (classroom == null)
                throw new Exception("Classroom with Id not found");

            if (request.SubjectId.Distinct().Count() != request.SubjectId.Count())
                throw new Exception("Duplicate Subject found");

            var subject = await repository.Subjects.GetByIdsAsync(request.SubjectId, cancellationToken);
            
                if (subject.Count != request.SubjectId.Count)
                    throw new Exception("Some subjects do not exist");


            if (classroom.DepartmentId.HasValue)
            {
                var department = await repository.Departments.GetByIdAsync(classroom.DepartmentId.Value, cancellationToken);

                    throw new Exception($"{department.DepartmentName}'s already has a fixed subject");
               
            }


            classroom.AssignSubjects(subject);
            await repository.Classrooms.UpdateAsync(classroom, cancellationToken);
            await repository.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
