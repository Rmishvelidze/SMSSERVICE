using Microsoft.EntityFrameworkCore;
using SmsService.Models.Providers;

namespace SmsService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<SmsProvider>? SmsProviders { get; set; }
    }
}
