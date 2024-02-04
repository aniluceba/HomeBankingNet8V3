using Microsoft.EntityFrameworkCore;

namespace HomeBankingNet8V3.Models
{
    public class HomeBankingContext:DbContext 

    {
        public HomeBankingContext(DbContextOptions<HomeBankingContext> options) : base(options)
        {
        }
        public DbSet<Client> Clients { get; set; }
    }
}
