using MediatR;
using School_Management.Application.DTO;
using School_Management.Application.Interfaces;
using School_Management.Domain.Entities;
using School_Management.Domain.ValueObjects;

namespace School_Management.Application.Commands.Departments.CreateDepartment
{
    public record AddDepartmentHandler(IUnitOfWork unitOfWork) : IRequestHandler<AddDepartmentCommand, DepartmentDto>
    {
        public async Task<DepartmentDto> Handle(AddDepartmentCommand request, CancellationToken cancellationToken)
        {
            var check = await unitOfWork.Departments.GetByName(request.DepartmentName.ToLower(), cancellationToken);
            if (check != null)
            {
                
                throw new Exception("Department already exists.");
            }

            if (string.IsNullOrWhiteSpace(request.DepartmentName))
                throw new ArgumentException("DepartmentName is required");


            if (request.Subjects.Distinct().Count() != request.Subjects.Count())
                throw new Exception("Duplicate Subject found");

            var subjects = await unitOfWork.Subjects.GetByIdsAsync(request.Subjects, cancellationToken);

            if (subjects.Count != request.Subjects.Count)
                throw new Exception("Some subjects do not exist");

            if (subjects.Count() <= 7 || subjects.Count() > 9)
                throw new Exception("Department must have 8–9 subjects.");


            var subjectNames = subjects.Select(s => SubjectName.Create(s.Name.Value)).ToList();

            var dept = Department.Create(request.DepartmentCode, request.DepartmentName,subjectNames, request.CreatedBy);
           

            await unitOfWork.Departments.AddAsync(dept, cancellationToken);
            await unitOfWork.SaveChangesAsync();

            return new DepartmentDto
            {
                Id = dept.Id,
                DepartmentCode = dept.DepartmentCode,
                DepartmentName = dept.DepartmentName,
                Subjects = dept.SubjectTaken.Select(s => s.Value).ToList(),
                CreatedBy = dept.CreatedBy,
                
            };
        }
    }
}
