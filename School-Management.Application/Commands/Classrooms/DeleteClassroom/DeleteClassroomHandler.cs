using MediatR;
using School_Management.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management.Application.Commands.Classrooms.DeleteClassroom
{
    public class DeleteClassroomHandler(IUnitOfWork repository) : IRequestHandler<DeleteClassroomCommand, bool>
    {
        public async Task<bool> Handle(DeleteClassroomCommand command, CancellationToken cancellationToken)
        {
            var check = await repository.Classrooms.GetClassroomById(command.Id, cancellationToken);
            if (check == null)
            {
                throw new NullReferenceException("Classroom with Id not found");
            }
            await repository.Classrooms.DeleteAsync(check, cancellationToken);
            await repository.SaveChangesAsync(cancellationToken);
            return true;

        }
    }
}
