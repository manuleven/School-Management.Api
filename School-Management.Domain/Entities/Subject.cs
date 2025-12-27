using Microsoft.AspNetCore.Mvc;
using School_Management.Domain.ValueObjects;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management.Domain.Entities
{
    public class Subject : BaseEntity
    {
        private Subject() { }

        private Subject(SubjectName name, string description, string code)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Code = code ?? throw new ArgumentNullException(nameof(name));
        }

        public SubjectName Name { get; private set; } = default!;

        public string Description { get; private set; } = default!;

        public string Code { get; private set; } = default!;

       

        public List<Teacher> Teachers { get; private set; } = new();
        public List<Department> Departments { get; private set; } = new();
        


       

        public static Subject Create(SubjectName name, string description, string code, string ? createdBy= null)
        {
            Validate(name, description, code);
            var newSubject = new Subject(name, description, code);
            newSubject.CreatedBy= createdBy;
            return newSubject;
        }

        public void UpdateDescription(string description, string ? modifiedBy = null)
        {
            if(string.IsNullOrWhiteSpace(description))
                throw new ArgumentNullException (nameof(description));

            Description = description.Trim();
            UpdateMetadata (modifiedBy);
        }

        public void UpdateCode(string code, string ? modifiedBy = null)
        {
            if(string.IsNullOrWhiteSpace(code))
                throw new ArgumentNullException (nameof(code));
            if (code.Length < 2 || code.Length > 20)
                throw new ArgumentException("Code must be between 2 and 20 characters.", nameof(code));
            Code = code.Trim();
            UpdateMetadata (modifiedBy);
        }

        public void UpdateName(SubjectName name, string ? modifiedBy = null)
        {
            if(name == null)
                throw new ArgumentNullException (nameof(name));
            Name = name;
            UpdateMetadata (modifiedBy);
        }

      

        public static void Validate(SubjectName name, string description, string code)
        {
            if (name == null)
                throw new ArgumentNullException (nameof(name));
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentNullException (nameof(description));
            if (description.Length < 10 || description.Length > 500)
                throw new ArgumentException("Description must be between 10 and 500 characters.", nameof(description));
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentNullException (nameof(code));
            if (code.Length < 2 || code.Length > 20)
                throw new ArgumentException("Code must be between 2 and 20 characters.", nameof(code));
        }


    }
}
