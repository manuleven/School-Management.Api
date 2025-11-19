using School_Management.Domain.Entities;
using SchoolManagement.Domain.Utilities;
using SchoolManagement.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities
{
    public class Teacher : BaseEntity
    {
        private Teacher() { }   

        private Teacher(FullName fullName, string email, string phoneNumber, Guid departmentId, string createdBy = null)
        {
            FullName = fullName ?? throw new ArgumentNullException(nameof(fullName));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            PhoneNumber = phoneNumber ?? throw new ArgumentNullException( nameof(phoneNumber));
            DepartmentId = departmentId;
            HireDate = DateTime.UtcNow;
        }

        public FullName FullName { get; private set; } = default!;

        public string Email { get; private set; } = default!;

        public string PhoneNumber { get; private set; } = default!;

        public Department Department { get; private set;} = default!;

        public Guid DepartmentId { get; private set; } = default!;

        public DateTime HireDate { get; private set; }

        public bool IsActive { get; private set; } = true;

        public static Teacher Create(FullName fullName, string email, string phoneNumber, Guid departmentId, string? createdBy = null)
        {
            Validate(email, phoneNumber);

            var teacher = new Teacher(fullName, email.Trim(), phoneNumber.Trim(), departmentId);
           teacher.CreatedBy = createdBy;
            return teacher;
        }

      

        public void ChangePhoneNumber(string phoneNumber, string? modifiedBy = null)
        {
            if(string.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentNullException(nameof (phoneNumber));

            PhoneNumber = phoneNumber.Trim();
            UpdateMetadata(modifiedBy);
        }
        public void UpdateEmail(string newEmail, string? modifiedBy = null)
        {
            if(string.IsNullOrWhiteSpace(newEmail) || !newEmail.Contains("@"))
                throw new ArgumentNullException(nameof (newEmail));

            Email = newEmail.Trim();
            UpdateMetadata(modifiedBy);
        }
        public void UpdateFullName(string firstName, string lastName, string? modifiedBy = null)
        {
            if(string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentNullException(nameof (firstName));

            if(string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentNullException(nameof (lastName));

            FullName = FullName.Create(firstName, lastName);
            UpdateMetadata(modifiedBy);
        }


        public void Deactivate(string modifiedBy = null)
        {
            IsActive = false;
            UpdateMetadata(modifiedBy);
        }

        public void Activate(string modifiedBy = null)
        {
            IsActive = true;
            UpdateMetadata(modifiedBy);
        }


        public static void Validate(string email, string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
                throw new ArgumentException("Invalid email");

            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentException("Phone number is required");

        }

    }
}
