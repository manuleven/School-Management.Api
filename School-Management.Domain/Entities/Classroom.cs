using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management.Domain.Entities
{
    public class Classroom : BaseEntity
    {
        private Classroom() { }

        private Classroom(string name, int capacity, Guid? departmentId = null)
        {
           Name = name ?? throw new ArgumentNullException(nameof(name));
            if(capacity == null)
           throw new ArgumentNullException("capacity exceeded");
            

            Capacity = capacity;

            DepartmentId = departmentId;
        }

        public string Name { get; private set; } = default!;

        public int Capacity { get; private set; } = default!;
        public ICollection<Student> Students { get; private set; } = new List<Student>();
        

        public List<Teacher> Teachers { get; private set; } = new();
        public Guid? DepartmentId { get; private set; } = default!;
        public ICollection<Subject> Subjects { get; private set; } = new List<Subject>();


        public Department? Department { get; private set; } = default!;

        public static Classroom Create(string name, int capacity, Guid? departmentid = null, string? createdBy = null)
        {
            ValidateInputs(name, capacity);
            var classroom = new Classroom(name.Trim(), capacity, departmentid);
            classroom.UpdateMetadata (createdBy);
            return classroom;
        }   

        public static void ValidateInputs(string name, int capacity)
        {
            if(string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));
            if (name.Length < 2 || name.Length > 50)
                throw new ArgumentException("Classroom name must be between 2 and 50 characters.", nameof(name));
            if (capacity <= 0)
                throw new ArgumentException("Capacity must be greater than 0.", nameof(capacity));

            if (capacity > 200)
                throw new ArgumentException("Capacity is too large");

        }

        public void AddTeacher(Teacher teacher)
        {
            if (!Teachers.Contains(teacher))
                Teachers.Add(teacher);
        }

        public void AddStudent(Student student, string? modifiedBy = null)
        {

            if (student == null)
                throw new ArgumentNullException(nameof(student));

            if (Students.Any(s => s.Id == student.Id))
                throw new Exception("Student already assigned to this classroom");

            if (Students.Count >= Capacity)
                throw new Exception("Classroom capacity reached");

            Students.Add(student);
            UpdateMetadata(modifiedBy);

        }
        public void AssignSubjects(List<Subject> subjects, string? modifiedBy = null)
        {
            if (subjects == null || !subjects.Any())
                throw new ArgumentException("Subject list cannot be empty");

            if (subjects.Count() <= 7 || subjects.Count() > 9)
                throw new Exception("Subject must not be lesser than 8 or greater than 9");

            foreach (var subjectId in subjects)
            {
                if (Subjects.Any(s => s.Id == subjectId.Id))
                    continue;
                   
                Subjects.Add(subjectId);

            }

            UpdateMetadata(modifiedBy);
        }


        public void UpdateClassroom(string newName,Guid? departmentId = null, string? modifiedBy = null)
        {
            if (string.IsNullOrEmpty(newName))
                throw new ArgumentNullException("Classroom name cannot be empty.", nameof(newName));
            if (newName.Length < 2 || newName.Length > 50)
                throw new ArgumentException("Classroom name must be between 2 and 50 characters.", nameof(newName));
            Name = newName.Trim();
            DepartmentId = departmentId;
            UpdateMetadata(modifiedBy);
        }

        public void UpdateCapacity(int newCapacity, string? modifiedBy = null)
        {
            if (newCapacity <= 0 || newCapacity > 200)
                throw new ArgumentException("Capacity must be greater than 0 and lesser than 200.", nameof(newCapacity));
            Capacity = newCapacity;
            UpdateMetadata(modifiedBy);
        }

        

    }
}
