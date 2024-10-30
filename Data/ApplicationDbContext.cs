using GraphQlApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphQlApi.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions option) : base(option) { }

    public DbSet<User> Users { get; set; }
}
