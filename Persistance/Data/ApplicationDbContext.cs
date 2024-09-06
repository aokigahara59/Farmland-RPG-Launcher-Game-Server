using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistance.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Player> Players { get; set; }
    }
}
