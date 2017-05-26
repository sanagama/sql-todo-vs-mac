using Microsoft.EntityFrameworkCore;

namespace TodoApp.Models
{
    public class TodoAppContext : DbContext
    {
        public TodoAppContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Person> Persons { get; set; }
        
        public DbSet<Task> Tasks { get; set; }
    }
}