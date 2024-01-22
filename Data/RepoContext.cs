using Microsoft.EntityFrameworkCore;

namespace Data;

public class RepoContext(DbContextOptions<RepoContext> options) : DbContext(options)
{

    // public DbSet<User> Users { get; set; }
}
