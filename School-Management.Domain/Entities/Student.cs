using SchoolManagement.Domain.Utilities;
using SchoolManagement.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management.Domain.Entities
{
    public class Student : BaseEntity
    {
        private Student() { }

        private Student(FullName fullName, DateTime dateOfBirth, string phoneNumber, string state, string address, int age, Guid departmentId)
        {
            FullName = fullName ?? throw new ArgumentNullException(nameof(fullName));
            DateOfBirth = dateOfBirth;
            PhoneNumber = phoneNumber ?? throw new ArgumentNullException(nameof(phoneNumber));
            State = state ?? throw new ArgumentNullException(nameof(state));
            Address = address ?? throw new ArgumentNullException(nameof(address));
            DateOfEnrollment = DateTime.UtcNow;
            if (age <= 0)
                throw new ArgumentException("Age must be greater than 0");

            Age = age;
            DepartmentId = departmentId;
        }

        public FullName FullName { get; private set; } = default!;

        public DateTime DateOfBirth { get; private set; }

        public string PhoneNumber { get; private set; } = default!;

        public Department Department { get; private set; } = default!;
        public Classroom Classroom { get; private set; } = default!;

        public Guid ClassroomId { get; private set; } = default!;
        public Guid DepartmentId { get; private set; } = default!;

        public string State { get; private set; } = default!;

        public string Address { get; private set; } = default!;

        public DateTime DateOfEnrollment { get; private set; }

        public int Age { get; private set; } = default!;

        public static Student Create(FullName fullName, DateTime dateOfBirth, string phoneNumber, string state, string address, int age, Guid departmentId, string? createdBy = null)
        {
            Validate(phoneNumber);
            var student = new Student(fullName, dateOfBirth, phoneNumber.Trim(), state.Trim(), address.Trim(), age, departmentId);
            student.CreatedBy = createdBy;
            return student;
        }

        public void UpdateFullName(string firstName, string lastName, string? modifiedBy = null)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentNullException(nameof(firstName));

            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentNullException(nameof(lastName));

            FullName = FullName.Create(firstName, lastName);
            UpdateMetadata(modifiedBy);
        }
        public void ChangePhoneNumber(string phoneNumber, string? modifiedBy = null)
        {
            if(string.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentNullException(nameof (phoneNumber));

            PhoneNumber = phoneNumber.Trim();
            UpdateMetadata(modifiedBy);
        }
        public void UpdateState(string state, string? modifiedBy = null)
        {
            if(string.IsNullOrWhiteSpace(state))
                throw new ArgumentNullException(nameof (state));

            State = state.Trim();
            UpdateMetadata(modifiedBy);
        }
        public void UpdateAddress(string address, string? modifiedBy = null)
        {
            if(string.IsNullOrWhiteSpace(address))
                throw new ArgumentNullException(nameof (address));

            Address = address.Trim();
            UpdateMetadata(modifiedBy);
        }

        public void ChangeDepartment(Guid newDepartmentId)
        {
            if (newDepartmentId == Guid.Empty)
                throw new ArgumentException("Invalid department Id");

            DepartmentId = newDepartmentId;
        }


        public void UpdateDob(DateTime newDob, string? modifiedBy = null)
        {
            if (newDob > DateTime.UtcNow)
                throw new ArgumentException("Date of birth cannot be in the future");

            DateOfBirth = newDob;

            // Age should change too
            Age = CalculateAge(newDob);

            UpdateMetadata(modifiedBy);
        }

        private int CalculateAge(DateTime dob)
        {
            var today = DateTime.UtcNow;
            int age = today.Year - dob.Year;

            if (dob.Date > today.AddYears(-age)) age--;

            return age;
        }
    
        private static void Validate(string phoneNumber)
        {
            if(string.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentNullException(nameof(phoneNumber));

            if (phoneNumber.Length < 7 || phoneNumber.Length > 15)
                throw new ArgumentNullException(nameof(phoneNumber));

        }




    }
}
