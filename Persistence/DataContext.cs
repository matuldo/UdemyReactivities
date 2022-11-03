using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class DataContext : DbContext
{
    public DbSet<Activity> Activities => Set<Activity>();
    
    public DataContext(DbContextOptions options) : base(options) { }
}