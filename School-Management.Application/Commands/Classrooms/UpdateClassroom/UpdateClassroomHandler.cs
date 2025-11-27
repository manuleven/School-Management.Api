using MediatR;
using School_Management.Application.Commands.Departments.UpdateDepartment;
using School_Management.Application.DTO;
using School_Management.Application.Interfaces;
using School_Management.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management.Application.Commands.Classrooms.UpdateClassroom
{
    public class UpdateClassroomHandler(IUnitOfWork repo) : IRequestHandler<UpdateClassroomCommand, ClassroomDto>
    {
        public async Task<ClassroomDto> Handle (UpdateClassroomCommand command, CancellationToken cancellationToken)
        {
            var check = await repo.Classrooms.GetClassroomById(command.Id, cancellationToken);
            if (check == null)
                throw new NullReferenceException("Classroom with Id not found");

            var newDepartment = await repo.Departments.GetByName(command.Department, cancellationToken);

            if(newDepartment == null)
                throw new NullReferenceException("Department with the given name not found");

            check.UpdateClassroom(command.Name, newDepartment.Id, command.modifiedBy);
            check.UpdateCapacity(command.Capacity, command.modifiedBy);

            await repo.Classrooms.UpdateAsync(check, cancellationToken);
            await repo.SaveChangesAsync(cancellationToken);

            return new ClassroomDto
                            {
                Name = check.Name,
                Capacity = check.Capacity,
                ClassId = check.Id,
                DepartmentName = newDepartment.DepartmentName
            };

        }
    }
}
