using EmployeeApi.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EmployeeApi.DataAccess
{
    public class EmployeeContext : DbContext, IApplicationContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Employees>(b =>
            {
                b.HasIndex(e => new { e.FirstName, e.LastName,e.EmailId }).IsUnique(true);
            });
        }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<EmployeeAddress> Addresses { get; set; }
        public async Task<int> SaveToDB()
        {
            return await base.SaveChangesAsync();
        }
       
    }


}
