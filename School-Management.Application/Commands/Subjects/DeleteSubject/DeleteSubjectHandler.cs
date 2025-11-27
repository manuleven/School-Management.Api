using MediatR;
using School_Management.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management.Application.Commands.Subjects.DeleteSubject
{
    public class DeleteSubjectHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteSubjectCommand, bool>
    {
        public async Task<bool> Handle(DeleteSubjectCommand request, CancellationToken cancellationToken)
        {
            var subject = await unitOfWork.Subjects.GetSubjectById(request.Id,cancellationToken);
            if (subject == null)
            {
                return false;
            }
            unitOfWork.Subjects.DeleteAsync(subject,cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    
    }
}
