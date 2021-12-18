using System.Collections.Generic;
using AirConsole.Solution.Models;

namespace AirConsole.Solution.Data
{
    public interface IRepository<TItem, in TKey> where TItem: Model<TKey>
    {
        void Add(TItem item);
        void Edit(TItem item);
        TItem Delete(TKey key);
        TItem Get(TKey key);
        IEnumerable<TItem> GetAll();
    }
}