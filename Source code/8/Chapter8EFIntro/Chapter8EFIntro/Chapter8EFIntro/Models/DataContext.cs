using Microsoft.EntityFrameworkCore;

namespace Chapter8EFIntro.Models;

public class DataContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    protected override void OnConfiguring
        (DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseInMemoryDatabase("database");
    public void InitializeDatabase()
        => Database.EnsureCreated();
    
}

