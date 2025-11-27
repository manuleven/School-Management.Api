using MediatR;

namespace School_Management.Application.Commands.Subjects.DeleteSubject
{
    public record DeleteSubjectCommand(Guid Id, string? DeletedBy = null) : IRequest<bool>;
    
    
}
