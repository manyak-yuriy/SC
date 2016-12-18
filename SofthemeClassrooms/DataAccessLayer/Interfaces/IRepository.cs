using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IRepository<T, ID> 
        where T : class
    {
        IEnumerable<T> GetAll();
        T Get(ID Id);
        void Update(T item);
        void Delete(ID id);
        void Delete(IEnumerable<T> items);
        void Insert(T item);
    }
}
