using EntityLayer.Abstracts;
using EntityLayer.Concretes;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DataAccessLayer.EntityFrameworkCore
{
	public class TraversalDbContext : IdentityDbContext<User, UserRole, int>
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
		public DbSet<TripComment> TripComments { get; set; }
		public DbSet<TripDate> TripDates { get; set; }
		public DbSet<BlogComment> BlogComments { get; set; }
		public DbSet<TripKey> TripKeys { get; set; }
		public DbSet<BlogKey> BlogKeys { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

			modelBuilder.Entity<Customer>()
			.HasOne(a => a.User)
			.WithOne(a => a.Customer)
			.HasForeignKey<Customer>(c => c.UserId);

			modelBuilder.Entity<Guide>()
			.HasOne(a => a.User)
			.WithOne(a => a.Guide)
			.HasForeignKey<Guide>(c => c.UserId);

            //modelBuilder.Entity<Trip>().HasQueryFilter(c => c.IsActive);
            //modelBuilder.Entity<Blog>().HasQueryFilter(c => c.IsActive);

            base.OnModelCreating(modelBuilder);
		}

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
