using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredicateMatcher
{
    public class Rule
    {
        public Guid Guid { get; private set; }
        public string Id { get; private set; }
        public Criterion[] Criteria { get; private set; }

        public string[] Outputs { get; private set; }
        //public string[] Scripts { get; private set; }

        public Rule(string name, params Criterion[] criteria)
        {
            Guid = Guid.NewGuid();
            Id = name;
            Criteria = criteria.OrderBy(c => c.Name).ToArray();
        }

        public Rule(string name, Criterion[] criteria, string[] outputs)
            : this(name, criteria)
        {
            Outputs = outputs;
        }
    }
}
