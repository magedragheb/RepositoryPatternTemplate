using Core.Entities;
using Core.Interfaces;

namespace Data;

public class UnitOfWork(RepoContext context, 
    IBaseRepository<Author> authors, 
    IBaseRepository<Book> books) : IUnitOfWork
{
    public IBaseRepository<Author> Authors => authors;

    public IBaseRepository<Book> Books => books;

    public async Task Commit() => await context.SaveChangesAsync();

    public async void Dispose()
    {
        await context.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}