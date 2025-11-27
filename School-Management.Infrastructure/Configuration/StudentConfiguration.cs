using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School_Management.Domain.Entities;

namespace School_Management.Infrastructure.Configuration
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {


        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(x => x.Id);


            builder.OwnsOne(x => x.FullName, fn =>
            {
                fn.Property(x => x.FirstName)
                .HasMaxLength(200)
                .IsRequired();

                fn.Property(x => x.LastName)
                .HasMaxLength(200)
                .IsRequired();
            }
             );

            builder.Property(x => x.State)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.PhoneNumber)
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(x => x.Age)
                .IsRequired();

            builder.Property(x => x.DateOfEnrollment)
                .IsRequired();

            builder.Property(x => x.DateOfBirth)
                .IsRequired();

            builder.Property(x => x.Address)
                .HasMaxLength(50)
                .IsRequired();

            builder.HasOne(s => s.Department)
                .WithMany(d => d.Students)
                .HasForeignKey(s => s.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict); 

            builder.HasOne(s => s.Classroom)
                .WithMany(d => d.Students)
                .HasForeignKey(s => s.ClassroomId)
                .OnDelete(DeleteBehavior.Restrict); 



        }
    }
}
