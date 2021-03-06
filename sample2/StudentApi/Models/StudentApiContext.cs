using Microsoft.EntityFrameworkCore;

namespace StudentApi.Models
{
    public class StudentApiContext :DbContext
    {
        public StudentApiContext(DbContextOptions<StudentApiContext> options) : base(options){

        }
        public DbSet<Student> Students {get; set;}
    }
}