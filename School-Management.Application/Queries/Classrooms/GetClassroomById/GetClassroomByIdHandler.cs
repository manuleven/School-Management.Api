using MediatR;
using School_Management.Application.DTO;
using School_Management.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management.Application.Queries.Classrooms.GetClassroomById
{
    public class GetClassroomByIdHandler(IUnitOfWork repository) : IRequestHandler<GetClassroomByIdQuery, ClassroomDto>
    {
        public async Task<ClassroomDto> Handle(GetClassroomByIdQuery command, CancellationToken cancellationToken)
        {
            var check = await repository.Classrooms.GetClassroomById(command.Id, cancellationToken);
            if (check == null)
                throw new Exception("Classroom not found");
            return new ClassroomDto
            {
                Name = check.Name,
                Capacity = check.Capacity,
                DepartmentName = check.Department.DepartmentName,
            };
        }
    
    }
}
