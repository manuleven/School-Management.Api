namespace School_Management.Application.DTO
{
    public class StudentDto
    {
        public Guid Id { get; set; }

        public string FullName { get; set; } 

        public DateTime DateOfBirth { get;  set; }

        public string PhoneNumber { get;  set; } 

        public string Department { get;  set; }
        public string Classroom { get;  set; }

        public string State { get; set; }

        public List<string> Subjects { get; set; } 

        public string Address { get;  set; } 
        public string CreatedBy { get;  set; } 

        public DateTime DateOfEnrollment { get; set; }

        public int Age { get; set; } 
    }
}
