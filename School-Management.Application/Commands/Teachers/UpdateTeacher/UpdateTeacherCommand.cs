using MediatR;
using School_Management.Application.DTO;
using SchoolManagement.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management.Application.Commands.Teachers.UpdateTeacher
{
    public class UpdateTeacherCommand : IRequest<TeacherDto>
    {
        public Guid Id { get; set; }
        public Guid? DepartmentId { get; set; }
        public Guid SubjectId { get; set; }
        public List<Guid> ClassroomIds { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
       

        
    }
}
