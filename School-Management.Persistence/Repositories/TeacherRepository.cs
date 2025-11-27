using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Infrastructure.Context;

namespace SchoolManagement.Persistence.Repositories
{
    public class TeacherRepository(AppDbContext dbContext) : ITeacherRepository
    {
        public async Task AddAsync(Teacher teacher, CancellationToken cancellationToken)
        {
            await dbContext.Teachers.AddAsync(teacher, cancellationToken);
            
        }



        public async Task DeleteAsync(Teacher teach, CancellationToken cancellationToken)
        {
            var teacher = await dbContext.Teachers.FirstOrDefaultAsync(t => t.Id == teach.Id);
            if (teacher != null)
            {
                dbContext.Teachers.Remove(teacher);
                
            }

        }

        public async Task <IEnumerable<Teacher>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await dbContext.Teachers
                .Include(x => x.Department)
                
                .ToListAsync(cancellationToken);
        }

        public async Task<Teacher> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await dbContext.Teachers.Include(x => x.Department).FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task UpdateAsync(Teacher teacher, CancellationToken cancellationToken)
        {
            dbContext.Teachers.Update(teacher);
            
        }
    }
}
