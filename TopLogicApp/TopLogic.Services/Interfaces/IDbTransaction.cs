using System.Collections.Generic;
using System.Threading.Tasks;

namespace TopLogic.Services.Interfaces
{
    public interface IDbTransaction
    {
        Task<object> Get(int id);
        Task<IEnumerable<object>> GetAll();
        Task<object> Update(object obj);
        Task Delete(int id);
    }
}
