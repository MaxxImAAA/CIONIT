using labaOnit.Models;
using Microsoft.EntityFrameworkCore;

namespace labaOnit.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            try
            {
                Database.EnsureCreated();
            }
            catch(Exception ex){
                Console.WriteLine("nononononono");
                Console.WriteLine(ex.Message);
            }
           
            if (Database.CanConnect()) Console.WriteLine("okookokokokokokokokoko");
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            

            base.OnModelCreating(builder);
        }
    }
}
