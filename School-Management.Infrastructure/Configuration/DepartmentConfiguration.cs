using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School_Management.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace School_Management.Infrastructure.Configuration
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("Departments");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.DepartmentCode)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.DepartmentName)
                .HasMaxLength(40)
                .IsRequired();

            builder.Property(x => x.CreatedBy)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.OwnsMany(d => d.SubjectTaken, a =>
            {
                a.ToTable("DepartmentAllowedSubjects");
                a.WithOwner()
                .HasForeignKey("DepartmentId");
                a.Property(p => p.Value)
                 .HasColumnName("SubjectName")
                 .HasMaxLength(100)
                 .IsRequired();
            });
        }
    }
}
