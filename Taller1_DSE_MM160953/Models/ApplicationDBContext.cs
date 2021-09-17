using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Taller1_DSE_MM160953.Models
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext() : base("AppDBContext")
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories{ get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}