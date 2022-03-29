using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManyToMany
{
    public class EmployeeAddress
    {
        public int Id { get; set; }
        public int EmployeeAdreessId { get; set; }

        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }

        [ForeignKey("AddressId")]
        public Address Address { get; set; }

        public static implicit operator DbSet<object>(EmployeeAddress v)
        {
            throw new NotImplementedException();
        }
    }
}
