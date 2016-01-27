using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;

namespace ClassicRelationalETL.DBcontrollers.MyCollection
{
    public class FastwwCollection<T> : IEnumerable<T>
    {
        private IList<T> _items;
        private IList<Expression<Func<T, object>>> _lookups;
        private Dictionary<string, ILookup<object, T>> _indexes;

        public FastwwCollection(IList<T> data)
        {
            _items = data;
            _lookups = new List<Expression<Func<T, object>>>();
            _indexes = new Dictionary<string, ILookup<object, T>>();
        }

        public void AddIndex(Expression<Func<T, object>> property)
        {
            _lookups.Add(property);
            _indexes.Add(property.ToString(), _items.ToLookup(property.Compile()));
        }

        public void Add(T item)
        {
            _items.Add(item);
            RebuildIndexes();
        }

        public void Remove(T item)
        {
            _items.Remove(item);
            RebuildIndexes();
        }

        public void RebuildIndexes()
        {
            if (_lookups.Count > 0)
            {
                _indexes = new Dictionary<string, ILookup<object, T>>();
                foreach (var lookup in _lookups)
                {
                    _indexes.Add(lookup.ToString(), _items.ToLookup(lookup.Compile()));
                }
            }
        }

        public IEnumerable<T> FindValue<TProperty>(Expression<Func<T, TProperty>> property, TProperty value)
        {
            var key = property.ToString();
            if (_indexes.ContainsKey(key))
            {
                return _indexes[key][value];
            }
            else
            {
                var c = property.Compile();
                return _items.Where(x => c(x).Equals(value));
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
