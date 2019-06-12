using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MovieSite.TestProject
{
    public class FakeDbSet<T>
    {
        private List<T> _data;

        public FakeDbSet()
        {
            _data = new List<T>();
        }
        public IEnumerable<T> Where(Func<T, bool> condition)
        {
            return _data.Where(condition);
        }
        public int Count()
        {
            return _data.Count();
        }
        public void Add(T item)
        {
            _data.Add(item);
        }
        public void RemoveAt(int index)
        {
            _data.RemoveAt(index);
        }
        public void Remove(T item)
        {
            _data.Remove(item);
        }
        public void AddRange(IEnumerable<T> entities)
        {
            _data.AddRange(entities);
        }
        public List<T> GetAll()
        {
            return _data;
        }
    }
}
