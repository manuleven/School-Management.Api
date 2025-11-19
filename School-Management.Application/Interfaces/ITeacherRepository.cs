using SchoolManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Interfaces
{
    public interface ITeacherRepository
    {
        Task AddAsync(Teacher teacher, CancellationToken cancellationToken);

        Task <Teacher> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<IEnumerable<Teacher>> GetAllAsync(CancellationToken cancellationToken);

        Task DeleteAsync(Teacher teacher, CancellationToken cancellationToken);

        Task UpdateAsync(Teacher teacher, CancellationToken cancellationToken);
    }
}
