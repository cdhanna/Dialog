using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredicateMatcher
{
    public class MatchSystem
    {

        public Rule Match(List<Query> queries, List<Rule> rules)
        {
            var ruleScore = new Dictionary<Guid, int>();
            rules.ForEach(r => ruleScore.Add(r.Guid, 0));

            var highest = 0;
            var bestSoFar = new List<Rule>();

            foreach (var r in rules)
            {
                Dictionary<Guid, int> queryPtr = new Dictionary<Guid, int>();
                queries.ForEach(q => queryPtr.Add(q.Guid, 0));
                var successCount = 0;
                for(int i = 0; i < r.Criteria.Length; i++)
                {
                    var key = r.Criteria[i].Name;
                    var value = r.Criteria[i].Value;
                    var hValue = r.Criteria[i].HighValue;
                    var lValue = r.Criteria[i].LowValue;


                    queries.ForEach(q =>
                    {
                        var ptr = queryPtr[q.Guid];
                        while (ptr < q.Components.Length && q.Components[ptr].Key < key)
                        {
                            ptr += 1;
                        }
                        queryPtr[q.Guid] = ptr;
                        if (ptr < q.Components.Length && q.Components[ptr].Key == key)
                        {
                            // MATCH!
                            var v = q.Components[ptr].Value;
                            if (v >= lValue && v <= hValue)
                            {
                                successCount += 1;
                                //ruleScore[r.Guid] += 1;
                                //if (ruleScore[r.Guid] > highest)
                                //{
                                //    highest = ruleScore[r.Guid];
                                //    bestSoFar.Clear();
                                //    bestSoFar.Add(r);

                                //} else if (ruleScore[r.Guid] == highest)
                                //{
                                //    bestSoFar.Add(r);
                                //}
                            }
                        }
                    });
                }

                if (successCount == r.Criteria.Length)
                {
                    if (successCount > highest)
                    {
                        bestSoFar.Clear();
                        bestSoFar.Add(r);
                        highest = successCount;
                    } else if (successCount == highest)
                    {
                        bestSoFar.Add(r);
                    }
                }

            }

            var rand = new Random();
            var index = rand.Next(bestSoFar.Count);

            if (bestSoFar.Count > 0)
            {
                return bestSoFar[index];
            } else
            {
                return null;
            }

            // 1. Build Query Sources
            // 2. Sort Query Sources
            // 3. Match Query against Rules to produce criterion match count
            // 4. Pick Rule with highest criterion match count
            
           

        }
        
    }
}
