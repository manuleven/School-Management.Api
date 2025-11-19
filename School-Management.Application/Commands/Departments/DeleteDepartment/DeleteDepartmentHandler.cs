using MediatR;
using School_Management.Application.Commands.Teachers.DeleteTeacher;
using School_Management.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management.Application.Commands.Departments.DeleteDepartment
{
    public class DeleteDepartmentHandler(IUnitOfWork repository) : IRequestHandler<DeleteTeacherCommand, bool>
    {
        public async Task<bool> Handle(DeleteTeacherCommand request, CancellationToken cancellationToken)
        {
            var department = await repository.Departments.GetByIdAsync(request.Id, cancellationToken);

            if (department is null)
                throw new KeyNotFoundException($"Department with Id {request.Id} not found.");

            await repository.Departments.DeleteAsync(department, cancellationToken);
            var result = await repository.SaveChangesAsync(cancellationToken);

            return result > 0;
        }

     
    }

}
