using Microsoft.EntityFrameworkCore;
using School_Management.Application.Interfaces;
using School_Management.Domain.Entities;
using SchoolManagement.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management.Persistence.Repositories
{
    public class ClassroomRepository(AppDbContext dbContext) : IClassroomRepository
    {
        public async Task<bool> ClassroomExistsAsync(string name, CancellationToken cancellationToken)
        {
           return await dbContext.Classrooms.AnyAsync(c => c.Name == name, cancellationToken);
        }

        public async Task AddAsync(Classroom classroom, CancellationToken cancellationToken)
        {
            await dbContext.AddAsync(classroom, cancellationToken);
        }

        public async Task<IEnumerable<Classroom>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await dbContext.Classrooms.Include(x => x.Department).ToListAsync(cancellationToken);
        }

        public async Task<Classroom> GetClassroomById(Guid id, CancellationToken cancellationToken)
        {
            return await dbContext.Classrooms.Include(x => x.Department).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> UpdateAsync(Classroom classroom, CancellationToken cancellationToken)
        {
            var check = await dbContext.Classrooms.FirstOrDefaultAsync(c => c.Id == classroom.Id);
            if (check != null)
            {
                dbContext.Update(classroom);
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(Classroom classroom, CancellationToken cancellationToken)
        {
            var check = await dbContext.Classrooms.FirstOrDefaultAsync(c => c.Id == classroom.Id);
            if (check != null)
            {
                dbContext.Remove(classroom);
                return true;
            }
            return false;
        }
    }
}
