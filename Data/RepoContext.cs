using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class RepoContext(DbContextOptions<RepoContext> options) : DbContext(options)
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }

}
