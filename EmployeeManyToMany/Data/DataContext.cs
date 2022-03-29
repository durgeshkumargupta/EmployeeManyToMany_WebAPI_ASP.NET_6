using Microsoft.EntityFrameworkCore;

namespace EmployeeManyToMany.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Employee> Employee { get; set; }

        public DbSet<Address> Address { get; set; }

        public DbSet<EmployeeAddress> EmployeesAddress { get; set; }

    }
}