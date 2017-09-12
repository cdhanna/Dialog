using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PredicateMatcher;
using System.Linq;

namespace Tests
{
    [TestClass]
    public class QueryTests
    {
        [TestMethod]
        public void OrderQuery()
        {

            var q = new Query(
                    new QueryComponent("b", 1),
                    new QueryComponent("c", 2),
                    new QueryComponent("a", 3)
                );

            var l = new string[] { "b", "a", "c" }.Select(a => a.GetHashCode()).ToArray();
            l = l.OrderBy(x => x).ToArray();

            Assert.AreEqual(q.Components[0].Key, l[0]);
            Assert.AreEqual(q.Components[1].Key, l[1]);
            Assert.AreEqual(q.Components[2].Key, l[2]);

        }

        [TestMethod]
        public void Match_Greater()
        {
            var rules = new Rule[]
            {
                new Rule("RULE_A",
                    new Criterion("x", CriterionOperator.GREATER_EQ,  1)
                    )
            };

            var queries = new Query[] {
                new Query(
                    new QueryComponent("x", 2)
                )
            };

            var m = new MatchSystem();
            var output = m.Match(queries.ToList(), rules.ToList());

            Assert.AreEqual(output.Id, "RULE_A");
        }

        [TestMethod]
        public void Match_Lower()
        {
            var rules = new Rule[]
            {
                new Rule("RULE_A",
                    new Criterion("x", CriterionOperator.LESS_EQ, 1)
                    )
            };

            var queries = new Query[] {
                new Query(
                    new QueryComponent("x", 1)
                )
            };

            var m = new MatchSystem();
            var output = m.Match(queries.ToList(), rules.ToList());

            Assert.AreEqual(output.Id, "RULE_A");
        }

        [TestMethod]
        public void Match_Lower_And_Higher()
        {
            var rules = new Rule[]
            {
                new Rule("RULE_A",
                    new Criterion("x", CriterionOperator.GREATER_EQ, 2),
                    new Criterion("x", CriterionOperator.LESS_EQ, 4)
                    )
            };

            var queries = new Query[] {
                new Query(
                    new QueryComponent("x", 3)
                )
            };

            var m = new MatchSystem();
            var output = m.Match(queries.ToList(), rules.ToList());

            Assert.AreEqual(output.Id, "RULE_A");
        }

        [TestMethod]
        public void Match_Lower_And_Higher_Not()
        {
            var rules = new Rule[]
            {
                new Rule("RULE_A",
                    new Criterion("x", CriterionOperator.GREATER_EQ, 2),
                    new Criterion("x", CriterionOperator.LESS_EQ, 4)
                    )
            };

            var queries = new Query[] {
                new Query(
                    new QueryComponent("x", 5)
                )
            };

            var m = new MatchSystem();
            var output = m.Match(queries.ToList(), rules.ToList());

            Assert.IsNull(output);
        }

        [TestMethod]
        public void Match_OneRuleOneQuery()
        {
            var rules = new Rule[]
            {
                new Rule("RULE_A",
                    new Criterion("x", 1)
                    )
            };

            var queries = new Query[] {
                new Query(
                    new QueryComponent("x", 1)
                )
            };

            var m = new MatchSystem();
            var output = m.Match(queries.ToList(), rules.ToList());

            Assert.AreEqual(output.Id, "RULE_A");
        }

        [TestMethod]
        public void Match_MissingCrit()
        {
            var rules = new Rule[]
            {
                new Rule("RULE_A",
                    new Criterion("x", 1),
                    new Criterion("y", 2)
                    )
            };

            var queries = new Query[] {
                new Query(
                    new QueryComponent("x", 1)
                )
            };

            var m = new MatchSystem();
            var output = m.Match(queries.ToList(), rules.ToList());
            Assert.IsNull(output);
        }

        [TestMethod]
        public void Match_MultipleRules()
        {
            var rules = new Rule[]
            {
                new Rule("RULE_A",
                    new Criterion("x", 3),
                    new Criterion("y", 2)
                    ),
                new Rule("RULE_B",
                    new Criterion("x", 1)
                )
            };

            var queries = new Query[] {
                new Query(
                    new QueryComponent("x", 1)
                )
            };

            var m = new MatchSystem();
            var output = m.Match(queries.ToList(), rules.ToList());

            Assert.AreEqual(output.Id, "RULE_B");
        }

        [TestMethod]
        public void Match_MultipleRulesAndComponents()
        {
            var rules = new Rule[]
            {
                new Rule("RULE_A",
                    new Criterion("x", 1),
                    new Criterion("y", 2)
                    ),
                new Rule("RULE_B",
                    new Criterion("x", 1)
                )
            };

            var queries = new Query[] {
                new Query(
                    new QueryComponent("x", 1),
                    new QueryComponent("y", 2)
                )
            };

            var m = new MatchSystem();
            var output = m.Match(queries.ToList(), rules.ToList());

            Assert.AreEqual(output.Id, "RULE_A");
        }

        [TestMethod]
        public void Match_C1()
        {
            var rules = new Rule[]
            {
                new Rule("RULE_A",
                    new Criterion("x", 1),
                    new Criterion("y", 2)
                    ),
                new Rule("RULE_B",
                    new Criterion("x", 1)
                    ),
                new Rule("RULE_C",
                    new Criterion("x", 1),
                    new Criterion("z", 3) 
                    )
            };

            var queries = new Query[] {
                new Query(
                    new QueryComponent("x", 1),
                    new QueryComponent("y", 3)
                    ),
                new Query(
                    new QueryComponent("z", 3)
                    )
            };

            var m = new MatchSystem();
            var output = m.Match(queries.ToList(), rules.ToList());

            Assert.AreEqual(output.Id, "RULE_C");
        }
    }
}
