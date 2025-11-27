using MediatR;
using School_Management.Application.DTO;
using School_Management.Application.Interfaces;
using School_Management.Domain.Entities;

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

            var dept = Department.Create(request.DepartmentCode, request.DepartmentName, request.CreatedBy);
           

            await unitOfWork.Departments.AddAsync(dept, cancellationToken);
            await unitOfWork.SaveChangesAsync();

            return new DepartmentDto
            {
                Id = dept.Id,
                DepartmentCode = dept.DepartmentCode,
                DepartmentName = dept.CreatedBy,
                CreatedBy = dept.CreatedBy,
                
            };
        }
    }
}
