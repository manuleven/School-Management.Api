using SchoolManagement.Application.DTO;
using SchoolManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management.Application.DTO
{
    public class DepartmentDto
    {
        public Guid Id { get; set; }

        public string DepartmentCode { get;  set; } 
        public string DepartmentName { get;  set; } 
        public string CreatedBy { get;  set; } 
        

    }
}
