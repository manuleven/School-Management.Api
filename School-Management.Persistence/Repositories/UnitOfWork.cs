using School_Management.Application.Interfaces;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Infrastructure.Context;

namespace School_Management.Persistence.Repositories
{
    public class UnitOfWork(AppDbContext context, ITeacherRepository teacherRepository, IDepartmentRepository departmentrepository, 
        IStudentRepository studentrepository, IClassroomRepository classroomrepository, ISubjectRepository subjectRepository) : IUnitOfWork
    {
        public ITeacherRepository Teachers {  get; } = teacherRepository;
        public IDepartmentRepository Departments {  get; } = departmentrepository;
        public IStudentRepository Students {  get; } = studentrepository;
        public ISubjectRepository Subjects {  get; } = subjectRepository;
        public IClassroomRepository Classrooms {  get; } = classroomrepository;

        public async Task <int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await context.SaveChangesAsync(cancellationToken);
        }
    }
}
