using Microsoft.EntityFrameworkCore;
using TopLogic.DataSource;

namespace TopLogic.Services
{
    public class BaseService
    {
        public readonly DbContextOptionsBuilder<DemoWebAppContext> DBContext;

        public BaseService(string dbConnection)
        {
            DBContext = new DbContextOptionsBuilder<DemoWebAppContext>();
            DBContext.UseSqlServer(dbConnection);
        }
    }
}
