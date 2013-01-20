using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TmdbWrapper.Cache
{
    internal class DatabaseCache
    {
        private readonly IDictionary<string, IDictionary<int, WeakReference>> cache = new Dictionary<string, IDictionary<int, WeakReference>>();

        private static readonly DatabaseCache _cache = new DatabaseCache();
        private readonly object _lock = new object();

        private object GetValue(Type type, int id)
        {
            object result = null;
            lock (_lock)
            {
                if (cache.ContainsKey(type.FullName))
                {
                    IDictionary<int, WeakReference> set = cache[type.FullName];
                    if (set.ContainsKey(id))
                    {
                        WeakReference reference = set[id];
                        if (reference.IsAlive)
                        {
                            result = reference.Target;
                        }
                    }
                }
            }
            return result; ;
        }

        private void SetValue(int id, object value)
        {
            lock (_lock)
            {
                if (!cache.ContainsKey(value.GetType().FullName))
                {
                    cache.Add(value.GetType().FullName, new Dictionary<int, WeakReference>());
                }
                IDictionary<int, WeakReference> set = cache[value.GetType().FullName];
                if (!set.ContainsKey(id))
                {
                    set.Add(id, new WeakReference(value));
                }
            }
        }


        public static T GetObject<T>(int id) where T : class
        {
            return _cache.GetValue(typeof(T), id) as T;
        }

        public static void SetObject(int id, object value)
        {
            _cache.SetValue(id, value);
        }
    }
}
