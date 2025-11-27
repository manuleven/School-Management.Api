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
           
            if (student == null)
                throw new Exception("Student not found");

            var newDepartment = await repository.Departments.GetByName(command.Department, cancellationToken);
            if (newDepartment == null)
                throw new Exception("Department doesnt exist");

            student.UpdateFullName(command.FirstName, command.LastName);
                student.ChangePhoneNumber(command.PhoneNumber);
                student.UpdateAddress(command.Address);
                student.UpdateDob(command.DateOfBirth);
                student.UpdateState(command.State);

                student.ChangeDepartment(command.DepartmentId);

                await repository.Students.UpdateAsync(student, cancellationToken);
               
                await repository.SaveChangesAsync(cancellationToken);

                return true;
            

            
        }
    }
}
