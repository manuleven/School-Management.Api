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
    public class DepartmentRepository(AppDbContext dbContext) : IDepartmentRepository
    {
        public async Task AddAsync(Department department, CancellationToken cancellation)
        {
            await dbContext.Departments.AddAsync(department);
        }

        public Task UpdateAsync(Department department, CancellationToken cancellation)
        {
            dbContext.Departments.Update(department);
            return Task.CompletedTask;
        }

        public async Task<Department> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await dbContext.Departments.FirstOrDefaultAsync(t => t.Id ==id);
        }

        public async Task<Department> GetByName(string department, CancellationToken cancellationToken)
        {
            return await dbContext.Departments.FirstOrDefaultAsync(t => t.DepartmentName == department, cancellationToken);
        }

        public async Task<IEnumerable<Department>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await dbContext.Departments.ToListAsync(cancellationToken);
        }

        public Task DeleteAsync(Department department, CancellationToken cancellationToken)
        {
              
            dbContext.Departments.Remove(department);
            return Task.CompletedTask;
            
        }
    }
}
