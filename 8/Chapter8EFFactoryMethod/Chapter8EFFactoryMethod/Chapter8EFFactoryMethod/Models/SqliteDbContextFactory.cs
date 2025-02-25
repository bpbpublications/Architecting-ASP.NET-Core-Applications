using Microsoft.EntityFrameworkCore;
namespace Chapter8EFFactoryMethod.Models;
public class SqliteDbContextFactory : IDbContextFactory
{
    private readonly string connectionString;
    public SqliteDbContextFactory(string connectionString)
    => this.connectionString = connectionString;
    public MyDbContext CreateDbContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();
        optionsBuilder.UseSqlite(connectionString);
        return new MyDbContext(optionsBuilder.Options);
    }
    public void InitializeDatabase()
    {
        using var context = CreateDbContext();
        // Ensure the database is created
        context.Database.EnsureCreated();
    }
}