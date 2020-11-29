using FixTheThing.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace FixTheThing.Database.Contexts
{
    public class FttDbContext : DbContext
    {
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<WorkOrder> WorkOrders { get; set; }

        public FttDbContext(DbContextOptions<FttDbContext> options) : base(options)
		{

		}
    }
}
