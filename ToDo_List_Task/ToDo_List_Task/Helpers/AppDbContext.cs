using Microsoft.EntityFrameworkCore;
using ToDo_List_Task.Models;

namespace ToDo_List_Task.Helpers
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        
        }

        public DbSet<ToDoListViewModel> ToDoListItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}