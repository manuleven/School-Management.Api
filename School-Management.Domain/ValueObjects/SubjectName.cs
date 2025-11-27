using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management.Domain.ValueObjects
{
    public class SubjectName
    {
        public string Value { get; private set; }
        
        private SubjectName() { }

        public SubjectName(string value)
        {
            if(string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value), "Subject name cannot be null or empty.");

            if (value.Length < 2 || value.Length > 100)
                throw new ArgumentException("Subject name must be between 2 and 100 characters.", nameof(value));

            Value = value.Trim();
        }

        public static SubjectName Create(string name)
      => new SubjectName(name);
    }
}
