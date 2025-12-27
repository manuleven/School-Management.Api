using MediatR;
using School_Management.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management.Application.Commands.Students.CreateStudent
{
    public record AddStudentCommand(string FirstName, string LastName, DateTime DateOfBirth, string PhoneNumber, string State, string Address, int Age, Guid ClassroomId, string? createdBy = null)
    : IRequest<StudentDto>;
}
