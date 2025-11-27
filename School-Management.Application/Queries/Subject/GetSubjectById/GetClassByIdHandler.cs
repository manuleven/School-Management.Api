using MediatR;
using School_Management.Application.DTO;
using School_Management.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management.Application.Queries.Subject.GetSubjectById
{
    public class GetClassByIdHandler(IUnitOfWork repository) :  IRequestHandler<GetSubjectByIdQuery, SubjectDto>
    {
        public async Task <SubjectDto> Handle(GetSubjectByIdQuery query, CancellationToken cancellationToken)
        {
            var check = await repository.Subjects.GetSubjectById(query.Id, cancellationToken);
            if (check == null)
                throw new KeyNotFoundException($"Subject with Id {query.Id} not found");    

            return new SubjectDto
            {
                Description = check.Description,
                Name = check.Name.ToString(),
                Code = check.Code,
                DepartmentId = check.DepartmentId,
                DepartmentName = check.Department.DepartmentName,
            };
        }
    }
}
