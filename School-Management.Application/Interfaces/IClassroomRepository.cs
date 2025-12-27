using School_Management.Domain.Entities;

namespace School_Management.Persistence.Repositories
{
    public interface IClassroomRepository
    {
        Task AddAsync(Classroom classroom, CancellationToken cancellationToken);
        Task <bool>ClassroomExistsAsync(string name, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(Classroom classroom, CancellationToken cancellationToken);
        Task<IEnumerable<Classroom>> GetAllAsync(CancellationToken cancellationToken);
        Task<Classroom> GetClassroomById(Guid id, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(Classroom classroom, CancellationToken cancellationToken);

        Task<List<Classroom>> GetByIdsAsync(List<Guid> ids, CancellationToken cancellationToken);

    }

}