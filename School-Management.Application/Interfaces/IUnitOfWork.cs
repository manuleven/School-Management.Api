using School_Management.Persistence.Repositories;
using SchoolManagement.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management.Application.Interfaces
{
    public interface IUnitOfWork
    {
        ITeacherRepository Teachers { get; }
        IDepartmentRepository Departments { get; }
        ISubjectRepository Subjects { get; }
        IStudentRepository Students { get; }
        IClassroomRepository Classrooms { get; }

        Task <int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
