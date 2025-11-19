using MediatR;
using School_Management.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management.Application.Commands.Students.DeleteStudent
{
    public class DeleteStudentHandler(IUnitOfWork repository) : IRequestHandler<DeleteStudentCommand, bool>
    {
      public async Task <bool> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await repository.Students.GetByIdAsync(request.Id, cancellationToken);
            if (student == null)
                throw new Exception("Student not found");
            await repository.Students.DeleteAsync(student, cancellationToken);
            await repository.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
