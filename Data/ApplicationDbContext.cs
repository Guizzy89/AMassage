using Microsoft.EntityFrameworkCore;

namespace AMassage.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Massage> Massages { get; set; }
        public DbSet<Booking> Bookings { get; set; }
    }
}