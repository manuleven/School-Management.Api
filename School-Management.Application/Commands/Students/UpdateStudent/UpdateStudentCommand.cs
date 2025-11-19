using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management.Application.Commands.Students.UpdateStudent
{
    public record UpdateStudentCommand(
        Guid Id,
        string FirstName,
        string LastName,
        string PhoneNumber,
        string Address,
        DateTime DateOfBirth,
        Guid DepartmentId,
        string State,
        string Department

        ) : IRequest<bool>;

}
