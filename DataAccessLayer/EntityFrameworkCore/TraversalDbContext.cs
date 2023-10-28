using EntityLayer.Abstracts;
using EntityLayer.Concretes;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DataAccessLayer.EntityFrameworkCore
{
    public class TraversalDbContext : DbContext
    {
        public TraversalDbContext(DbContextOptions<TraversalDbContext> options) : base(options)
        { }

        public DbSet<Trip> Trips { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Guide> Guides { get; set; }
        public DbSet<CustomerTrip> CustomerTrips { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<TripLocation> TripLocations { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseNpgsql(Configuration.ConnectionString);
        //}
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var datas = ChangeTracker.Entries<IEntity>();
            foreach (var data in datas)
            {
                switch (data.State)
                {
                    case EntityState.Added:
                        data.Entity.CreatedTime = DateTime.Now;
                        data.Entity.IsActive = true;
                        break;
                    case EntityState.Modified:
                        data.Entity.UpdatedTime = DateTime.Now;
                        break;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }

    }
}
