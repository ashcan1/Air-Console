using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Runtime.InteropServices;
using AirConsole.Solution.Models;

namespace AirConsole.Solution.Data
{
    public class InMemoryRepository<TItem, TKey> 
        : IRepository<TItem, TKey> where TItem : Model<TKey>
    {
        private Dictionary<TKey, TItem> items;
        public InMemoryRepository()
        {
            items = new Dictionary<TKey, TItem>();
        }

        public virtual void Add(TItem item)
        {
            items.Add(item.Id, item);
        }

        public virtual void Edit(TItem item)
        {
            items[item.Id] = item;
        }

        public virtual TItem Delete(TKey key)
        {
            var returnValue = items[key];
            items.Remove(key);
            return returnValue;
        }

        public virtual TItem Get(TKey key)
        {
            if (!items.ContainsKey(key))
            {
                return null;
            }
            var returnValue = items[key];
            return returnValue;
        }

        public virtual IEnumerable<TItem> GetAll()
        {
            return items.Values;
        }
    }
}