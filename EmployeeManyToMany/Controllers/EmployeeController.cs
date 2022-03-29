using EmployeeManyToMany.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManyToMany.Controllers
{
   // [Route("api/[controller]")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private readonly DataContext dataContext;
        public EmployeeController(DataContext dataContext )
        {
            this.dataContext = dataContext; 
        }

        [HttpGet]
        public async Task<ActionResult<List<EmployeeAddress>>> Get()
        {

            var employee = await dataContext.Employee.ToListAsync();
            var address= await dataContext.Address.ToListAsync();
            var employeeAddress = await dataContext.EmployeesAddress.ToListAsync();

            foreach (EmployeeAddress employeeAddress2 in employeeAddress)
            {
                employeeAddress2.Employee = dataContext.Employee.Where(e => e.EmployeeId == employeeAddress2.Employee.EmployeeId).FirstOrDefault();
            }
            return Ok(dataContext.EmployeesAddress.ToList());
        }

        [HttpPost]
        public async Task<ActionResult> Post(EmployeeAddress employeeAddress)
        {
            dataContext.EmployeesAddress.Add(employeeAddress);
            dataContext.SaveChanges();
            return Ok(employeeAddress);
        }
        
       
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployeeById(int id)
        {

            var employees =  dataContext.Employee.Where(e => e.EmployeeId == id).ToList();
          
            if (employees == null)
                return BadRequest("Employee Not Avaiable");

            foreach (var employee in employees)
            {
                dataContext.Employee.Remove(employee);
            }
            
            dataContext.SaveChanges();
            return Ok("Employee Deleted");

        }
        
       
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAddressById(int id)
        {

            var addressess =  dataContext.Address.Where(e => e.AddressId == id).ToList();

            if (addressess == null)

                return BadRequest("Address Not Avaiable");

            foreach(var address in addressess)
            {
                dataContext.Address.Remove(address);
            }
           
            dataContext.SaveChanges();

            return Ok("Address Deleted");
        }
        

        [HttpPut("id")]
        public async Task<ActionResult<List<EmployeeAddress>>> UpdateAddressById(int id,Address address)
        {
           
            var addresses = dataContext.Address.Where( e => e.AddressId == id);
            if (addresses == null )
            {
                return BadRequest("Address Not available");
            }

            foreach (var address2 in addresses)
            {
                address2.AddressId = address.AddressId;
                address2.City = address.City;
                address2.Pin = address.Pin;
            }
            
            dataContext.SaveChanges();
            return Ok(addresses);
        }
        [HttpPut("id")]
        public async Task<ActionResult> UpdateEmployeeById(int id,Employee employee)
        {
            var employeees=dataContext.Employee.Where(e => e.EmployeeId == id).ToList();
            if (employeees == null)
                return BadRequest("Employee Not Available");

            foreach(var employee2 in employeees)
            {
                employee2.EmployeeId = employee.EmployeeId;
                employee2.Name= employee.Name;
                employee2.Company= employee.Company;
            }
            dataContext.SaveChanges();
            return Ok(employee);

        }
  
    }
}
