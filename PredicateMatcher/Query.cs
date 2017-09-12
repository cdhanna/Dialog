using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredicateMatcher
{
    public struct QueryComponent
    {
        public long Key;
        public long Value;

        public QueryComponent(long key, long value)
        {
            Key = key;
            Value = value;
        }

        public QueryComponent(string key, object value)
        {
            Key = key.GetHashCode();
            Value = value.GetHashCode();
        }
    }

    public class Query
    {
        public Guid Guid { get; private set; }
        public QueryComponent[] Components { get; private set; }

        public Query(params QueryComponent[] components)
        {
            Guid = Guid.NewGuid();
            Components = components.OrderBy(c => c.Key).ToArray();
        }


    }
}
