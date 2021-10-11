using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TopLogic.DataSource;
using TopLogic.Services.Interfaces;

namespace TopLogic.Services
{
    public class EmployeeService: IEmployeeService
    {
        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            using (var context = new DemoWebAppContext()) {
                return await context.Employees.ToListAsync();
            }
        }
    }
}
