using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School_Management.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management.Infrastructure.Configuration
{
    public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder.HasKey(s => s.Id);

            builder.OwnsOne(s => s.Name, n =>
            {
                n.Property(p => p.Value)
                 
                 .HasMaxLength(100)
                 .IsRequired();
            });


            builder.Property(s => s.Description)
                .IsRequired();

            

        }
    }
}
