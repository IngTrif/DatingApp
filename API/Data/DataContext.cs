using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext // photo associated with on euser
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet <AppUser> Users { get; set; }
    }
}