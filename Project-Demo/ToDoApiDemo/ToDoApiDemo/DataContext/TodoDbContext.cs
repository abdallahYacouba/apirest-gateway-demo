using Microsoft.EntityFrameworkCore;
using ToDoApiDemo.Model.Entities;

namespace ToDoApiDemo.DataContext;

public class TodoDbContext : DbContext
{
    public TodoDbContext(DbContextOptions<TodoDbContext> option) : base(option)
    {  }

    public DbSet<TodoItem> Todos { get; set; }
}
