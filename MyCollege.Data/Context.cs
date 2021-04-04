using MyCollege.Data.Models;
using System.Data.Entity;

namespace MyCollege.Data
{
    public class Context : DbContext
    {
        public Context() : base("MyCollegeDb")
        {

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Grade> Grades { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Grade>()
                .HasKey(e => e.Id);
            modelBuilder.Entity<Student>()
                .HasKey(e => e.Id);
            modelBuilder.Entity<Teacher>()
                .HasKey(e => e.Id);
            modelBuilder.Entity<Subject>()
                .HasKey(e => e.Id);
            modelBuilder.Entity<Course>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<Grade>()
                .HasRequired(s => s.Subject)
                .WithMany(c => c.Grades)
                .HasForeignKey<int>(e => e.SubjectId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Grade>()
                .HasRequired(s => s.Student)
                .WithMany(c => c.Grades)
                .HasForeignKey<int>(e => e.StudentId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Subject>()
                .HasRequired(e => e.Teacher)
                .WithMany(e => e.Subjects)
                .HasForeignKey<int>(e => e.TeacherId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Subject>()
                 .HasOptional(e => e.Course)
                 .WithMany(e => e.Subjects)
                 .HasForeignKey<int?>(e => e.CourseId)
                 .WillCascadeOnDelete(true);
        }
    }
}
