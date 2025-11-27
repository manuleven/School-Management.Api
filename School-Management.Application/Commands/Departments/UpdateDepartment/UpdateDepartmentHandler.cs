using MediatR;
using School_Management.Application.DTO;
using School_Management.Application.Interfaces;

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
            if (check != null)
            {

                throw new Exception("Department already exists.");
            }

            department.UpdateDepartmentCode(request.DepartmentCode);
            department.UpdateDepartmentName(request.DepartmentName);
            

            await repository.Departments.UpdateAsync(department, cancellationToken);
             var result = await repository.SaveChangesAsync(cancellationToken) > 0;
            if (!result)
                throw new Exception("Failed to update department");

            return true;
        }
    }
}
