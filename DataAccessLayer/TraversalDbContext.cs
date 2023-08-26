using EntityLayer.Concretes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace DataAccessLayer
{
    public class TraversalDbContext : DbContext 
    {
        protected readonly IConfiguration configuration;

        public TraversalDbContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("PostgreSql"));
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Trip> Trips { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Guide> Guides { get; set; }
        public DbSet<CustomerTrip> CustomerTrips { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Blog> Blogs { get; set; }
    }
}
