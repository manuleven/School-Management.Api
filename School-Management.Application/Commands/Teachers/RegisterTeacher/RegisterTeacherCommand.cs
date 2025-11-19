using MediatR;
using SchoolManagement.Application.DTO;

namespace SchoolManagement.Application.Commands.Teachers.RegisterTeacher
{
    public record RegisterTeacherCommand(
        string FirstName,
        string LastName,
        string Email,
        string PhoneNumber,
        Guid DepartmentId,
        string? CreatedBy= null

        ) : IRequest<TeacherDto>;
    
}
