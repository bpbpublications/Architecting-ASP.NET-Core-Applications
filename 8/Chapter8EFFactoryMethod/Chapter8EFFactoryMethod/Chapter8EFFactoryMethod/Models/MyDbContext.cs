using Microsoft.EntityFrameworkCore;
namespace Chapter8EFFactoryMethod.Models;
public class MyDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }
    public void InitializeDatabase()
        => Database.EnsureCreated();
}
