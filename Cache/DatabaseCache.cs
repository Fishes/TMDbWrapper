using System;
using System.Collections.Generic;

namespace TmdbWrapper.Cache
{
    internal class DatabaseCache
    {
        private readonly IDictionary<string, IDictionary<int, WeakReference>> cache = new Dictionary<string, IDictionary<int, WeakReference>>();

        private static readonly DatabaseCache CacheInstance = new DatabaseCache();
        private readonly object _lock = new object();

        private object GetValue(Type type, int id)
        {
            object result = null;
            lock (_lock)
            {
                if (cache.ContainsKey(type.FullName ?? throw new InvalidOperationException()))
                {
                    var set = cache[type.FullName];
                    if (set.ContainsKey(id))
                    {
                        var reference = set[id];
                        if (reference.IsAlive)
                        {
                            result = reference.Target;
                        }
                    }
                }
            }
            return result;
        }

        private void SetValue(int id, object value)
        {
            lock (_lock)
            {
                if (!cache.ContainsKey(value.GetType().FullName ?? throw new InvalidOperationException()))
                {
                    cache.Add(value.GetType().FullName ?? throw new InvalidOperationException(), new Dictionary<int, WeakReference>());
                }
                var set = cache[value.GetType().FullName ?? throw new InvalidOperationException()];
                if (!set.ContainsKey(id))
                {
                    set.Add(id, new WeakReference(value));
                }
            }
        }

        public static T GetObject<T>(int id) where T : class
        {
            return CacheInstance.GetValue(typeof(T), id) as T;
        }

        public static void SetObject(int id, object value)
        {
            CacheInstance.SetValue(id, value);
        }
    }
}