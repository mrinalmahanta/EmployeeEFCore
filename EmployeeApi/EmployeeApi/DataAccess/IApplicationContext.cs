using EmployeeApi.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EmployeeApi.DataAccess
{
    public interface IApplicationContext
    {
        public DbSet<Employees> Employees { get; set; }
        public DbSet<EmployeeAddress> Addresses { get; set; }
        public Task<int> SaveToDB();
    }
}
