using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagement.Domain.Entities;

namespace School_Management.Infrastructure.Configuration
{
    public class TeachersConfiguration : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Department)
                .IsRequired()
                .HasMaxLength(100);

            builder.OwnsOne(t => t.FullName, fn =>
            {
                fn.Property(f => f.FirstName)
                  .HasMaxLength(50)
                  .IsRequired();
                
                fn.Property(f => f.LastName)
                  .HasMaxLength(50)
                  .IsRequired();

                builder.HasOne(t => t.Department)
                       .WithMany(d => d.Teachers)
                       .HasForeignKey(t => t.DepartmentId)
                       .OnDelete(DeleteBehavior.Restrict);



            });

           
        }
    }
}
