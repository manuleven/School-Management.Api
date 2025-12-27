using School_Management.Application.DTO;
using School_Management.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTO
{
    public class TeacherDto
    {
        public Guid Id { get; set; }

        public string FullName { get; set; } = string.Empty;
         
        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string Department { get; set; } 
        public string Subject { get; set; } 
        public IEnumerable<string> TeacherClassrooms { get; set; } 

        public DateTime HireDate { get; set; }

        public bool IsActive { get; set; }

    }
}
