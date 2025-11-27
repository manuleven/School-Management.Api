using MediatR;
using SchoolManagement.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management.Application.Queries.Teachers.GetAllTeachers
{
    public record GetAllTeachersQuery() : IRequest<IEnumerable<TeacherDto>>;
   
}
