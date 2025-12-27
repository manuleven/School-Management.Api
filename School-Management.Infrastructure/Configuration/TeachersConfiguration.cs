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


            builder.OwnsOne(t => t.FullName, fn =>
            {
                fn.Property(f => f.FirstName)
                  .HasMaxLength(50)
                  .IsRequired();

                fn.Property(f => f.LastName)
                  .HasMaxLength(50)
                  .IsRequired();

            });

            builder.HasOne(t => t.Department)
                      .WithMany(d => d.Teachers)
                      .HasForeignKey(t => t.DepartmentId)
                      .IsRequired(false)
                      .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.Subject)
                   .WithMany(s => s.Teachers)
                   .HasForeignKey(t => t.SubjectId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(t => t.Classrooms)
                .WithMany(c => c.Teachers);





        }
    }
}
