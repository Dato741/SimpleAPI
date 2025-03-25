using Microsoft.EntityFrameworkCore;
using ToDo.Api.Entities;

namespace ToDo.Api.Data
{
    public class ToDoListContext(DbContextOptions<ToDoListContext> options) : DbContext(options)
    {
        public DbSet<ToDoTask> Tasks => Set<ToDoTask>();
    }
}
