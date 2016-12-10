using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    interface IRepository<T, ID> 
        where T : class
    {
        IEnumerable<T> GetAll();
        T Get(ID Id);
        void Update(T item);
        void Delete(ID id);
    }
}
