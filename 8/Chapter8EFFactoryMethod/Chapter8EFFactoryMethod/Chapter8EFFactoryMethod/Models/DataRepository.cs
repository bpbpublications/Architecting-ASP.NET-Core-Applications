using Microsoft.EntityFrameworkCore;
namespace Chapter8EFFactoryMethod.Models;
public class DataRepository
{
    private readonly IDbContextFactory dbContextFactory;
    private readonly MyDbContext context;
    public DataRepository(IDbContextFactory dbFactory)
    {
        dbContextFactory = dbFactory;
        context = dbContextFactory.CreateDbContext();
    }
    public async Task<List<Book>> GetAllEntitiesAsync() 
        => await context.Books.ToListAsync();
    public async Task AddEntityAsync(Book entity)
    {
        context.Books.Add(entity);
        await context.SaveChangesAsync();
    }
    public void InitializeDatabase() 
        => context.Database.EnsureCreated();
}