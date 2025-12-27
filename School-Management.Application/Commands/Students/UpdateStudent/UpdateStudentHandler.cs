using MediatR;
using School_Management.Application.Interfaces;

namespace School_Management.Application.Commands.Students.UpdateStudent
{
    public class UpdateStudentHandler(IUnitOfWork repository) : IRequestHandler<UpdateStudentCommand, bool>
    {
        public async Task<bool> Handle(UpdateStudentCommand command, CancellationToken cancellationToken)
        {
            var student = await repository.Students.GetByIdAsync(command.Id, cancellationToken);
           
            if (student == null)
                throw new Exception("Student not found");

         

            var newClassroom = await repository.Classrooms.GetClassroomById(command.ClassroomId, cancellationToken);
            if (newClassroom == null)
                throw new Exception("Classroom doesnt exist");

            if (newClassroom.DepartmentId.HasValue)
            {
                var department = await repository.Departments.GetByIdAsync(newClassroom.DepartmentId.Value, cancellationToken);

                if (department == null)
                    throw new Exception("Department does not exist");

                student.ChangeDepartment(newClassroom.DepartmentId.Value);
            }



            student.UpdateFullName(command.FirstName, command.LastName);
                student.ChangePhoneNumber(command.PhoneNumber);
                student.UpdateAddress(command.Address);
            student.ChangeClassroom(newClassroom.Id);
                student.UpdateDob(command.DateOfBirth);
                student.UpdateState(command.State);

              

                await repository.Students.UpdateAsync(student, cancellationToken);
               
                await repository.SaveChangesAsync(cancellationToken);

                return true;
            

            
        }
    }
}
