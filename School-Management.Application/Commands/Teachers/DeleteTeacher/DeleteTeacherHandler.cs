using MediatR;
using School_Management.Application.Interfaces;
using SchoolManagement.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management.Application.Commands.Teachers.DeleteTeacher
{
    public class DeleteTeacherHandler(IUnitOfWork repository) : IRequestHandler<DeleteTeacherCommand, bool>
    {
        public async Task<bool>Handle(DeleteTeacherCommand request, CancellationToken cancellationToken)
        {
            var teacher = await repository.Teachers.GetByIdAsync(request.Id, cancellationToken);
            if(teacher == null)
            
                return false;
            

            await repository.Teachers.DeleteAsync(teacher,cancellationToken);
            await repository.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
