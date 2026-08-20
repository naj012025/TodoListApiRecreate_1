using Microsoft.EntityFrameworkCore;
using Npgsql;
using TodoListApiRecreate_1.Models;

namespace TodoListApiRecreate_1.Data;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
    public DbSet<TodoItem> Todos => Set<TodoItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var todo = modelBuilder.Entity<TodoItem>();

        todo.HasKey(x => x.Id); //compiler gets told what x is here so it knows.
        todo.Property(x => x.Title)
            .HasMaxLength(200) // Checks the info in createtodo for example that it has these attributes
            .IsRequired();
    }

}





