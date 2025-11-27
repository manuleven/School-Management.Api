using School_Management.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;

namespace School_Management.Application.Interfaces
{
    public interface IDepartmentRepository
    {
        Task AddAsync(Department department, CancellationToken cancellation);
        Task UpdateAsync(Department department, CancellationToken cancellation);
        Task<Department> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<Department>> GetAllAsync(CancellationToken cancellationToken);
        Task DeleteAsync(Department department, CancellationToken cancellationToken);

        Task<Department> GetByName(string department, CancellationToken cancellationToken);
    }
}
