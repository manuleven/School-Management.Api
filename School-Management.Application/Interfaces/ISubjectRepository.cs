using School_Management.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management.Application.Interfaces
{
    public interface ISubjectRepository
    {
        Task AddAsync(Subject subject, CancellationToken cancellationToken);

        Task<Subject> GetSubjectById(Guid id, CancellationToken cancellationToken);

        Task<List<Subject>> GetByIdsAsync(List<Guid> ids, CancellationToken cancellationToken);

        Task<IEnumerable<Subject>> GetAllSubjects(CancellationToken cancellationToken);

        Task<bool> UpdateAsync(Subject subject, CancellationToken cancellationToken);
        Task<Subject> GetSubjectByName(string name, CancellationToken cancellationToken);
        Task DeleteAsync(Subject subject, CancellationToken cancellationToken);
    }
}
