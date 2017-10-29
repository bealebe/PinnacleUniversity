using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinnacleUniversity.DataModels
{
    public class UniversityContext : DbContext
    {

        public UniversityContext()
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<UniversityContext>());
        }

        public DbSet<StudentOverview> StudentsOverview { get; set; }
        public DbSet<EnrolledCourse> EnrolledCourses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // configures one-to-many relationship
            modelBuilder.Entity<StudentOverview>()
                .HasMany<EnrolledCourse>(s => s.Courses)
                .WithMany(c => c.Students)
                .Map(cs =>
                    {
                        cs.MapLeftKey("StudentId");
                        cs.MapRightKey("CourseId");
                        cs.ToTable("StudentCourses");
                    });            
        }
    }
}
