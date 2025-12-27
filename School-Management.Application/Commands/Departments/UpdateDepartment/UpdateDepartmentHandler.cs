using MediatR;
using School_Management.Application.DTO;
using School_Management.Application.Interfaces;
using School_Management.Domain.ValueObjects;

namespace School_Management.Application.Commands.Departments.UpdateDepartment
{
    public class UpdateDepartmentHandler(IUnitOfWork repository) : IRequestHandler<UpdateDepartmentCommand , bool>
    {
        public async Task <bool> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = await repository.Departments.GetByIdAsync(request.Id, cancellationToken);

            if (department == null)
                throw new Exception("Department not found");

            var check = await repository.Departments.GetByName(request.DepartmentName.ToLower(), cancellationToken);
            if (check != null && check.Id != department.Id)
            {

                throw new Exception("Department already exists.");
            }

            if (request.SubjectTaken == null || !request.SubjectTaken.Any())
                throw new ArgumentException("At least one subject must be selected");

            if (request.SubjectTaken.Any(id => id == Guid.Empty))
                throw new ArgumentException("invalid subject  ID detected");

            var subj = await repository.Subjects.GetByIdsAsync(request.SubjectTaken, cancellationToken);
            if (subj.Count != request.SubjectTaken.Count)
                throw new Exception("Some selected  subject do not exist");



            var subjectNames = subj.Select(s => SubjectName.Create(s.Name.Value)).ToList();

            if (subjectNames.Count < 7 || subjectNames.Count > 9)
                throw new Exception("Department must have 8–9 subjects assigned.");




            department.UpdateDepartmentCode(request.DepartmentCode, request.ModifiedBy);
            department.UpdateDepartmentName(request.DepartmentName, request.ModifiedBy);
            department.UpdateSubjectTaken(subjectNames, request.ModifiedBy);
            

            
             var result = await repository.SaveChangesAsync(cancellationToken) > 0;
            if (!result)
                throw new Exception("Failed to update department");

            return true;
        }
    }
}
