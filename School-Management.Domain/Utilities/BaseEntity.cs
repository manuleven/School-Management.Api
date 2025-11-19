using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Utilities
{
    public  abstract class BaseEntity
    {
        public Guid Id { get; protected set; } = Guid.NewGuid();

        public DateTime DateCreated { get; protected set; } = DateTime.UtcNow;
        public DateTime? DateModified { get; protected set; } 

        public string CreatedBy { get; protected set; }
        public string ModifiedBy { get; protected set; }

        protected void UpdateMetadata(string modifiedBy = null)
        {
            DateModified = DateTime.UtcNow;
            ModifiedBy = modifiedBy;
        }
          

    }
}
