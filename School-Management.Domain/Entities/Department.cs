using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Utilities;

namespace School_Management.Domain.Entities
{
    public class Department : BaseEntity
    {
        private Department() { }

        private Department(string departmentCode, string departmentName)
        {
            DepartmentCode = departmentCode ?? throw new ArgumentNullException(nameof(departmentCode));
            CreatedBy = departmentCode ?? throw new ArgumentNullException(nameof(departmentName));
        }

        public string DepartmentCode { get; private set; } = default!;
        public string CreatedBy { get; private set; } = default!;

        public string DepartmentName { get; private set; } = default!;
        public ICollection<Teacher> Teachers { get; private set; } = new List<Teacher>();
        public ICollection<Student> Students { get; private set; } = new List<Student>();

        public static Department Create(string departmentCode, string departmentName, string createdBy = null)
        {
            ValidateInputs(departmentCode, departmentName);

            var department = new Department(departmentCode.Trim(), departmentName.Trim());
            department.UpdateMetadata(createdBy);
            return department;
        }

        public void UpdateDepartmentName(string newName,string modifiedBy = null)
        {
            if (string.IsNullOrEmpty(newName))
                throw new ArgumentNullException("Department name cannot be empty.", nameof(newName));


            if (newName.Length < 2 || newName.Length > 100)
                throw new ArgumentException("Department name must be between and 100 characters.", nameof(newName));

            CreatedBy = newName.Trim();
            UpdateMetadata(modifiedBy);
        }

        public void UpdateDepartmentCode(string newCode,string modifiedBy = null)
        {
            if (string.IsNullOrEmpty(newCode))
                throw new ArgumentNullException("Department code cannot be empty.", nameof(newCode));


            if (newCode.Length < 2 || newCode.Length > 20)
                throw new ArgumentException("Department code must be between and 20 characters.", nameof(newCode));

            DepartmentCode = newCode.Trim();
            UpdateMetadata(modifiedBy);
        }

        public static void ValidateInputs(string departmentCode, string departmentName)
        {
            if(string.IsNullOrEmpty(departmentCode))
                throw new ArgumentNullException("Department code cannot be empty", nameof(departmentCode) );

            if (departmentCode.Length < 2 || departmentCode.Length > 20)
                throw new ArgumentException("Department code must be between and 20 characters.", nameof(departmentCode));

            if (string.IsNullOrEmpty(departmentName))
                throw new ArgumentNullException("Department name cannot be empty", nameof(departmentName));

            if (departmentCode.Length < 2 || departmentCode.Length > 100)
                throw new ArgumentException("Department name must be between and 100 characters.", nameof(departmentName));
        }
    }
}
