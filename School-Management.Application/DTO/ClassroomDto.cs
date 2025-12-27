using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management.Application.DTO
{
    public class ClassroomDto
    {

        public string Name { get;  set; } 

        public int Capacity { get;  set; } 
        public Guid ClassId { get;  set; }
        
        public List<string> SubjectTaken { get; set; }
        public string DepartmentName { get;  set; } 

       
    }
}
