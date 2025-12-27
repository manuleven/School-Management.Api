using MediatR;
using Microsoft.AspNetCore.Mvc.Filters;
using School_Management.Application.DTO;
using School_Management.Application.Interfaces;
using School_Management.Domain.Entities;
using School_Management.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management.Application.Commands.Subjects.CreateSubject
{
    public class CreateSubjectHandler(IUnitOfWork repository) : IRequestHandler<CreateSubjectCommand, SubjectDto>
    {
        public async Task<SubjectDto> Handle(CreateSubjectCommand request, CancellationToken cancellationToken)
        {
            var check = await repository.Subjects.GetSubjectByName(request.Name, cancellationToken);

            if (check != null)
                throw new Exception("Subject already exist");


            var name = SubjectName.Create(request.Name);
            var subject = Subject.Create(name, request.Description, request.Code, request.CreatedBy);

            await repository.Subjects.AddAsync(subject, cancellationToken);
            var result = await repository.SaveChangesAsync(cancellationToken);
            if(result == 0)
            {
                throw new Exception("Failed to create subject");
            }

            return new SubjectDto
            {
                Description = subject.Description,
                Code = subject.Code,
                Name = subject.Name.Value
                
               
            };
        }
    }
}