using RandomSchoolAsync.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace RandomSchoolAsync.DAL
{
    public class SchoolContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Pupil> Pupils { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Nation> Nations { get; set; }
        public DbSet<Parent> Parents { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<SchoolTypeCode> SchoolTypeCodes { get; set; }
        public DbSet<Year> Years { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Grading> Gradings { get; set; }

        public SchoolContext()
        {
            Database.SetInitializer(new SchoolInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
