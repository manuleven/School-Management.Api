using Microsoft.EntityFrameworkCore;
using School_Management.Application.Interfaces;
using School_Management.Domain.Entities;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Infrastructure.Context;

namespace School_Management.Persistence.Repositories
{
    public class StudentRepository(AppDbContext dbContext) : IStudentRepository
    {
        public async Task AddAsync(Student student, CancellationToken cancellationToken)
        {
            await dbContext.AddAsync(student, cancellationToken);
        }

        public async Task DeleteAsync(Student student, CancellationToken cancellationToken)
        {
            var check = await dbContext.Students.FirstOrDefaultAsync(t => t.Id == student.Id);
            if (check != null)
            {
                dbContext.Remove(student);

            }
        }

        public async Task<IEnumerable<Student>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await dbContext.Students.Include(x => x.Department).ToListAsync(cancellationToken);
        }

        public async Task<Student> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await dbContext.Students.Include(x => x.Department).FirstOrDefaultAsync(t => t.Id == id);

        }

        public async Task UpdateAsync(Student student, CancellationToken cancellationToken)
        {
            var check = await dbContext.Teachers.FirstOrDefaultAsync(t => t.Id == student.Id);
            if (check != null)
            {
                dbContext.Update(student);
            }
        }
    }
}
