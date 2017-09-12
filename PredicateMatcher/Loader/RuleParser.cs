using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredicateMatcher.Loader
{
    public class RuleParser
    {
        public Rule[] ParseFile(string filePath)
        {
            var json = File.ReadAllText(filePath);
            return ParseJson(json);
        }
        public Rule[] ParseJson(string json)
        {
            var collection = JsonConvert.DeserializeObject<RuleCollectionModel>(json);

            return collection.Rules.Select(model =>
            {

                var criteria = new List<Criterion>();
                model.Criteria.ToList().ForEach(cModel => criteria.AddRange(ParseCriteria(cModel)));

                var rule = new Rule(model.Id, criteria.ToArray(), model.Text);

                return rule;
            }).ToArray();
        }

        private List<Criterion> ParseCriteria(string criteriaString)
        {
            var parts = criteriaString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            var name = "";
            var op = "";
            var val = "";

            switch (parts.Length)
            {
                case 1:
                    name = parts[0];
                    op = "=";
                    val = "true";
                    break;
                case 2:
                    name = parts[0];
                    op = "=";
                    val = parts[1];
                    break;
                case 3:
                    name = parts[0];
                    op = parts[1];
                    val = parts[2];
                    break;
                default:
                    throw new NotImplementedException("Can only handle 1, 2, or 3 componenent criteria string. The string " + criteriaString + " is no good.");
            }

            // parse value 
            var value = ParseValue(val);


            var resultingCriteria = new List<Criterion>();

            switch (op)
            {
                case "=":
                    resultingCriteria.Add(new Criterion(name, CriterionOperator.EQUALS, value));
                    break;
                case ">=":
                    resultingCriteria.Add(new Criterion(name, CriterionOperator.GREATER_EQ, value));
                    break;
                case "<=":
                    resultingCriteria.Add(new Criterion(name, CriterionOperator.LESS_EQ, value));
                    break;

                default:
                    throw new NotImplementedException("Unknown operation " + op);
            }

            return resultingCriteria;
        }

        private object ParseValue(string valueString)
        {
            // is it an integer ?
            var intVal = 0;
            if (int.TryParse(valueString, out intVal))
            {
                return intVal;
            }
            // is it a boolean ?
            var boolVal = false;
            if (bool.TryParse(valueString, out boolVal))
            {
                return boolVal;
            }
            // or perhaps a float ?
            var floatVal = 0f;
            if (float.TryParse(valueString, out floatVal))
            {
                return floatVal;
            }
            // perchance a string ?
            return valueString;
        }

    }
}
