using Microsoft.EntityFrameworkCore;
using school_system_api.models;

namespace school_system_api.data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<TeacherSubject> TeacherSubjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TeacherSubject>()
                    .HasKey(pc => new { pc.TeacherId, pc.SubjectId });
            modelBuilder.Entity<TeacherSubject>()
                    .HasOne(p => p.Teacher)
                    .WithMany(pc => pc.TeacherSubjects)
                    .HasForeignKey(p => p.TeacherId);
            modelBuilder.Entity<TeacherSubject>()
                    .HasOne(p => p.Subject)
                    .WithMany(pc => pc.TeacherSubjects)
                    .HasForeignKey(c => c.SubjectId);
        }
    }
}