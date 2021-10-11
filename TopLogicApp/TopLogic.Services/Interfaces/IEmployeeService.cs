using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TopLogic.DataSource;

namespace TopLogic.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetEmployees();
    }
}
