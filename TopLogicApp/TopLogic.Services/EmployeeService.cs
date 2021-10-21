using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TopLogic.Core.Exceptions;
using TopLogic.DataSource;
using TopLogic.Services.Interfaces;

namespace TopLogic.Services
{
    public class EmployeeService: DBService, IEmployeeService
    {        
        public EmployeeService(string dbConnection)
            : base(dbConnection)
        {
        }

        public async Task Delete(int id)
        {
            try {
                using (var context = new DemoWebAppContext(DBContext.Options)) {
                    var empData = await context.Employees
                        .FirstOrDefaultAsync(i => i.Id == id);

                    if (empData is null)
                        throw new RecordNotFoundException("Employee data not found");

                    context.Entry(empData).State = EntityState.Deleted;
                    await context.SaveChangesAsync();
                }
            }
            catch {
                throw;
            }
        }

        public async Task<object> Get(int id)
        {
            try {
                using (var context = new DemoWebAppContext(DBContext.Options)) {
                    var empData = await context.Employees
                        .FirstOrDefaultAsync(i => i.Id == id);

                    if (empData is null)
                        throw new RecordNotFoundException("Employee data not found");

                    return empData;
                }
            }
            catch {
                throw;
            }
        }

        public async Task<IEnumerable<object>> GetAll()
        {
            try {
                using (var context = new DemoWebAppContext(DBContext.Options)) {
                    return await context.Employees.ToListAsync();
                }
            }
            catch {
                throw;
            }
        }

        public Task<object> Update(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
