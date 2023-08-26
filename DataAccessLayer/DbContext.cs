using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class TraversalDbContext : DbContext 
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(options =>
            {

            })
            base.OnConfiguring(optionsBuilder);
        }
    }
}
