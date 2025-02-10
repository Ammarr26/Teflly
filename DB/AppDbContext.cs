using Microsoft.EntityFrameworkCore;
using Teffly.Models;

namespace Teffly.DB
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Vaccine> Vaccines { get; set; }
    }
}