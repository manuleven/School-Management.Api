using MediatR;
using School_Management.Application.DTO;
using School_Management.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management.Application.Queries.Classrooms.GetAllClassrooms
{
    public class GetAllClassroomHandler(IUnitOfWork repository) : IRequestHandler<GetAllClassroomsQuery, List<ClassroomDto>>
    {
        public async Task<List <ClassroomDto>> Handle(GetAllClassroomsQuery query, CancellationToken cancellationToken)
        {
            var check = await repository.Classrooms.GetAllAsync(cancellationToken);
            if (check == null || check.Count() == 0)
                throw new Exception("No classroom found");

            return check.Select(Classrooms => new ClassroomDto
            {
                ClassId = Classrooms.Id,
                Name = Classrooms.Name,
                Capacity = Classrooms.Capacity,
                DepartmentName = Classrooms.Department?.DepartmentName,
                SubjectTaken = Classrooms.DepartmentId.HasValue ? Classrooms.Department!.SubjectTaken
                .Select(s => s.Value).ToList() : Classrooms.Subjects.Select(s => s.Name.Value).ToList(),

            }).ToList();

        }
    }
}
