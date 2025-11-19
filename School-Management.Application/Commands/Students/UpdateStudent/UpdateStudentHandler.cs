using MediatR;
using School_Management.Application.DTO;
using School_Management.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management.Application.Commands.Students.UpdateStudent
{
    public class UpdateStudentHandler(IUnitOfWork repository) : IRequestHandler<UpdateStudentCommand, bool>
    {
        public async Task<bool> Handle(UpdateStudentCommand command, CancellationToken cancellationToken)
        {
            var student = await repository.Students.GetByIdAsync(command.Id, cancellationToken);
            var department = await repository.Departments.GetByIdAsync(command.DepartmentId, cancellationToken);
            if (student == null)
                throw new Exception("Student not found");

            if (department == null)
                throw new Exception("Department not found");

            student.UpdateFullName(command.FirstName, command.LastName);
                student.ChangePhoneNumber(command.PhoneNumber);
                student.UpdateAddress(command.Address);
                student.UpdateDob(command.DateOfBirth);
                student.UpdateState(command.State);

                department.UpdateDepartmentName(command.Department);

                await repository.Students.UpdateAsync(student, cancellationToken);
               
                await repository.SaveChangesAsync(cancellationToken);

                return true;
            

            
        }
    }
}
