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

        private Classroom(string name, int capacity, Guid classId)
        {
           Name = name ?? throw new ArgumentNullException(nameof(name));
            if(capacity == null) { 
           throw new ArgumentNullException("capacity exceeded");
            }

            Capacity = capacity;

            ClassId = classId;
        }

        public string Name { get; private set; } = default!;

        public int Capacity { get; private set; } = default!;
        public ICollection<Student> Students { get; private set; } = new List<Student>();
        public Guid ClassId { get; private set; } = default!;

        public List<Teacher> Teachers { get; private set; } = new();
        public Guid DepartmentId { get; private set; } = default!;

        public Department Department { get; private set; } = default!;

        public static Classroom Create(string name, int capacity, Guid departmentid, string? createdBy = null)
        {
            ValidateInputs(name, capacity, departmentid);
            var classroom = new Classroom(name.Trim(), capacity, departmentid);
            classroom.UpdateMetadata (createdBy);
            return classroom;
        }   

        public static void ValidateInputs(string name, int capacity, Guid departmentId)
        {
            if(string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));
            if (name.Length < 2 || name.Length > 50)
                throw new ArgumentException("Classroom name must be between 2 and 50 characters.", nameof(name));
            if (capacity <= 0)
                throw new ArgumentException("Capacity must be greater than 0.", nameof(capacity));

            if (capacity > 200)
                throw new ArgumentException("Capacity is too large");

            if (departmentId == Guid.Empty)
                throw new ArgumentException("Department Id is required");
        }

        public void UpdateClassroom(string newName,Guid departmentId, string? modifiedBy = null)
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
            if (newCapacity <= 0)
                throw new ArgumentException("Capacity must be greater than 0.", nameof(newCapacity));
            Capacity = newCapacity;
            UpdateMetadata(modifiedBy);
        }

        

    }
}
