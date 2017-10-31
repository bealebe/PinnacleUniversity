using System.Data.Entity;

namespace PinnacleUniversity.DataModels
{
    /// <summary>
    /// This is the class for creating an Entity Framework Database through code-first
    /// </summary>
    public class UniversityContext : DbContext
    {
        public UniversityContext()
        {
            //Set initializer to drop and recreate DB because we are resetting the API each time anyway.
            Database.SetInitializer(new DropCreateDatabaseAlways<UniversityContext>());
        }

        public DbSet<StudentOverview> StudentsOverview { get; set; }
        public DbSet<EnrolledCourse> EnrolledCourses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // configures many-to-many relationship
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