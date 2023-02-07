using System.Collections.Generic;

namespace Codecool.CodecoolShop.Daos
{
    public interface IDbDao<T>
    {
        void Add(T item);
        void Update(int id);
        void Delete(int id);
        T Get(int id);
        IEnumerable<T> GetAll();
    }
}
