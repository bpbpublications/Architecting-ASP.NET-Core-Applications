using Microsoft.EntityFrameworkCore;

namespace Chapter8EFFactoryMethod.Models;

public interface IDbContextFactory
{
    MyDbContext CreateDbContext();
    void InitializeDatabase();
}

