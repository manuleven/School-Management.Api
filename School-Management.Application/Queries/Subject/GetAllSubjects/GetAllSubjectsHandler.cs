using MediatR;
using School_Management.Application.DTO;
using School_Management.Application.Interfaces;
using School_Management.Application.Queries.Subject.GetSAllubjects;

namespace School_Management.Application.Queries.Subject.GetAllSubjects
{
    public class GetAllSubjectsHandler(IUnitOfWork repository) : IRequestHandler<GetAllSubjectsQuery, IEnumerable<SubjectDto>>
    {
        public async Task<IEnumerable<SubjectDto>> Handle(GetAllSubjectsQuery request, CancellationToken cancellationToken)
        {
           var subjects = await repository.Subjects.GetAllSubjects(cancellationToken);

            if(subjects is null || !subjects.Any())
            {
                return Enumerable.Empty<SubjectDto>();
            }

            var result = subjects.Select(subjects=> new SubjectDto
            {

                
                Name = subjects.Name.Value,
                Code = subjects.Code,
                Description = subjects.Description
              
            });

            return result;
        }
    }
}
