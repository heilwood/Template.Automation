using System;
using System.Linq;
using TechTalk.SpecFlow;

namespace Common.Automation.Common.Steps.StepsTransformation
{
    [Binding]
    public class CommonStepsTransformation
    {
        [StepArgumentTransformation]
        public bool StringToBoolean(string condition)
        {
            condition = condition.ToLowerInvariant();
            string[] positiveArray = { "is", "are", "increased", "yes", "true", "ja", "check", "checked", "contains", "has", "have", "open", "enabled", "should be" };
            string[] negativeArray = { "is not", "decreased", "are not", "no", "false", "nej", "nei", "uncheck", "unchecked", "do not contain", "has not", "have not", "close", "disabled", "should not be" };
            if (positiveArray.Any(e => e.Equals(condition)))
            {
                return true;
            }
            if (negativeArray.Any(e => e.Equals(condition)))
            {
                return false;
            }
            throw new Exception($"Unrecognized text can not be converted to boolean! Text [{condition}], recognizable strings: [{positiveArray},{negativeArray}]");
        }
    }
}
