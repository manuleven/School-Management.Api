using MediatR;
using School_Management.Application.Interfaces;
using School_Management.Domain.Entities;
using School_Management.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management.Application.Commands.Subjects.UpdateSubject
{
    public class UpdateSubjectHandler(IUnitOfWork repository) : IRequestHandler<UpdateSubjectCommand, bool>
    {
        public async Task<bool> Handle(UpdateSubjectCommand command, CancellationToken cancellationToken)
        {
            var check= await repository.Subjects.GetSubjectById(command.Id, cancellationToken);
            if (check == null)
                throw new Exception("Subject with Id not found");

            var dept = await repository.Subjects.GetSubjectByName(command.Name.Trim().ToLower(), cancellationToken);
            if (dept == null)
                throw new Exception("Department doesnt exist");

            var newName = new SubjectName(command.Name);
            check.UpdateName(newName, command.ModifiedBy);
            check.UpdateDescription(command.Description);
            check.UpdateCode(command.Code);
            check.ChangeDepartment(command.DepartmentId);

            await repository.Subjects.UpdateAsync(check, cancellationToken);
            await repository.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
