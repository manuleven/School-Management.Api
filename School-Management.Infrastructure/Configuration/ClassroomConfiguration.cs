using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School_Management.Domain.Entities;

namespace School_Management.Infrastructure.Configuration
{
    public class ClassroomConfiguration : IEntityTypeConfiguration<Classroom>
    {
        public void Configure(EntityTypeBuilder<Classroom> builder)
        {
            builder.HasKey(c => c.ClassId);

            builder.Property(c => c.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(c => c.Capacity)
                .IsRequired();

            builder.HasOne(c => c.Department)
                .WithMany(c => c.Classrooms)
                .HasForeignKey(c=>c.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
