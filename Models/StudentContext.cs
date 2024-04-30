using Microsoft.EntityFrameworkCore;

namespace MyFullStackBackendDemo1;

public class StudentContext : DbContext
{
    public StudentContext(DbContextOptions<StudentContext> options):base(options){}
    public DbSet<Student> Student{get;set;}
}
