using School_Management.Domain.Entities;
using SchoolManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management.Application.Interfaces
{
    public interface IStudentRepository
    {

        Task AddAsync(Student student, CancellationToken cancellationToken);

        Task<Student> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<IEnumerable<Student>> GetAllAsync(CancellationToken cancellationToken);

        Task DeleteAsync(Student student, CancellationToken cancellationToken);

        Task UpdateAsync(Student student, CancellationToken cancellationToken);
    }
}

