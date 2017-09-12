using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredicateMatcher
{
    public enum CriterionOperator
    {
        EQUALS,
        GREATER_EQ,
        LESS_EQ
    }
    public class Criterion
    {
        public long Name { get; private set; }
        public long Value { get; private set; }
        public CriterionOperator Op { get; private set; }

        public long HighValue { get; private set; }
        public long LowValue { get; private set; }

        public Criterion(string name, CriterionOperator op, string value)
        {
            Name = name.GetHashCode();
            Value = value.GetHashCode();
            Op = op;
            SetHighLow();
        }
        public Criterion(string name, object value)
        {
            Op = CriterionOperator.EQUALS;
            Name = name.GetHashCode();
            Value = value.GetHashCode();
            SetHighLow();
        }

        public Criterion(string name, CriterionOperator op, object value)
        {
            Op = op;
            Name = name.GetHashCode();
            Value = value.GetHashCode();
            SetHighLow();
        }

        private void SetHighLow()
        {
            switch (Op)
            {
                case CriterionOperator.EQUALS:
                    LowValue = Value;
                    HighValue = Value;
                    break;
                case CriterionOperator.GREATER_EQ:
                    LowValue = Value;
                    HighValue = long.MaxValue;
                    break;
                case CriterionOperator.LESS_EQ:
                    LowValue = long.MinValue;
                    HighValue = Value;
                    break;
                default:
                    throw new NotImplementedException("No highlow known for critop");
            }
        }
    }
}
