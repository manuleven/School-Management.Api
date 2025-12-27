using Microsoft.EntityFrameworkCore;
using School_Management.Application.Interfaces;
using School_Management.Domain.Entities;
using SchoolManagement.Infrastructure.Context;

namespace School_Management.Persistence.Repositories
{
    public class SubjectRepository(AppDbContext dbContext) : ISubjectRepository
    {
        public async Task AddAsync(Subject subject, CancellationToken cancellationToken)
        {
            await dbContext.AddAsync(subject, cancellationToken);
        }

        public async Task<Subject> GetSubjectById(Guid id, CancellationToken cancellationToken)
        {
            return await dbContext.Subjects.FirstOrDefaultAsync(s => s.Id == id);
        }
        public async Task<Subject> GetSubjectByName(string name, CancellationToken cancellationToken)
        {
            return await dbContext.Subjects.FirstOrDefaultAsync(s => s.Name.Value == name, cancellationToken);
        }

        public async Task<List<Subject>> GetByIdsAsync(List<Guid> ids, CancellationToken cancellationToken)
        {
            return await dbContext.Subjects
                .Where(c => ids.Contains(c.Id))
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Subject>> GetAllSubjects(CancellationToken cancellationToken)
        {
            return await dbContext.Subjects.ToListAsync(cancellationToken);
        }

        public async Task<bool> UpdateAsync(Subject subject, CancellationToken cancellationToken)
        {
            var check = await dbContext.Subjects.FirstOrDefaultAsync(s => s.Id == subject.Id);
            if(check != null)
            {
                dbContext.Subjects.Update(check);
                return true;
            }   
            return false;
        }

        public async Task DeleteAsync(Subject subject, CancellationToken cancellationToken)
        {
            var check = await dbContext.Subjects.FirstOrDefaultAsync(s => s.Id == subject.Id);
            if (check != null)
            {
                dbContext.Subjects.Remove(check);
            }
        }
    }
}