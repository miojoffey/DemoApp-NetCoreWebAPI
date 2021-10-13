using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TopLogic.DataSource;
using TopLogic.Services.Interfaces;

namespace TopLogic.Services
{
    public class EmployeeService: BaseService, IEmployeeService
    {        
        public EmployeeService(string dbConnection)
            : base(dbConnection)
        {
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            try {
                using (var context = new DemoWebAppContext(DBContext.Options)) {
                    return await context.Employees.ToListAsync();
                }
            }
            catch (Exception error) {
                throw;
            }
        }
    }
}
