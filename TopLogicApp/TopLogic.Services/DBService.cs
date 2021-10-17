using Microsoft.EntityFrameworkCore;
using TopLogic.DataSource;

namespace TopLogic.Services
{
    public class DBService
    {
        public readonly DbContextOptionsBuilder<DemoWebAppContext> DBContext;

        public DBService(string dbConnection)
        {
            DBContext = new DbContextOptionsBuilder<DemoWebAppContext>();
            DBContext.UseSqlServer(dbConnection);
        }
    }
}
